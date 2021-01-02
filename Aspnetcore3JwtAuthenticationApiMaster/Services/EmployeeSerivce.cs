
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Helpers;
using WebApi.Models;

namespace WebApi.Services
{



    public interface IEmployeeSerivce
    {
         IEnumerable<Employee> GetAll();
        Employee GetById(int id);
    }
    public class EmployeeSerivce : IEmployeeSerivce
    {
        private List<Employee> empList = new List<Employee>
            {
                new Employee{Id=101,FirstName="Abhinav",Location="Bangalore",Salary=12345},
                new Employee{Id=102,FirstName="Abhishek",Location="Chennai",Salary=23456},
                new Employee{Id=103,FirstName="Akshay",Location="Bangalore",Salary=34567},
                new Employee{Id=104,FirstName="Akash",Location="Chennai",Salary=45678},
                new Employee{Id=105,FirstName="Anil",Location="Bangalore",Salary=56789}
        };

        public IEnumerable<Employee> GetAll()
        {
 
            return empList;
        }

        public Employee GetById(int id)
        {
            return empList.FirstOrDefault(x => x.Id == id);
        }
    }
}
