using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Data_Access.Entities
{
    public partial class Session
    {
        public Session()
        {
            SessionDetails = new HashSet<SessionDetail>();
        }

        public Guid SessionId { get; set; }
        [Required]
        public Guid? ItemId { get; set; }
        public string SessionName { get; set; }
        public DateTime BeginTime { get; set; }
        public DateTime AuctionTime { get; set; }
        public DateTime EndTime { get; set; }
        public double? FinailPrice { get; set; }
        public DateTime UpdateDate { get; set; }
        public DateTime CreateDate { get; set; }
        public int Status { get; set; }

        public virtual Item Item { get; set; }
        public virtual ICollection<SessionDetail> SessionDetails { get; set; }
    }
}
