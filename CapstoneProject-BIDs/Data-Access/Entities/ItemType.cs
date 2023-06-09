using System;
using System.Collections.Generic;

#nullable disable

namespace Data_Access.Entities
{
    public partial class ItemType
    {
        public ItemType()
        {
            Items = new HashSet<Item>();
        }

        public Guid ItemTypeId { get; set; }
        public string ItemTypeName { get; set; }
        public DateTime UpdateDate { get; set; }
        public DateTime CreateDate { get; set; }
        public bool Status { get; set; }

        public virtual ICollection<Item> Items { get; set; }
    }
}
