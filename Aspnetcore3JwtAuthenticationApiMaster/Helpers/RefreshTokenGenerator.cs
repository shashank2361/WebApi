using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Services
{
    public interface IRefreshTokenGenerator
    {
        RefreshToken generateRefreshToken(string ipAddress);
    }
    public class RefreshTokenGenerator : IRefreshTokenGenerator
    {
        public RefreshToken generateRefreshToken(string ipAddress)
        {
            
                using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
                {
                    var randomBytes = new byte[64];
                    rngCryptoServiceProvider.GetBytes(randomBytes);
                    return new RefreshToken
                    {
                        Token = Convert.ToBase64String(randomBytes),
                        Expires = DateTime.UtcNow.AddMinutes(15),  // DateTime.UtcNow.AddDays(7),
                        Created = DateTime.UtcNow,
                        CreatedByIp = ipAddress
                    };
                }
            

        }




    }




}
