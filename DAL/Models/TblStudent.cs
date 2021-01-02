using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class TblStudent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string City { get; set; }
    }
}
