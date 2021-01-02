using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class TblCity
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
        public int? CountryId { get; set; }

        public virtual TblCountry Country { get; set; }
    }
}
