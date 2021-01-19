using BLL;
using DAL;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using WebApi.Controllers;
using WebApi.Helpers;
using WebApi.Services;
using Xunit;

namespace Test
{
    public class UnitTest1
    {

        EmployeeController _employeeController;
        //private IUserService _userService;
        IEmployeeBs _employeeBs;
        IEmployeeDb _employeeDb;

        IOptions<AppSettings> _appSettings;


        public UnitTest1()
        {
            //_employeeDb = new EmployeeDb();
            //_employeeBs = new EmployeeBs();
            //_appSettings = new Options<AppSettings>();
            //_userService = new UserService();

            //_employeeController = new EmployeeController(_employeeBs);

        }
        [Fact]
        public void Test1()
        {
           Assert.True(true, $"The test  is not valid");

        }

      
    }
}
