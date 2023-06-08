using Business_Logic.Modules.ItemTypeModule.Interface;
using Business_Logic.Modules.ItemTypeModule.Request;
using Business_Logic.Modules.UserModule.Interface;
using Data_Access.Constant;
using Data_Access.Entities;
using FluentValidation.Results;

namespace Business_Logic.Modules.ItemTypeModule
{
    public class ItemTypeService : IItemTypeService
    {
        private readonly IItemTypeRepository _ItemTypeRepository;
        public ItemTypeService(IItemTypeRepository ItemTypeRepository)
        {
            _ItemTypeRepository = ItemTypeRepository;
        }

        public async Task<ICollection<ItemType>> GetAll()
        {
            return await _ItemTypeRepository.GetAll(options: o => o.OrderByDescending(x => x.UpdateDate).ToList());
        }

        public Task<ICollection<ItemType>> GetItemTypesIsValid()
        {
            return _ItemTypeRepository.GetItemTypesBy(x => x.Status == true, options: o => o.OrderByDescending(x => x.UpdateDate).ToList());
        }

        public async Task<ItemType> GetItemTypeByID(Guid? id)
        {
            if (id == null)
            {
                throw new Exception(ErrorMessage.CommonError.ID_IS_NULL);
            }
            var ItemType = await _ItemTypeRepository.GetFirstOrDefaultAsync(x => x.ItemTypeId == id);
            if (ItemType == null)
            {
                throw new Exception(ErrorMessage.ItemTypeError.ITEM_TYPE_NOT_FOUND);
            }
            return ItemType;
        }

        public async Task<ItemType> GetItemTypeByName(string ItemTypeName)
        {
            if (ItemTypeName == null)
            {
                throw new Exception(ErrorMessage.CommonError.NAME_IS_NULL);
            }
            var ItemType = await _ItemTypeRepository.GetFirstOrDefaultAsync(x => x.ItemTypeName == ItemTypeName);
            if (ItemType == null)
            {
                throw new Exception(ErrorMessage.ItemTypeError.ITEM_TYPE_NOT_FOUND);
            }
            return ItemType;
        }

        public async Task<Guid?> AddNewItemType(CreateItemTypeRequest ItemTypeRequest)
        {

            ValidationResult result = new CreateItemTypeRequestValidator().Validate(ItemTypeRequest);
            if (!result.IsValid)
            {
                throw new Exception(ErrorMessage.CommonError.INVALID_REQUEST);
            }

            ItemType ItemTypeCheck = _ItemTypeRepository.GetFirstOrDefaultAsync(x => x.ItemTypeName == ItemTypeRequest.ItemTypeName).Result;

            if (ItemTypeCheck != null)
            {
                throw new Exception(ErrorMessage.ItemTypeError.ITEM_TYPE_EXISTED);
            }

            var newItemType = new ItemType();

            newItemType.ItemTypeId = Guid.NewGuid();
            newItemType.ItemTypeName = ItemTypeRequest.ItemTypeName;
            newItemType.Status = true;

            await _ItemTypeRepository.AddAsync(newItemType);
            return newItemType.ItemTypeId;
        }

        public async Task UpdateItemType(UpdateItemTypeRequest ItemTypeRequest)
        {
            try
            {
                var ItemTypeUpdate = GetItemTypeByID(ItemTypeRequest.ItemTypeId).Result;

                if (ItemTypeUpdate == null)
                {
                    throw new Exception(ErrorMessage.ItemTypeError.ITEM_TYPE_NOT_FOUND);
                }

                ValidationResult result = new UpdateItemTypeRequestValidator().Validate(ItemTypeRequest);
                if (!result.IsValid)
                {
                    throw new Exception(ErrorMessage.CommonError.INVALID_REQUEST);
                }

                ItemType ItemTypeCheck = _ItemTypeRepository.GetFirstOrDefaultAsync(x => x.ItemTypeName == ItemTypeRequest.ItemTypeName).Result;

                if (ItemTypeCheck != null)
                {
                    throw new Exception(ErrorMessage.ItemTypeError.ITEM_TYPE_EXISTED);
                }

                ItemTypeUpdate.ItemTypeName = ItemTypeRequest.ItemTypeName;
                ItemTypeUpdate.Status = ItemTypeRequest.Status;

                await _ItemTypeRepository.UpdateAsync(ItemTypeUpdate);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at update type: " + ex.Message);
                throw new Exception(ex.Message);
            }

        }

        public async Task DeleteItemType(Guid? ItemTypeDeleteID)
        {
            try
            {
                if (ItemTypeDeleteID == null)
                {
                    throw new Exception(ErrorMessage.CommonError.ID_IS_NULL);
                }

                ItemType ItemTypeDelete = _ItemTypeRepository.GetFirstOrDefaultAsync(x => x.ItemTypeId == ItemTypeDeleteID && x.Status == true).Result;

                if (ItemTypeDelete == null)
                {
                    throw new Exception(ErrorMessage.ItemTypeError.ITEM_TYPE_NOT_FOUND);
                }

                ItemTypeDelete.Status = false;
                await _ItemTypeRepository.UpdateAsync(ItemTypeDelete);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at delete type: " + ex.Message);
                throw new Exception(ex.Message);
            }
        }

    }
}
