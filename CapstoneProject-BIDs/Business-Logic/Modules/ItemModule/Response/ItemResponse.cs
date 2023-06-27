using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic.Modules.ItemModule.Response
{
    public class ItemResponse
    {
        public string UserName { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public string DescriptionDetail { get; set; }
        public int Quantity { get; set; }
        public string Image { get; set; }
        public double FristPrice { get; set; }
        public double StepPrice { get; set; }
        public bool Deposit { get; set; }
    }
}
