using System;
using System.Collections.Generic;

#nullable disable

namespace Data_Access.Entities
{
    public partial class Item
    {
        public Item()
        {
            Payments = new HashSet<Payment>();
            Sessions = new HashSet<Session>();
        }

        public Guid ItemId { get; set; }
        public Guid UserId { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }
        public Guid ItemTypeId { get; set; }
        public Guid Quantity { get; set; }
        public string Image { get; set; }
        public double FristPrice { get; set; }
        public double StepPrice { get; set; }
        public int Deposit { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool Status { get; set; }

        public virtual ItemType ItemType { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<Session> Sessions { get; set; }
    }
}
