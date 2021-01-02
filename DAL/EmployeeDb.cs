using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public interface IEmployeeDb
    {
        IEnumerable<Employee> GetAll();
        IEnumerable<string> GetAllLocation();

        Employee GetById(int id);
        bool Create(Employee employee);
        bool Update(Employee employee);
        bool Delete(int id);
    }
    public class EmployeeDb : IEmployeeDb
    {
        EmployeeDBContext EmployeeDBContext;

        public EmployeeDb(EmployeeDBContext _employeeDBContext )
        {
            EmployeeDBContext = _employeeDBContext;
        }
        public bool Create(Employee employee)
        {
             EmployeeDBContext.Add(employee);
            EmployeeDBContext.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var employee = EmployeeDBContext.Employees.Find(id);
            EmployeeDBContext.Remove<Employee>(employee);
            EmployeeDBContext.SaveChanges();
            return true;
        }

        public IEnumerable<Employee> GetAll()
        {
            return EmployeeDBContext.Employees;
        }

        public IEnumerable<string> GetAllLocation()
        {
            return EmployeeDBContext.Employees.Select(t => t.Location).Distinct();

        }

        public Employee GetById(int id)
        {
            var employee = EmployeeDBContext.Employees.Find(id);
            return employee;
        }

        public bool Update(Employee employee)
        {
            EmployeeDBContext.Update<Employee>(employee);
            EmployeeDBContext.SaveChanges();
            return true;
        }
    }
}
