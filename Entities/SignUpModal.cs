
using System.Text.Json.Serialization;


namespace WebApi.Entities
{
    public class SignUpModal
    { 
        public string UserName { get; set; }
        //[JsonIgnore]
        public string Password { get; set; }
        public string Avatar { get; set; }


        public string Bio { get; set; }
    }
}
