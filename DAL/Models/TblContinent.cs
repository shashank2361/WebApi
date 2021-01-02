using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class TblContinent
    {
        public TblContinent()
        {
            TblCountries = new HashSet<TblCountry>();
        }

        public int ContinentId { get; set; }
        public string ContinentName { get; set; }

        public virtual ICollection<TblCountry> TblCountries { get; set; }
    }
}
