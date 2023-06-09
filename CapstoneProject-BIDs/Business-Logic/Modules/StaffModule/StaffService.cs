using Business_Logic.Modules.StaffModule.Interface;
using Business_Logic.Modules.StaffModule.Request;
using Business_Logic.Modules.UserModule.Interface;
using Data_Access.Constant;
using Data_Access.Entities;
using FluentValidation.Results;
using System.Net.Mail;
using System.Net;
using static System.Net.WebRequestMethods;
using System.Text;
using Data_Access.Enum;

namespace Business_Logic.Modules.StaffModule
{
    public class StaffService : IStaffService
    {
        private readonly IStaffRepository _StaffRepository;
        private readonly IUserRepository _UserRepository;
        public StaffService(IStaffRepository StaffRepository, IUserRepository UserRepository)
        {
            _StaffRepository = StaffRepository;
            _UserRepository = UserRepository;
        }

        public async Task<ICollection<Staff>> GetAll()
        {
            return await _StaffRepository.GetAll(options: o => o.OrderByDescending(x => x.UpdateDate).ToList());
        }

        public Task<ICollection<Staff>> GetStaffsIsValid()
        {
            return _StaffRepository.GetStaffsBy(x => x.Status == true, options: o => o.OrderByDescending(x => x.UpdateDate).ToList());
        }

        public async Task<Staff> GetStaffByID(Guid? id)
        {
            if (id == null)
            {
                throw new Exception(ErrorMessage.CommonError.ID_IS_NULL);
            }
            var Staff = await _StaffRepository.GetFirstOrDefaultAsync(x => x.StaffId == id);
            if (Staff == null)
            {
                throw new Exception(ErrorMessage.StaffError.STAFF_NOT_FOUND);
            }
            return Staff;
        }

        public async Task<Staff> GetStaffByName(string StaffName)
        {
            if (StaffName == null)
            {
                throw new Exception(ErrorMessage.CommonError.NAME_IS_NULL);
            }
            var Staff = await _StaffRepository.GetFirstOrDefaultAsync(x => x.StaffName == StaffName);
            if (Staff == null)
            {
                throw new Exception(ErrorMessage.StaffError.STAFF_NOT_FOUND);
            }
            return Staff;
        }

        public async Task<Guid?> AddNewStaff(CreateStaffRequest StaffRequest)
        {

            ValidationResult result = new CreateStaffRequestValidator().Validate(StaffRequest);
            if (!result.IsValid)
            {
                throw new Exception(ErrorMessage.CommonError.INVALID_REQUEST);
            }

            Staff staffCheckAccountName = _StaffRepository.GetFirstOrDefaultAsync(x => x.AccountName == StaffRequest.AccountName).Result;
            if (staffCheckAccountName != null)
            {
                throw new Exception(ErrorMessage.CommonError.ACCOUNT_NAME_IS_EXITED);
            }
            Staff staffCheckEmail = _StaffRepository.GetFirstOrDefaultAsync(x => x.Email == StaffRequest.Email).Result;
            if (staffCheckEmail != null)
            {
                throw new Exception(ErrorMessage.CommonError.EMAIL_IS_EXITED);
            }
            Staff staffTestPhone = _StaffRepository.GetFirstOrDefaultAsync(x => x.Phone == StaffRequest.Phone).Result;
            if (staffTestPhone != null)
            {
                throw new Exception(ErrorMessage.CommonError.PHONE_IS_EXITED);
            }

            if (!StaffRequest.Email.Contains("@"))
            {
                throw new Exception(ErrorMessage.CommonError.WRONG_EMAIL_FORMAT);
            }
            if ((!StaffRequest.Phone.StartsWith("09") 
                && !StaffRequest.Phone.StartsWith("08")
                && !StaffRequest.Phone.StartsWith("07")
                && !StaffRequest.Phone.StartsWith("05")
                && !StaffRequest.Phone.StartsWith("03"))
                || StaffRequest.Phone.Length != 10)
            {
                throw new Exception(ErrorMessage.CommonError.WRONG_PHONE_FORMAT);
            }

            var newStaff = new Staff();

            newStaff.StaffId = Guid.NewGuid();
            newStaff.RoleId = StaffRequest.RoleId;
            newStaff.AccountName = StaffRequest.AccountName;
            newStaff.StaffName = StaffRequest.StaffName;
            newStaff.Email = StaffRequest.Email;
            newStaff.Password = StaffRequest.Password;
            newStaff.Address = StaffRequest.Address;
            newStaff.Phone = StaffRequest.Phone;
            newStaff.DateOfBirth = StaffRequest.DateOfBirth;
            newStaff.CreateDate = DateTime.Now;
            newStaff.UpdateDate = DateTime.Now;
            newStaff.Notification = "Khởi tạo thành công";         
            newStaff.Status = true;

            await _StaffRepository.AddAsync(newStaff);
            return newStaff.StaffId;
        }

        public async Task UpdateStaff(UpdateStaffRequest StaffRequest)
        {
            try
            {
                var StaffUpdate = GetStaffByID(StaffRequest.StaffId).Result;

                if (StaffUpdate == null)
                {
                    throw new Exception(ErrorMessage.StaffError.STAFF_NOT_FOUND);
                }

                ValidationResult result = new UpdateStaffRequestValidator().Validate(StaffRequest);
                if (!result.IsValid)
                {
                    throw new Exception(ErrorMessage.CommonError.INVALID_REQUEST);
                }

                Staff staffCheckEmail = _StaffRepository.GetFirstOrDefaultAsync(x => x.Email == StaffRequest.Email).Result;
                if (staffCheckEmail != null)
                {
                    throw new Exception(ErrorMessage.CommonError.EMAIL_IS_EXITED);
                }
                Staff staffCheckPhone = _StaffRepository.GetFirstOrDefaultAsync(x => x.Phone == StaffRequest.Phone).Result;
                if (staffCheckPhone != null)
                {
                    throw new Exception(ErrorMessage.CommonError.PHONE_IS_EXITED);
                }


                if (!StaffRequest.Email.Contains("@"))
                {
                    throw new Exception(ErrorMessage.CommonError.WRONG_EMAIL_FORMAT);
                }
                if ((!StaffRequest.Phone.StartsWith("09")
                    && !StaffRequest.Phone.StartsWith("08")
                    && !StaffRequest.Phone.StartsWith("07")
                    && !StaffRequest.Phone.StartsWith("05")
                    && !StaffRequest.Phone.StartsWith("03"))
                    || StaffRequest.Phone.Length != 10)
                {
                    throw new Exception(ErrorMessage.CommonError.WRONG_PHONE_FORMAT);
                }

                StaffUpdate.StaffName = StaffRequest.StaffName;
                StaffUpdate.Password = StaffRequest.Password;
                StaffUpdate.Email = StaffRequest.Email;
                StaffUpdate.Address = StaffRequest.Address;
                StaffUpdate.Phone = StaffRequest.Phone;
                StaffUpdate.UpdateDate = DateTime.Now;

                await _StaffRepository.UpdateAsync(StaffUpdate);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at update type: " + ex.Message);
                throw new Exception(ex.Message);
            }

        }

        public async Task DeleteStaff(Guid? StaffDeleteID)
        {
            try
            {
                if (StaffDeleteID == null)
                {
                    throw new Exception(ErrorMessage.CommonError.ID_IS_NULL);
                }

                Staff StaffDelete = _StaffRepository.GetFirstOrDefaultAsync(x => x.StaffId == StaffDeleteID && x.Status == true).Result;

                if (StaffDelete == null)
                {
                    throw new Exception(ErrorMessage.StaffError.STAFF_NOT_FOUND);
                }

                StaffDelete.Status = false;
                await _StaffRepository.UpdateAsync(StaffDelete);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at delete type: " + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task AcceptCreateAccount(Guid? accountCreateID)
        {
            try
            {
                if (accountCreateID == null)
                {
                    throw new Exception(ErrorMessage.CommonError.ID_IS_NULL);
                }

                User UserCreate = _UserRepository.GetFirstOrDefaultAsync(x => x.UserId == accountCreateID && x.Status == 0).Result;

                if (UserCreate == null)
                {
                    throw new Exception(ErrorMessage.UserError.ACCOUNT_CREATE_NOT_FOUND);
                }

                string _gmail = "bidauctionfloor@gmail.com";
                string _password = "gnauvhbfubtgxjow";

                string sendto = UserCreate.Email;
                string subject = "[BIDs] - Dịch vụ tài khoản";
                string content = "Tài khoản" + UserCreate.AccountName + " đã được khởi tạo thành công, chúc bạn có những phút giây sử dụng hệ thống vui vẻ";

                UserCreate.Status = (int)UserStatusEnum.Acctive;
                UserCreate.Notification = "Khởi tạo thành công, chào mừng đến với BIDs";
                await _UserRepository.UpdateAsync(UserCreate);

                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress(_gmail);
                mail.To.Add(UserCreate.Email);
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
                Console.WriteLine("Error at accept create account type: " + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task DenyCreate(Guid? accountCreateID)
        {
            try
            {
                if (accountCreateID == null)
                {
                    throw new Exception(ErrorMessage.CommonError.ID_IS_NULL);
                }

                User UserCreate = _UserRepository.GetFirstOrDefaultAsync(x => x.UserId == accountCreateID && x.Status == 0).Result;

                if (UserCreate == null)
                {
                    throw new Exception(ErrorMessage.UserError.ACCOUNT_CREATE_NOT_FOUND);
                }

                string _gmail = "bidauctionfloor@gmail.com";
                string _password = "gnauvhbfubtgxjow";

                string sendto = UserCreate.Email;
                string subject = "[BIDs] - Dịch vụ tài khoản";
                string content = "Tài khoản" + UserCreate.AccountName + " khởi tạo không thành công vì thông tin bạn cung cấp không chính xác!";

                UserCreate.Status = (int)UserStatusEnum.Deny;
                UserCreate.Notification = "Khởi tạo thất bại, thông tin không hợp lệ";
                await _UserRepository.UpdateAsync(UserCreate);

                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress(_gmail);
                mail.To.Add(UserCreate.Email);
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
                Console.WriteLine("Error at accept create account type: " + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task BanUser(Guid? UserBanID)
        {
            try
            {
                if (UserBanID == null)
                {
                    throw new Exception(ErrorMessage.CommonError.ID_IS_NULL);
                }

                var UserBan = _UserRepository.GetFirstOrDefaultAsync(x => x.UserId == UserBanID && x.Status == 0).Result;

                if (UserBan == null)
                {
                    throw new Exception(ErrorMessage.UserError.USER_NOT_FOUND);
                }

                string _gmail = "bidauctionfloor@gmail.com";
                string _password = "gnauvhbfubtgxjow";

                string sendto = UserBan.Email;
                string subject = "[BIDs] - Dịch vụ tài khoản";
                string content = "Tài khoản" + UserBan.AccountName + "đã bị khóa, bạn sẽ không thể sử dụng dịch vụ của hệ thống chúng tôi! ";

                UserBan.Status = (int)UserStatusEnum.Ban;
                await _UserRepository.UpdateAsync(UserBan);

                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress(_gmail);
                mail.To.Add(UserBan.Email);
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
                Console.WriteLine("Error at ban user type: " + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task UnbanUser(Guid? UserUnbanID)
        {
            try
            {
                if (UserUnbanID == null)
                {
                    throw new Exception(ErrorMessage.CommonError.ID_IS_NULL);
                }

                User UserUnban = _UserRepository.GetFirstOrDefaultAsync(x => x.UserId == UserUnbanID && x.Status == 0).Result;

                if (UserUnban == null)
                {
                    throw new Exception(ErrorMessage.UserError.USER_NOT_FOUND);
                }

                string _gmail = "bidauctionfloor@gmail.com";
                string _password = "gnauvhbfubtgxjow";

                string sendto = UserUnban.Email;
                string subject = "[BIDs] - Dịch vụ tài khoản";
                string content = "Tài khoản" + UserUnban.AccountName + "đã được mở khóa, mong bạn sẽ có những trải nghiệm tốt tại hệ thống. ";

                UserUnban.Status = (int)UserStatusEnum.Acctive;
                await _UserRepository.UpdateAsync(UserUnban);

                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress(_gmail);
                mail.To.Add(UserUnban.Email);
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
                Console.WriteLine("Error at ban user type: " + ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}
