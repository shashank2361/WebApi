using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class TblImage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Size { get; set; }
        public byte[] ImageData { get; set; }
    }
}
