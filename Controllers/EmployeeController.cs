using BLL;
 using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
 using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        //private IEmployeeSerivce _employeeSerivce;
        IEmployeeBs employeeBs;
        public EmployeeController(IEmployeeBs _employeeBs )
        {
            // _employeeSerivce = employeeSerivce;
            employeeBs = _employeeBs;
        }
        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
          //  HttpContext.Session.SetString("emp", JsonConvert.SerializeObject(_employeeSerivce.GetAll()));

            var emps = employeeBs.GetAll();
            return Ok(emps);

        }
        //[Authorize]
        [HttpGet("/GetLocations")]
        public IActionResult GetLocations()
        {
            var emp = employeeBs.GetAllLocation();
            return Ok(emp);
        }

        [Authorize]
        [HttpPut]
        public IActionResult Edit(Employee emp)
        {
            //var employee = employeeBs.GetById(emp.Id);
            var empl = new DAL.Models.Employee
            {
                Id = emp.Id,
                FirstName = emp.FirstName,
                LastName = emp.LastName,
                Gender = "Male",
                Country = "UK",
                DateOfBirth = emp.Dob,
                DateOfJoining = emp.Doj,
                Location = emp.Location,
                Salary = emp.Salary                
            };

             employeeBs.Update(empl);
 
            //return employeeBs.Update(emp);

           // System.Threading.Thread.Sleep(5000);
              return Ok(empl);
        }


        [Authorize]
        [HttpPost("/Save")]
        public IActionResult Save(Employee emp)
        {

            System.Threading.Thread.Sleep(2000);
            return Ok(true);


            var dalEmp = new DAL.Models.Employee{
            Id = emp.Id,
            FirstName = emp.FirstName,
            LastName = emp.LastName,
            DateOfBirth = emp.Dob,
            DateOfJoining = emp.Doj,
            Salary = emp.Salary,
            Location = emp.Location
            };

            var success = employeeBs.Create(dalEmp);
            System.Threading.Thread.Sleep(2000);
            return Ok(success);
        }
    }
}


 