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
            if(!context.Staffs.Any())
            {
                var staff = new Staff()
                {
                    StaffId = Guid.NewGuid(),
                    AccountName = "staff1",
                    Address = "this is home",
                    CreateDate = DateTime.Now,
                    DateOfBirth = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    Email = "email@email.com",
                    Password = "12345678",
                    Notification = "no noti",
                    Phone = "0951515151",
                    RoleId = 2,
                    StaffName = "staff 1",
                    Status = true
                };
                context.Staffs.Add(staff);
            }
            await context.SaveChangesAsync();
        }
    }
}
