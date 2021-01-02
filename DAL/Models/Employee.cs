using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public int? Salary { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Country { get; set; }
        public string Location { get; set; }
        public DateTime? DateOfJoining { get; set; }
    }
}
