using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class TblMenuItemsLevel2
    {
        public int Id { get; set; }
        public string MenuText { get; set; }
        public string NavigateUrl { get; set; }
        public int? ParentId { get; set; }

        public virtual TblMenuItemsLevel1 Parent { get; set; }
    }
}
