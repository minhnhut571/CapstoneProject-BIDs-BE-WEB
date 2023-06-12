using Data_Access.Enum;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Entities
{
    public class BIDsContextSeed
    {
        public async Task SeedAsync(BIDsContext context, ILogger<BIDsContextSeed> logger)
        {
            // seed role
            if (!context.Roles.Any())
            {
                var role1 = new Role()
                {
                    RoleName = "Admin",
                    Status = true
                };
                var role2 = new Role()
                {
                    RoleName = "Staff",
                    Status = true
                };
                context.Roles.Add(role1);
                context.Roles.Add(role2);
            }

            // seed staff
            if (!context.Staffs.Any())
            {
                var admin = new Staff()
                {
                    StaffId = Guid.NewGuid(),
                    AccountName = "adminseed",
                    Address = "115/4/2 đường số 11 thành phố Thủ Đức",
                    CreateDate = DateTime.Now,
                    DateOfBirth = new DateTime(2001, 07, 05),
                    UpdateDate = DateTime.Now,
                    Email = "nhutdmse151298@fpt.edu.vn",
                    Password = "05072001",
                    Notification = "Admin mẫu",
                    Phone = "0933403842",
                    RoleId = 1,
                    StaffName = "Seed Admin",
                    Status = true
                };
                var staff = new Staff()
                {
                    StaffId = Guid.NewGuid(),
                    AccountName = "staff",
                    Address = "115/4/2 đường số 11 thành phố Thủ Đức",
                    CreateDate = DateTime.Now,
                    DateOfBirth = new DateTime(2001, 07, 05),
                    UpdateDate = DateTime.Now,
                    Email = "minhnhut05072001@email.com",
                    Password = "05072001",
                    Notification = "Staff mẫu",
                    Phone = "0933403843",
                    RoleId = 2,
                    StaffName = "Staff seed",
                    Status = true
                };
                context.Staffs.Add(admin);
                context.Staffs.Add(staff);
            }

            // seed user
            if (!context.Users.Any())
            {
                var user1 = new User()
                {
                    UserId = Guid.NewGuid(),
                    AccountName = "user1",
                    UserName = "User seed 1",
                    Address = "115/4/2 đường số 11 thành phố Thủ Đức",
                    CreateDate = DateTime.Now,
                    DateOfBirth = new DateTime(2003, 07, 07),
                    UpdateDate = DateTime.Now,
                    Email = "minhnhut05072001@email.com",
                    Cccdnumber = "077201000702",
                    CccdbackImage = "CCCD mặt sau mẫu",
                    CccdfrontImage = "CCCD mặt trước mẫu",
                    Password = "07072001",
                    Notification = "User mẫu 1",
                    Phone = "0933403844",
                    Status = (int)UserStatusEnum.Waitting,

                };
                var user2 = new User()
                {
                    UserId = Guid.NewGuid(),
                    AccountName = "user2",
                    UserName = "User seed 2",
                    Address = "115/4/2 đường số 11 thành phố Thủ Đức",
                    CreateDate = DateTime.Now,
                    DateOfBirth = new DateTime(2003, 08, 07),
                    UpdateDate = DateTime.Now,
                    Email = "minhnhut05072001@email.com",
                    Cccdnumber = "077201000703",
                    CccdbackImage = "CCCD mặt sau mẫu",
                    CccdfrontImage = "CCCD mặt trước mẫu",
                    Password = "07082001",
                    Notification = "User mẫu 2",
                    Phone = "0933403845",
                    Status = (int)UserStatusEnum.Acctive

                };
                var user3 = new User()
                {
                    UserId = Guid.NewGuid(),
                    AccountName = "user3",
                    UserName = "User seed 3",
                    Address = "115/4/2 đường số 11 thành phố Thủ Đức",
                    CreateDate = DateTime.Now,
                    DateOfBirth = new DateTime(2003, 09, 07),
                    UpdateDate = DateTime.Now,
                    Email = "minhnhut05072001@email.com",
                    Cccdnumber = "077201000704",
                    CccdbackImage = "CCCD mặt sau mẫu",
                    CccdfrontImage = "CCCD mặt trước mẫu",
                    Password = "07092001",
                    Notification = "User mẫu 3",
                    Phone = "0933403846",
                    Status = (int)UserStatusEnum.Deny

                };
                var user4 = new User()
                {
                    UserId = Guid.NewGuid(),
                    AccountName = "user4",
                    UserName = "User seed 4",
                    Address = "115/4/2 đường số 11 thành phố Thủ Đức",
                    CreateDate = DateTime.Now,
                    DateOfBirth = new DateTime(2003, 10, 07),
                    UpdateDate = DateTime.Now,
                    Email = "minhnhut05072001@email.com",
                    Cccdnumber = "077201000705",
                    CccdbackImage = "CCCD mặt sau mẫu",
                    CccdfrontImage = "CCCD mặt trước mẫu",
                    Password = "07102001",
                    Notification = "User mẫu 4",
                    Phone = "0933403847",
                    Status = (int)UserStatusEnum.Acctive

                };
                context.Users.Add(user1);
                context.Users.Add(user2);
                context.Users.Add(user3);
                context.Users.Add(user4);
            }

            // seed item type
            if (!context.ItemTypes.Any())
            {
                var itemType1 = new ItemType()
                {
                    ItemTypeId = Guid.NewGuid(),
                    ItemTypeName = "Đồ công nghệ",
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    Status = true
                };
                var itemType2 = new ItemType()
                {
                    ItemTypeId = Guid.NewGuid(),
                    ItemTypeName = "Xe máy, xe đạp",
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    Status = true
                };
                var itemType3 = new ItemType()
                {
                    ItemTypeId = Guid.NewGuid(),
                    ItemTypeName = "Đồ sưu tầm",
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    Status = true
                };
                var itemType4 = new ItemType()
                {
                    ItemTypeId = Guid.NewGuid(),
                    ItemTypeName = "Đồ cổ",
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    Status = false
                };
                context.ItemTypes.Add(itemType1);
                context.ItemTypes.Add(itemType2);
                context.ItemTypes.Add(itemType3);
                context.ItemTypes.Add(itemType4);
            }

            await context.SaveChangesAsync();
        }
    }
}
