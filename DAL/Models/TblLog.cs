using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class TblLog
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public string ExceptionMessage { get; set; }
    }
}
