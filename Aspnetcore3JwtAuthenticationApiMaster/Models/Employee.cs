using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Dob { get; set; }
        public DateTime Doj { get; set; }
        public bool Manager { get; set; }
        public string Location { get; set; }
        public int Salary { get; set; }
    }
}
