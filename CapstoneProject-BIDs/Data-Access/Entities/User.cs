using System;
using System.Collections.Generic;

#nullable disable

namespace Data_Access.Entities
{
    public partial class User
    {
        public User()
        {
            BanHistories = new HashSet<BanHistory>();
            Items = new HashSet<Item>();
            Payments = new HashSet<Payment>();
            SessionDetails = new HashSet<SessionDetail>();
        }

        public Guid UserId { get; set; }
        public string AccountName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Cccdnumber { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string CccdfrontImage { get; set; }
        public string CccdbackImage { get; set; }
        public string Notification { get; set; }
        public int Status { get; set; }

        public virtual ICollection<BanHistory> BanHistories { get; set; }
        public virtual ICollection<Item> Items { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<SessionDetail> SessionDetails { get; set; }
    }
}
