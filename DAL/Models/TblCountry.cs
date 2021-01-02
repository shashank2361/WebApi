using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class TblCountry
    {
        public TblCountry()
        {
            TblCities = new HashSet<TblCity>();
        }

        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public int? ContinentId { get; set; }

        public virtual TblContinent Continent { get; set; }
        public virtual ICollection<TblCity> TblCities { get; set; }
    }
}
