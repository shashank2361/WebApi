using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using WebApi.Models;
using WebApi.Helpers;
using WebApi.Models;
using BLL;

namespace WebApi.Services
{
    public interface IUserService
    {
     //   AuthenticateResponse Authenticate(AuthenticateRequest model);
        //IEnumerable<User> GetAll();
     //    User GetById(int id);
    }

    public class UserService : IUserService
    {
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        //private List<User> _users = new List<User>
        //{
        //    new User { Id = 1, FirstName = "Test", LastName = "User", Username = "test", Password = "test" },
        //    new User { Id = 2, FirstName = "Test2", LastName = "User2", Username = "test2", Password = "test2" }

        //};


        //IUserBs _userBs;

        //private readonly AppSettings _appSettings;
        //private IJWTAuthenticationManager _JWTAuthenticationManager;

        //public UserService(IOptions<AppSettings> appSettings, IUserBs userBs, IJWTAuthenticationManager JWTAuthenticationManager)
        //{
        //    _appSettings = appSettings.Value;
        //    _JWTAuthenticationManager = JWTAuthenticationManager;
        //    _userBs = userBs;
        //}

        //public AuthenticateResponse Authenticate(AuthenticateRequest model)
        //{
        //    var  Users = _userBs.GetAll();

        //    var user = Users.SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);

        //    // return null if user not found
        //    if (user == null) return null;
        //    var  Usermodal = new WebApi.Models.User()
        //    {
        //        Username = user.Username,
        //        Id = user.Id,
        //        Password = user.Password,
        //        FirstName = user.FirstName,
        //        LastName = user.LastName
        //    };
        //    // authentication successful so generate jwt token
        //    var token = _JWTAuthenticationManager.generateJwtToken(Usermodal);
        //    System.Threading.Thread.Sleep(2000);

        //    return new AuthenticateResponse(Usermodal, token);
        //}

        //public IEnumerable<User> GetAll()
        //{
        //    return _users;
        //}

        //public User GetById(int id)
        //{
        //    return _users.FirstOrDefault(x => x.Id == id);
        //}

        // helper methods

        //private string generateJwtToken(User user)
        //{
        //    // generate token that is valid for 7 days
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
        //        Expires = DateTime.UtcNow.AddMinutes(60),
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //    };
        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    return tokenHandler.WriteToken(token);
        //}
    }
}