using BLL;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Helpers
{



    public interface IJWTAuthenticationManager
    {

        AuthenticateResponse Authenticate(AuthenticateRequest model, string ipAddress);
        IDictionary<string, string> UsersRefreshTokens { get; set; }
        AuthenticateResponse Authenticate(string userName, Claim[] claims, string ipAddress , RefreshCred refreshCred);
        bool refreshtokenvalidate(string refreshtoken);
        bool RevokeToken(string token, string ipAddress);
    };
    public class JWTAuthenticationManager : IJWTAuthenticationManager
    {
        public IDictionary<string, string> UsersRefreshTokens { get; set; }
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
 

        private readonly AppSettings _appSettings;
        
        IUserBs _userBs;
        IRefreshTokenGenerator refreshTokenGenerator;
  

        public JWTAuthenticationManager(IOptions<AppSettings> appSettings, IUserBs userBs, IRefreshTokenGenerator refreshTokenGenerator
            , IHttpContextAccessor httpContextAccessor
            )
        {
            _appSettings = appSettings.Value;
            _userBs = userBs;
            this.refreshTokenGenerator = refreshTokenGenerator;
            UsersRefreshTokens = new Dictionary<string, string>();
            _httpContextAccessor = httpContextAccessor;
        }


        public AuthenticateResponse Authenticate(AuthenticateRequest model, string ipAddress)
        {
            var Users = _userBs.GetAll();
            var user = Users.SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);
            // return null if user not found
            if (user == null) return null;
            var Usermodal = new WebApi.Models.User()
            {
                Username = user.Username,
                Id = user.Id,
                Password = user.Password,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
            // authentication successful so generate jwt token
            var token = GenerateJwtToken(Usermodal);
            var refreshToken = refreshTokenGenerator.generateRefreshToken(ipAddress);

            user.RefreshTokens.Add(new DAL.Models.RefreshToken()
            {
                Token = refreshToken.Token,
                Expires = refreshToken.Expires,
                IsExpired =  refreshToken.IsExpired,
                Created = refreshToken.Created,
                CreatedByIp = refreshToken.CreatedByIp,
                Revoked = refreshToken.Revoked,
                RevokedByIp = refreshToken.RevokedByIp,
                ReplacedByToken = refreshToken.ReplacedByToken,
                IsActive = refreshToken.IsActive,
                UserId = user.Id
            });

            var success = _userBs.Update(user);

            //System.Threading.Thread.Sleep(2000);
            var xxx = _session?.GetString(user?.Username);
            // first tokens
            //_session.SetString(user.Username, refreshToken.Token);



            //if (UsersRefreshTokens.ContainsKey(user.Username))
            //{
            //    UsersRefreshTokens[user.Username] = refreshToken.Token;
            //}
            //else
            //{
            //    UsersRefreshTokens.Add(user.Username, refreshToken.Token);
            //}
            return new AuthenticateResponse(Usermodal, token, refreshToken.Token);
        }

        // overload method 
        public AuthenticateResponse Authenticate(string userName, Claim[] claims, string ipAddress , RefreshCred refreshCred)
        {
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var jwtSecurityToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(5),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                );

            var Users = _userBs.GetAll();
            var user = Users.SingleOrDefault(x => x.Username == userName);
            // return null if user not found
            if (user == null) return null;
            var Usermodal = new WebApi.Models.User()
            {
                Username = user.Username,
                Id = user.Id,
                Password = user.Password,
                FirstName = user.FirstName,
                LastName = user.LastName
            };


            var token = GenerateJwtToken(Usermodal, claims);
            // var token  = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            var refreshToken = user.RefreshTokens.Single(x => x.Token.Trim().Equals(refreshCred.RefreshToken.Trim()));

            // this is to change the expired and active conditions
            // pending using automapper
            var refrshtokenMod = new Models.RefreshToken()
            {
                Revoked = DateTime.UtcNow,
               Expires = (DateTime)refreshToken.Expires,
                                    
            
            };
            var newrefreshToken = refreshTokenGenerator.generateRefreshToken(ipAddress);
            // subsequesnt tokens
            _session.SetString(user.Username, newrefreshToken.Token);
            refreshToken.Expires = refrshtokenMod.Expires;
            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;
            refreshToken.ReplacedByToken = newrefreshToken.Token;
            refreshToken.IsActive = refrshtokenMod.IsActive;


            user.RefreshTokens.Add(new DAL.Models.RefreshToken()
            {
                Token = newrefreshToken.Token,
                Expires = newrefreshToken.Expires,
                IsExpired =  newrefreshToken.IsExpired ,
                Created = newrefreshToken.Created,
                CreatedByIp = newrefreshToken.CreatedByIp,
                Revoked = newrefreshToken.Revoked,
                RevokedByIp = newrefreshToken.RevokedByIp,
                ReplacedByToken = newrefreshToken.ReplacedByToken,
                IsActive = newrefreshToken.IsActive,
                UserId = user.Id
            });

            var success = _userBs.Update(user);

        
            return new AuthenticateResponse(Usermodal, token, newrefreshToken.Token);
        }

        public string GenerateJwtToken(User user, Claim[] claims = null) // same as generate token string
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims ?? new Claim[]  {
                      new Claim(ClaimTypes.Name, user.Username.ToString()),
                      new Claim("id", user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };

            // added 2 claims just to remember to add  custom claim
            //Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) ,
            //        new Claim(ClaimTypes.Name, user.Username.ToString()) }),

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        bool IJWTAuthenticationManager.refreshtokenvalidate(string refreshtoken)
        {
            var tokenExists = _userBs.ValidateToken(refreshtoken);
            return tokenExists;
        }

        public bool RevokeToken(string token, string ipAddress)
        {
            var user = _userBs.GetAll().SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));


            var refreshToken= user.RefreshTokens.Single(t =>t.Token == token);

            if (refreshToken == null) return false;
            
            //var refreshToken = user.RefreshTokens.Single(x => x.Token.Trim().Equals(token.Trim()));
            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;

            var success = _userBs.Update(user);
            return success;
 
        }
    }
}

