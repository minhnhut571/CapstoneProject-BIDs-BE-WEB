using Business_Logic.Modules.ItemModule.Interface;
using Business_Logic.Modules.ItemModule.Request;
using Business_Logic.Modules.ItemTypeModule.Interface;
using Data_Access.Constant;
using Data_Access.Entities;
using FluentValidation.Results;

namespace Business_Logic.Modules.ItemModule
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _ItemRepository;
        private readonly IItemTypeService _ItemTypeService;
        public ItemService(IItemRepository ItemRepository, IItemTypeService ItemTypeService)
        {
            _ItemRepository = ItemRepository;
            _ItemTypeService = ItemTypeService;
        }

        public async Task<ICollection<Item>> GetAll()
        {
            return await _ItemRepository.GetAll(options: o => o.OrderByDescending(x => x.UpdateDate).ToList());
        }

        public Task<ICollection<Item>> GetItemsIsValid()
        {
            return _ItemRepository.GetItemsBy(x => x.Status == true, options: o => o.OrderByDescending(x => x.UpdateDate).ToList());
        }

        public async Task<Item> GetItemByID(Guid? id)
        {
            if (id == null)
            {
                throw new Exception(ErrorMessage.CommonError.ID_IS_NULL);
            }
            var Item = await _ItemRepository.GetFirstOrDefaultAsync(x => x.ItemId == id);
            if (Item == null)
            {
                throw new Exception(ErrorMessage.ItemError.ITEM_NOT_FOUND);
            }
            return Item;
        }

        public async Task<Item> GetItemByName(string ItemName)
        {
            if (ItemName == null)
            {
                throw new Exception(ErrorMessage.CommonError.NAME_IS_NULL);
            }
            var Item = await _ItemRepository.GetFirstOrDefaultAsync(x => x.ItemName == ItemName);
            if (Item == null)
            {
                throw new Exception(ErrorMessage.ItemError.ITEM_NOT_FOUND);
            }
            return Item;
        }

        public async Task<Item> GetItemByTypeName(string TypeName)
        {
            if (TypeName == null)
            {
                throw new Exception(ErrorMessage.CommonError.NAME_IS_NULL);
            }
            ItemType TypeCheck = await _ItemTypeService.GetItemTypeByName(TypeName);
            var Item = await _ItemRepository.GetFirstOrDefaultAsync(x => x.ItemTypeId == TypeCheck.ItemTypeId);
            if (Item == null)
            {
                throw new Exception(ErrorMessage.ItemError.ITEM_NOT_FOUND);
            }
            return Item;
        }

        public async Task<Guid?> AddNewItem(CreateItemRequest ItemRequest)
        {

            ValidationResult result = new CreateItemRequestValidator().Validate(ItemRequest);
            if (!result.IsValid)
            {
                throw new Exception(ErrorMessage.CommonError.INVALID_REQUEST);
            }

            Item ItemCheck = _ItemRepository.GetFirstOrDefaultAsync(x => x.ItemName == ItemRequest.ItemName).Result;

            if (ItemCheck != null)
            {
                throw new Exception(ErrorMessage.ItemError.ITEM_EXISTED);
            }

            var newItem = new Item();

            newItem.ItemId = new Guid();
            newItem.ItemName = ItemRequest.ItemName;
            newItem.Description = ItemRequest.Description;
            newItem.UserId = ItemRequest.UserId;
            newItem.Quantity = ItemRequest.Quantity;
            newItem.FristPrice = ItemRequest.FristPrice;
            newItem.StepPrice = ItemRequest.StepPrice;
            newItem.Image = ItemRequest.Image;
            newItem.ItemTypeId = ItemRequest.ItemTypeId;
            if(ItemRequest.FristPrice < 30000000 && ItemRequest.FristPrice >= 15000000)
            {
                newItem.Deposit = 15;
            }
            if(ItemRequest.FristPrice >= 30000000)
            {
                newItem.Deposit = 25;
            }
            newItem.UpdateDate = DateTime.Now;
            newItem.CreateDate = DateTime.Now;
            newItem.Status = false;

            await _ItemRepository.AddAsync(newItem);
            return newItem.ItemId;
        }

        public async Task UpdateItem(UpdateItemRequest ItemRequest)
        {
            try
            {
                var ItemUpdate = GetItemByID(ItemRequest.ItemId).Result;

                if (ItemUpdate == null)
                {
                    throw new Exception(ErrorMessage.ItemError.ITEM_NOT_FOUND);
                }

                ValidationResult result = new UpdateItemRequestValidator().Validate(ItemRequest);
                if (!result.IsValid)
                {
                    throw new Exception(ErrorMessage.CommonError.INVALID_REQUEST);
                }

                Item ItemCheck = _ItemRepository.GetFirstOrDefaultAsync(x => x.ItemName == ItemRequest.ItemName).Result;

                if (ItemCheck != null)
                {
                    throw new Exception(ErrorMessage.ItemError.ITEM_EXISTED);
                }
                ItemUpdate.ItemId = ItemRequest.ItemId;
                ItemUpdate.ItemName = ItemRequest.ItemName;
                ItemUpdate.Description = ItemRequest.Description;
                ItemUpdate.Quantity = ItemRequest.Quantity;
                ItemUpdate.FristPrice = ItemRequest.FristPrice;
                ItemUpdate.StepPrice = ItemRequest.StepPrice;
                ItemUpdate.Image = ItemRequest.Image;
                if (ItemRequest.FristPrice < 30000000 && ItemRequest.FristPrice >= 15000000)
                {
                    ItemUpdate.Deposit = 15;
                }
                if (ItemRequest.FristPrice >= 30000000)
                {
                    ItemUpdate.Deposit = 25;
                }
                ItemUpdate.UpdateDate = DateTime.Now;

                await _ItemRepository.UpdateAsync(ItemUpdate);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at update type: " + ex.Message);
                throw new Exception(ex.Message);
            }

        }

        public async Task DeleteItem(Guid? ItemDeleteID)
        {
            try
            {
                if (ItemDeleteID == null)
                {
                    throw new Exception(ErrorMessage.CommonError.ID_IS_NULL);
                }

                Item ItemDelete = _ItemRepository.GetFirstOrDefaultAsync(x => x.ItemId == ItemDeleteID && x.Status == true).Result;

                if (ItemDelete == null)
                {
                    throw new Exception(ErrorMessage.ItemError.ITEM_NOT_FOUND);
                }

                ItemDelete.Status = false;
                await _ItemRepository.UpdateAsync(ItemDelete);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at delete type: " + ex.Message);
                throw new Exception(ex.Message);
            }
        }

    }
}
