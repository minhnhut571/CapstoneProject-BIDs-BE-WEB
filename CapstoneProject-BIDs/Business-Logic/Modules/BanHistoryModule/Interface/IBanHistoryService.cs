using Business_Logic.Modules.BanHistoryModule.Request;
using Data_Access.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic.Modules.BanHistoryModule.Interface
{
    public interface IBanHistoryService
    {
        public Task<Guid?> AddNewBanHistory(CreateBanHistoryRequest BanHistoryCreate);

        public Task UpdateBanHistory(UpdateBanHistoryRequest BanHistoryUpdate);

        //public Task DeleteBanHistory(Guid? BanHistoryDeleteID);

        public Task<ICollection<BanHistory>> GetAll();

        public Task<BanHistory> GetBanHistoryByUserID(Guid? id);

        public Task<BanHistory> GetBanHistoryByUserName(string Name);

    }
}
