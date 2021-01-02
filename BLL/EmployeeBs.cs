using DAL;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{

    public interface IEmployeeBs
    {
        IEnumerable<Employee> GetAll();
        IEnumerable<string> GetAllLocation();

        Employee GetById(int id);
        bool Create(Employee employee);
        bool Update(Employee employee);
        bool Delete(int id);
    }
    public class EmployeeBs : IEmployeeBs
    {
        IEmployeeDb EmployeeDb;
        public EmployeeBs(IEmployeeDb employeeDb)
        {
            EmployeeDb = employeeDb;
        }
        public bool Create(Employee employee)
        {
            return EmployeeDb.Create(employee);
        }

        public bool Delete(int id)
        {
            return EmployeeDb.Delete(id);
        }

        public IEnumerable<Employee> GetAll()
        {
            var employees = EmployeeDb.GetAll();
            return employees;
        }

        public IEnumerable<string> GetAllLocation()
        {
            var locations = EmployeeDb.GetAllLocation();
            return locations;
        }

        public Employee GetById(int id)
        {
            var employee = EmployeeDb.GetById(id);
            return employee;
        }

        public bool Update(Employee employee)
        {
            return EmployeeDb.Update(employee);
        }
    }
}
