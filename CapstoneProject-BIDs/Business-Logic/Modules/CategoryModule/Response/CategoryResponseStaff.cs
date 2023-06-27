using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic.Modules.CategoryModule.Response
{
    public class CategoryResponseStaff
    {
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public DateTime UpdateDate { get; set; }
        public DateTime CreateDate { get; set; }
        public bool Status { get; set; }
    }
}
