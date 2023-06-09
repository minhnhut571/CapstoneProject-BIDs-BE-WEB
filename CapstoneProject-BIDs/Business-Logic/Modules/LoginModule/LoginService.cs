using Azure.Messaging;
using Business_Logic.Modules.LoginModule.InterFace;
using Business_Logic.Modules.LoginModule.Request;
using Business_Logic.Modules.RoleModule.Interface;
using Business_Logic.Modules.StaffModule.Interface;
using Business_Logic.Modules.UserModule.Interface;
using Data_Access.Constant;
using Data_Access.Entities;
using FluentValidation.Results;
using System.Net.Mail;
using System.Net;
using System.Text;

namespace Business_Logic.Modules.LoginModule
{
    public class LoginService : ILoginService
    {
        private readonly IUserRepository _UserRepository;
        private readonly IStaffRepository _StaffRepository;
        private readonly IRoleRepository _RoleRepository;
        public LoginService(IUserRepository UserRepository, IStaffRepository StaffRepository, IRoleRepository RoleRepository)
        {
            _UserRepository = UserRepository;
            _StaffRepository = StaffRepository;
            _RoleRepository = RoleRepository;
        }
        public async Task<string> Login(LoginRequest loginRequest)
        {
            ValidationResult result = new LoginRequestValidator().Validate(loginRequest);
            if (!result.IsValid)
            {
                throw new Exception(ErrorMessage.CommonError.INVALID_REQUEST);
            }

            User UserCheckLogin =  _UserRepository.GetFirstOrDefaultAsync(x => x.AccountName == loginRequest.AccountName
            && x.Password == loginRequest.Password).Result;

            Staff StaffCheckLogin = _StaffRepository.GetFirstOrDefaultAsync(x => x.AccountName == loginRequest.AccountName
            && x.Password == loginRequest.Password).Result;

            if(UserCheckLogin == null && StaffCheckLogin == null)
            {
                throw new Exception(ErrorMessage.LoginError.WRONG_ACCOUNT_NAME_OR_PASSWORD);
            }

            if(UserCheckLogin != null)
            {
                return "Login successfull with user";
            }
            else
            {
                Role getRole =  _RoleRepository.GetFirstOrDefaultAsync(x => x.RoleId == StaffCheckLogin.RoleId).Result;
                return "Login successfull with role " + getRole.RoleName;
            }
        }

        public async Task ResetPassword(string email)
        {
            try
            {
                if (email == null)
                {
                    throw new Exception(ErrorMessage.CommonError.EMAIL_IS_NULL);
                }

                var userReset = _UserRepository.GetFirstOrDefaultAsync(x => x.Email == email).Result;

                var staffReset = _StaffRepository.GetFirstOrDefaultAsync(x => x.Email == email).Result;

                if (userReset == null && staffReset == null)
                {
                    throw new Exception(ErrorMessage.UserError.USER_NOT_FOUND);
                }


                string _gmail = "bidauctionfloor@gmail.com";
                string _password = "gnauvhbfubtgxjow";
                string randomPassword = RandomString(10);

                string sendto = email;
                string subject = "[BIDs] - Khôi phục mật khẩu";

                string content = "";

                if(userReset != null)
                {
                    content = "Mật khẩu mới cho tài khoản đăng nhập " + userReset.AccountName + " : " + randomPassword + ".\r\nĐường link quay lại trang đăng nhập: ";
                    userReset.Password = randomPassword;
                    await _UserRepository.UpdateAsync(userReset);
                }
                else
                {
                    content = "Mật khẩu mới cho tài khoản đăng nhập " + staffReset.AccountName + " : " + randomPassword + ".\r\nĐường link quay lại trang đăng nhập: ";
                    staffReset.Password = randomPassword;
                    await _StaffRepository.UpdateAsync(staffReset);
                }

                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress(_gmail);
                mail.To.Add(email);
                mail.Subject = subject;
                mail.IsBodyHtml = true;
                mail.Body = content;

                mail.Priority = MailPriority.High;

                SmtpServer.Port = 587;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new NetworkCredential(_gmail, _password);
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at delete type: " + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        private string RandomString(int size)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            return builder.ToString();
        }

        public async Task sendemail(string email)
        {
            string _gmail = "bidauctionfloor@gmail.com";
            string _password = "gnauvhbfubtgxjow";

            string sendto = email;
            string subject = "[BIDs] - Khôi phục mật khẩu";

            string content = "abc";

            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            mail.From = new MailAddress(_gmail);
            mail.To.Add(email);
            mail.Subject = subject;
            mail.IsBodyHtml = true;
            mail.Body = content;

            mail.Priority = MailPriority.High;

            SmtpServer.Port = 587;
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Credentials = new NetworkCredential(_gmail, _password);
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
        }
    }
}
