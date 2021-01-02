using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class TblProduct1
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ProductDescription { get; set; }
        public int UnitPrice { get; set; }
        public int QuantitySold { get; set; }
    }
}
