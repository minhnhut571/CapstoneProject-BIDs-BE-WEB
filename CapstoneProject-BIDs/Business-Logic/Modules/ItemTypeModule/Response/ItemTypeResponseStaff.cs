using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic.Modules.ItemTypeModule.Response
{
    public class ItemTypeResponseStaff
    {
        public Guid ItemTypeId { get; set; }
        public string ItemTypeName { get; set; }
        public DateTime UpdateDate { get; set; }
        public DateTime CreateDate { get; set; }
        public bool Status { get; set; }
    }
}
