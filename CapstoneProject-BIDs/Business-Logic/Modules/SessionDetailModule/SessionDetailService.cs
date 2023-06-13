using Business_Logic.Modules.ItemTypeModule;
using Business_Logic.Modules.ItemTypeModule.Interface;
using Business_Logic.Modules.SessionDetailModule.Interface;
using Business_Logic.Modules.SessionDetailModule.Request;
using Business_Logic.Modules.UserModule.Interface;
using Data_Access.Constant;
using Data_Access.Entities;
using Data_Access.Enum;
using FluentValidation.Results;

namespace Business_Logic.Modules.SessionDetailModule
{
    public class SessionDetailService : ISessionDetailService
    {
        private readonly ISessionDetailRepository _SessionDetailRepository;
        private readonly IItemTypeRepository _ItemTypeRepository;
        public SessionDetailService(ISessionDetailRepository SessionDetailRepository, IItemTypeRepository ItemTypeRepository)
        {
            _SessionDetailRepository = SessionDetailRepository;
            _ItemTypeRepository = ItemTypeRepository;
        }

        public async Task<ICollection<SessionDetail>> GetAll()
        {
            return await _SessionDetailRepository.GetAll();
        }

        public async Task<ICollection<SessionDetail>> GetSessionDetailIsActive()
        {
            return await _SessionDetailRepository.GetSessionDetailsBy(x => x.Status == true);
        }

        public async Task<ICollection<SessionDetail>> GetSessionDetailsIsInActive()
        {
            return await _SessionDetailRepository.GetSessionDetailsBy(x => x.Status == false);
        }

        public async Task<SessionDetail> GetSessionDetailByID(Guid? id)
        {
            if (id == null)
            {
                throw new Exception(ErrorMessage.CommonError.ID_IS_NULL);
            }
            var SessionDetail = await _SessionDetailRepository.GetFirstOrDefaultAsync(x => x.SessionId == id);
            if (SessionDetail == null)
            {
                throw new Exception(ErrorMessage.AuctionHistoryError.AUCTION_HISTORY_NOT_FOUND);
            }
            return SessionDetail;
        }

        public async Task<ICollection<SessionDetail>> GetSessionDetailByUser(Guid? id)
        {
            if (id == null)
            {
                throw new Exception(ErrorMessage.CommonError.ID_IS_NULL);
            }
            var SessionDetail = await _SessionDetailRepository.GetSessionDetailsBy(x => x.UserId == id);
            if (SessionDetail == null)
            {
                throw new Exception(ErrorMessage.AuctionHistoryError.AUCTION_HISTORY_NOT_FOUND);
            }
            return SessionDetail;
        }

        public async Task<ICollection<SessionDetail>> GetSessionDetailBySession(Guid? id)
        {
            if (id == null)
            {
                throw new Exception(ErrorMessage.CommonError.NAME_IS_NULL);
            }
            var SessionDetail = await _SessionDetailRepository.GetSessionDetailsBy(x => x.SessionId == id);
            if (SessionDetail == null)
            {
                throw new Exception(ErrorMessage.AuctionHistoryError.AUCTION_HISTORY_NOT_FOUND);
            }
            return SessionDetail;
        }

        //public async Task<SessionDetail> GetSessionDetailByType(string itemType)
        //{
        //    if (itemType == null)
        //    {
        //        throw new Exception(ErrorMessage.CommonError.NAME_IS_NULL);
        //    }
        //    var type = _ItemTypeRepository.GetFirstOrDefaultAsync(x => x.ItemTypeName == itemType).Result;
        //    var SessionDetail = await _SessionDetailRepository.GetFirstOrDefaultAsync(x => x.ItemTypeId == type.ItemTypeId);
        //    if (SessionDetail == null)
        //    {
        //        throw new Exception(ErrorMessage.SessionDetailError.SessionDetail_NOT_FOUND);
        //    }
        //    return SessionDetail;
        //}

        public async Task<SessionDetail> AddNewSessionDetail(CreateSessionDetailRequest SessionDetailRequest)
        {

            ValidationResult result = new CreateSessionDetailRequestValidator().Validate(SessionDetailRequest);
            if (!result.IsValid)
            {
                throw new Exception(ErrorMessage.CommonError.INVALID_REQUEST);
            }


            var newSessionDetail = new SessionDetail();

            newSessionDetail.SessionDetailId = Guid.NewGuid();
            newSessionDetail.UserId = SessionDetailRequest.UserId;
            newSessionDetail.SessionId = SessionDetailRequest.SessionId;
            newSessionDetail.Price = SessionDetailRequest.Price;
            newSessionDetail.CreateDate = DateTime.Now;
            newSessionDetail.Status = true;

            await _SessionDetailRepository.AddAsync(newSessionDetail);
            return newSessionDetail;
        }

        public async Task<SessionDetail> UpdateSessionDetail(UpdateSessionDetailRequest SessionDetailRequest)
        {
            try
            {
                var SessionDetailUpdate = GetSessionDetailByID(SessionDetailRequest.SessionDetailId).Result;

                if (SessionDetailUpdate == null)
                {
                    throw new Exception(ErrorMessage.AuctionHistoryError.AUCTION_HISTORY_NOT_FOUND);
                }

                ValidationResult result = new UpdateSessionDetailRequestValidator().Validate(SessionDetailRequest);
                if (!result.IsValid)
                {
                    throw new Exception(ErrorMessage.CommonError.INVALID_REQUEST);
                }

                SessionDetailUpdate.Status = SessionDetailRequest.Status;

                await _SessionDetailRepository.UpdateAsync(SessionDetailUpdate);
                return SessionDetailUpdate;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at update type: " + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<SessionDetail> DeleteSessionDetail(Guid? SessionDetailDeleteID)
        {
            try
            {
                if (SessionDetailDeleteID == null)
                {
                    throw new Exception(ErrorMessage.CommonError.ID_IS_NULL);
                }

                SessionDetail SessionDetailDelete = _SessionDetailRepository.GetFirstOrDefaultAsync(x => x.SessionDetailId == SessionDetailDeleteID && x.Status == true).Result;

                if (SessionDetailDelete == null)
                {
                    throw new Exception(ErrorMessage.AuctionHistoryError.AUCTION_HISTORY_NOT_FOUND);
                }

                SessionDetailDelete.Status = false;
                await _SessionDetailRepository.UpdateAsync(SessionDetailDelete);
                return SessionDetailDelete;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at delete type: " + ex.Message);
                throw new Exception(ex.Message);
            }
        }

    }
}
