using DAL;
using DAL.Models;
using System;
using System.Collections.Generic;

namespace BLL
{
    public interface IUserBs
    {
        IEnumerable<User> GetAll();
 
        User GetById(int id);
        bool Create(User user);
        bool Update(User user);
        bool Delete(int id);
        bool ValidateToken(string refreshToken);
        bool RevokeToken(string refreshtoken, string ipAddress);
    }

    public class UserBs : IUserBs
    {
        IUserDb UserDb;
        public UserBs(IUserDb userDb)
        {
            UserDb = userDb;
        }
        public bool Create(User user)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            var users = UserDb.GetAll();
            return users;
        }

        public User GetById(int id)
        {
            var user = UserDb.GetById(id);
            return user;
        }

        public bool ValidateToken(string refreshToken)
        {
            return UserDb.GetRefreshToken(refreshToken);
        }

        public bool Update(User user)
        {
            return UserDb.Update(user);
        }

        public bool RevokeToken(string refreshtoken, string ipAddress)
        {
            return UserDb.RevokeToken(refreshtoken, ipAddress);
        }
 
    }
}
