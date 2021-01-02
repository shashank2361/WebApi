using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class TblMenuItemsLevel1
    {
        public TblMenuItemsLevel1()
        {
            TblMenuItemsLevel2s = new HashSet<TblMenuItemsLevel2>();
        }

        public int Id { get; set; }
        public string MenuText { get; set; }
        public string NavigateUrl { get; set; }

        public virtual ICollection<TblMenuItemsLevel2> TblMenuItemsLevel2s { get; set; }
    }
}
