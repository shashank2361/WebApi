using BLL;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Helpers
{

    public interface ITokenRefresher
    {
        AuthenticateResponse Refresh(RefreshCred refreshCred  , string ipAddress);
        IDictionary<string, string> UsersRefreshTokens { get; set; }

    }
    public class TokenRefresher : ITokenRefresher 
    {
        private readonly AppSettings _appSettings;
        private   IJWTAuthenticationManager jWTAuthenticationManager;
        public  IDictionary<string, string> UsersRefreshTokens { get; set; }
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        IUserBs userBs;

        public TokenRefresher(IOptions<AppSettings> appSettings , IJWTAuthenticationManager jWTAuthenticationManager 
              , IHttpContextAccessor httpContextAccessor , IUserBs userBs)
        {
            _appSettings = appSettings.Value;
            this.jWTAuthenticationManager = jWTAuthenticationManager;            
            _httpContextAccessor = httpContextAccessor;
            this.userBs = userBs;
        }
        public AuthenticateResponse Refresh(RefreshCred refreshCred , string ipAddress)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);     
            var pricipal = tokenHandler.ValidateToken(refreshCred.JwtToken, new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false //here we are saying that we don't care about the token's expiration date

                // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                //  ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = validatedToken as JwtSecurityToken;
            var lifeTime = new JwtSecurityTokenHandler().ReadToken(refreshCred.JwtToken).ValidTo;

            if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token passed");
            }
            var userName = pricipal.Identity.Name;
            var userId =  Convert.ToInt32( pricipal.FindFirst("id").Value);
            var Users = userBs.GetAll();
             var user = Users.SingleOrDefault(x => x.Id == userId);

            var sessionrefreshtoken = user?.RefreshTokens?.Where(x => x.Token.Trim().Equals(refreshCred?.RefreshToken?.Trim()))?.FirstOrDefault()?.Token;
            //_session?.GetString(userName)?.ToString();
            if (sessionrefreshtoken  == null)
            {
                    throw new SecurityTokenException("Invalid token passed!");
            }

            ///jWTAuthenticationManager.UsersRefreshTokens[userName] need to fix why not working
            //if (  user.RefreshTokens.Single(x => x.Token.Trim().Equals(refreshCred.RefreshToken.Trim())
            //{
            // //   throw new SecurityTokenException("Invalid token passed!")
            //}

            return jWTAuthenticationManager.Authenticate(userName, pricipal.Claims.ToArray() , ipAddress ,   refreshCred);
        }
    }
}
