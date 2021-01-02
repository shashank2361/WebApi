using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class TblProduct
    {
        public int ProdcutId { get; set; }
        public string Name { get; set; }
        public int? UnitPrice { get; set; }
        public int? QuantitySold { get; set; }
    }
}
