using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class TblCity1
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? CountryId { get; set; }

        public virtual TblCountry1 Country { get; set; }
    }
}
