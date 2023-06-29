using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic.Modules.SessionDetailModule.Response
{
    public class SessionDetailResponse
    {
        public string UserName { get; set; }
        public string SessionName { get; set; }
        public double Price { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
