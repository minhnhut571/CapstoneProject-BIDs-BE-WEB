using System;
using System.Collections.Generic;

#nullable disable

namespace Data_Access.Entities
{
    public partial class Payment
    {
        public Guid PaymentId { get; set; }
        public Guid UserId { get; set; }
        public Guid ItemId { get; set; }
        public string Detail { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public bool Status { get; set; }

        public virtual Item Item { get; set; }
        public virtual User User { get; set; }
    }
}
