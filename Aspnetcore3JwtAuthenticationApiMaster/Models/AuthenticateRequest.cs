using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class AuthenticateRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class RefreshCred
    {
      
        public string JwtToken { get; set; }

     
        public string RefreshToken { get; set; }
    }

}