using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class TblResetPasswordRequest
    {
        public Guid Id { get; set; }
        public int? UserId { get; set; }
        public DateTime? ResetRequestDateTime { get; set; }

        public virtual Tbluser User { get; set; }
    }
}
