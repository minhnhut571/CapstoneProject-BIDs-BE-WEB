using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic.Modules.UserModule.Response
{
    public class UserResponse
    {
        public string AccountName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string CCCDNumber { get; set; }
        public string CCCDFrontImage { get; set; }
        public string CCCDBackImage { get; set; }
    }
}
