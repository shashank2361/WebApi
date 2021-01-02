using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class TblEmployee
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string City { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
