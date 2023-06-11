using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic.Modules.LoginModule.Data
{
    public class LoginAccount
    {
        public string AccountName { get; set; }
        public string Password { get; set; }
        public bool Remember { get; set; }
    }
}
