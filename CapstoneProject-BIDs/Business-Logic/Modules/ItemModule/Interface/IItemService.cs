using Business_Logic.Modules.ItemModule.Request;
using Data_Access.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic.Modules.ItemModule.Interface
{
    public interface IItemService
    {
        public Task<Guid?> AddNewItem(CreateItemRequest ItemCreate);

        public Task UpdateItem(UpdateItemRequest ItemUpdate);

        public Task DeleteItem(Guid? ItemDeleteID);

        public Task<ICollection<Item>> GetAll();

        public Task<Item> GetItemByID(Guid? id);

        public Task<Item> GetItemByName(string Name);

        public Task<Item> GetItemByTypeName(string TypeName);

    }
}
