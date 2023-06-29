using Business_Logic.Modules.LoginModule.Data;
using Business_Logic.Modules.LoginModule.Request;
using Data_Access.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic.Modules.LoginModule.InterFace
{
    public interface ILoginService
    {
        //public Task<string> Login(LoginRequest loginRequest);
        //public bool CheckLogin(Login login);
        //public Task<bool> Login(Login login);
        //public Task<TokenModel> GenerateToken(LoginRespone account);
        //public ClaimsPrincipal EncrypToken(string token);
        public User LoginUser(LoginRequest loginRequest);
        public string LoginStaff(LoginRequest loginRequest);
        public ClaimsPrincipal EncrypToken(string Token);
        public Task ResetPassword(string email);
        public Task sendemail(string email);
    }
}
