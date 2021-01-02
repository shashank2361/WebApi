using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class TblRate
    {
        public int Id { get; set; }
        public string Currency { get; set; }
        public decimal? Rate { get; set; }
    }
}
