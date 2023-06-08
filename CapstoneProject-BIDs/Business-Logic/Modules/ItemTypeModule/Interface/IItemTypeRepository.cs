using Common.Utils.Repository.Interface;
using Data_Access.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic.Modules.ItemTypeModule.Interface
{
    public interface IItemTypeRepository : IRepository<ItemType>
    {
        public Task<ICollection<ItemType>> GetItemTypesBy(
               Expression<Func<ItemType, bool>> filter = null,
               Func<IQueryable<ItemType>, ICollection<ItemType>> options = null,
               string includeProperties = null
           );
    }
}
