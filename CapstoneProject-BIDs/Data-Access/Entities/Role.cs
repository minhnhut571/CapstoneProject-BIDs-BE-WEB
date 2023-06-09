using System;
using System.Collections.Generic;

#nullable disable

namespace Data_Access.Entities
{
    public partial class Role
    {
        public Role()
        {
            Staff = new HashSet<Staff>();
        }

        public string RoleName { get; set; }
        public bool Status { get; set; }
        public int RoleId { get; set; }

        public virtual ICollection<Staff> Staff { get; set; }
    }
}
