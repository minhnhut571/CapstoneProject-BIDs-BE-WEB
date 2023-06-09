using System;
using System.Collections.Generic;

#nullable disable

namespace Data_Access.Entities
{
    public partial class Staff
    {
        public Guid StaffId { get; set; }
        public string AccountName { get; set; }
        public string StaffName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string Notification { get; set; }
        public bool Status { get; set; }
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }
    }
}
