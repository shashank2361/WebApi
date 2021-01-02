using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class TblCountry1
    {
        public TblCountry1()
        {
            TblCity1s = new HashSet<TblCity1>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<TblCity1> TblCity1s { get; set; }
    }
}
