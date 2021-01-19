using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public interface IUserDb
    {
        IEnumerable<User> GetAll();

        User GetById(int id);
        bool Create(User user);
        bool Update(User user);
        bool Delete(int id);
        bool GetRefreshToken(string refreshToken);
        bool RevokeToken(string refreshtoken, string ipAddress);
    }


    public class UserDb : IUserDb
    {
        EmployeeDBContext EmployeeDBContext;

        public UserDb(EmployeeDBContext _employeeDBContext)
        {
            EmployeeDBContext = _employeeDBContext;
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
            //  var RefreshTokens = EmployeeDBContext.RefreshTokens;
            return EmployeeDBContext.Users.Include(x => x.RefreshTokens ) ;
        }

        public User GetById(int id)
        {
             
            var user = EmployeeDBContext.Users.Find(id) ;
            return user;
        }

        public bool GetRefreshToken(string refreshToken)
        {
            return EmployeeDBContext.RefreshTokens.SingleAsync(x => x.Token == refreshToken) != null ? true : false;
        }

        public bool RevokeToken(string refreshtoken, string ipAddress)
        {
            var revokedToken = EmployeeDBContext.RefreshTokens.SingleAsync(x => x.Token == refreshtoken && x.CreatedByIp == ipAddress) != null ? true : false;
            return revokedToken;
            
        }

        //public bool RevokeToken(string refreshtoken, string ipAddress)
        //{
        //    var revokedToken = EmployeeDBContext.RefreshTokens.SingleAsync(x => x.Token == refreshtoken && x.CreatedByIp) != null ? true : false;
        //    return 

        //}

        public bool Update(User user)
        {
            EmployeeDBContext.Update<User>(user);
            EmployeeDBContext.SaveChanges();
            return true;

           
        }
    }
}
