using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class TblTreeViewItem
    {
        public TblTreeViewItem()
        {
            InverseParent = new HashSet<TblTreeViewItem>();
        }

        public int Id { get; set; }
        public string TreeViewText { get; set; }
        public string NavigateUrl { get; set; }
        public int? ParentId { get; set; }

        public virtual TblTreeViewItem Parent { get; set; }
        public virtual ICollection<TblTreeViewItem> InverseParent { get; set; }
    }
}
