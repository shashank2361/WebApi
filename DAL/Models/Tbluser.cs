using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class Tbluser
    {
        public Tbluser()
        {
            TblResetPasswordRequests = new HashSet<TblResetPasswordRequest>();
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int? RetryAttempts { get; set; }
        public bool? IsLocked { get; set; }
        public DateTime? LockedDateTime { get; set; }

        public virtual ICollection<TblResetPasswordRequest> TblResetPasswordRequests { get; set; }
    }
}
