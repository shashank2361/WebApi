using BLL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using WebApi.Helpers;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        //private IUserService _userService;
        IUserBs _userBs;
        IJWTAuthenticationManager _JWTAuthenticationManager;
        ITokenRefresher _tokenRefresher;

        public UsersController(  IUserBs  userBs, IJWTAuthenticationManager JWTAuthenticationManager , ITokenRefresher  tokenRefresher)
        {
            //_userService = userService;
            _userBs = userBs;
            _JWTAuthenticationManager = JWTAuthenticationManager;
            _tokenRefresher = tokenRefresher;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = _JWTAuthenticationManager.Authenticate(model , ipAddress());
             System.Threading.Thread.Sleep(2000);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            setTokenCookie(response.RefreshToken);
            HttpContext.Session.SetString( response.Username, response.RefreshToken);
            return Ok(response);
        }

 
        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userBs.GetAll();
            return Ok(users);
        }


        [AllowAnonymous]
        [HttpPost("refresh")]
        public IActionResult Refresh (RefreshCred  refreshCred)
        {
            //var refreshTokenfromCookie = Request.Cookies["refreshToken"];

            var response = _tokenRefresher.Refresh(refreshCred , ipAddress() );

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });
            return Ok(response);


            //var refreshToken = Request.Cookies["refreshToken"];

            //var response = _userService.RefreshToken(refreshToken, ipAddress());

            //if (response == null)
            //    return Unauthorized(new { message = "Invalid token" });

            //setTokenCookie(response.RefreshToken);

            //return Ok(response);
        }


        [HttpPost("revoke-token")]
        public IActionResult RevokeToken([FromBody] RevokeTokenRequest model)
        {
            // accept token from request body or cookie
            var istokenExists = _JWTAuthenticationManager.refreshtokenvalidate(model.Token);

            if (!istokenExists)
                return BadRequest(new { message = "Token is required" });

            var response = _JWTAuthenticationManager.RevokeToken(model.Token, ipAddress());

            //if (!response)
            //    return NotFound(new { message = "Token not found" });

            return Ok(new { message = "Token revoked" });
        }





        private void setTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }

        private string ipAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
    }
}
