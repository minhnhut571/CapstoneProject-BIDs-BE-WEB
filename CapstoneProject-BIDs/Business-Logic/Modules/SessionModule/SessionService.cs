using Business_Logic.Modules.ItemTypeModule;
using Business_Logic.Modules.ItemTypeModule.Interface;
using Business_Logic.Modules.SessionModule.Interface;
using Business_Logic.Modules.SessionModule.Request;
using Business_Logic.Modules.UserModule.Interface;
using Data_Access.Constant;
using Data_Access.Entities;
using Data_Access.Enum;
using FluentValidation.Results;

namespace Business_Logic.Modules.SessionModule
{
    public class SessionService : ISessionService
    {
        private readonly ISessionRepository _SessionRepository;
        private readonly IItemTypeRepository _ItemTypeRepository;
        public SessionService(ISessionRepository SessionRepository, IItemTypeRepository ItemTypeRepository)
        {
            _SessionRepository = SessionRepository;
            _ItemTypeRepository = ItemTypeRepository;
        }

        public async Task<ICollection<Session>> GetAll()
        {
            return await _SessionRepository.GetAll(options: o => o.OrderByDescending(x => x.UpdateDate).ToList());
        }

        public async Task<ICollection<Session>> GetSessionsIsNotStart()
        {
            return await _SessionRepository.GetSessionsBy(x => x.Status == (int)SessionStatusEnum.NotStart, options: o => o.OrderByDescending(x => x.UpdateDate).ToList());
        }

        public async Task<ICollection<Session>> GetSessionsIsInStage()
        {
            return await _SessionRepository.GetSessionsBy(x => x.Status == (int)SessionStatusEnum.InStage, options: o => o.OrderByDescending(x => x.UpdateDate).ToList());
        }

        public async Task<ICollection<Session>> GetSessionsIsComplete()
        {
            return await _SessionRepository.GetSessionsBy(x => x.Status == (int)SessionStatusEnum.Complete, options: o => o.OrderByDescending(x => x.UpdateDate).ToList());
        }

        public async Task<Session> GetSessionByID(Guid? id)
        {
            if (id == null)
            {
                throw new Exception(ErrorMessage.CommonError.ID_IS_NULL);
            }
            var Session = await _SessionRepository.GetFirstOrDefaultAsync(x => x.SessionId == id);
            if (Session == null)
            {
                throw new Exception(ErrorMessage.SessionError.SESSION_NOT_FOUND);
            }
            return Session;
        }

        public async Task<Session> GetSessionByName(string SessionName)
        {
            if (SessionName == null)
            {
                throw new Exception(ErrorMessage.CommonError.NAME_IS_NULL);
            }
            var Session = await _SessionRepository.GetFirstOrDefaultAsync(x => x.SessionName == SessionName);
            if (Session == null)
            {
                throw new Exception(ErrorMessage.SessionError.SESSION_NOT_FOUND);
            }
            return Session;
        }

        //public async Task<Session> GetSessionByType(string itemType)
        //{
        //    if (itemType == null)
        //    {
        //        throw new Exception(ErrorMessage.CommonError.NAME_IS_NULL);
        //    }
        //    var type = _ItemTypeRepository.GetFirstOrDefaultAsync(x => x.ItemTypeName == itemType).Result;
        //    var Session = await _SessionRepository.GetFirstOrDefaultAsync(x => x.ItemTypeId == type.ItemTypeId);
        //    if (Session == null)
        //    {
        //        throw new Exception(ErrorMessage.SessionError.SESSION_NOT_FOUND);
        //    }
        //    return Session;
        //}

        public async Task<Guid?> AddNewSession(CreateSessionRequest SessionRequest)
        {

            ValidationResult result = new CreateSessionRequestValidator().Validate(SessionRequest);
            if (!result.IsValid)
            {
                throw new Exception(ErrorMessage.CommonError.INVALID_REQUEST);
            }

            Session SessionCheck = _SessionRepository.GetFirstOrDefaultAsync(x => x.SessionName == SessionRequest.SessionName).Result;

            if (SessionCheck != null)
            {
                throw new Exception(ErrorMessage.SessionError.SESSION_EXISTED);
            }

            var newSession = new Session();

            newSession.SessionId = Guid.NewGuid();
            newSession.SessionName = SessionRequest.SessionName;
            newSession.ItemId = SessionRequest.ItemId;
            newSession.BeginTime = SessionRequest.BeginTime;
            newSession.AuctionTime = SessionRequest.AuctionTime;
            newSession.EndTime = SessionRequest.EndTime;
            newSession.CreateDate = DateTime.Now;
            newSession.UpdateDate = DateTime.Now;
            newSession.Status = (int)SessionStatusEnum.NotStart;

            await _SessionRepository.AddAsync(newSession);
            return newSession.SessionId;
        }

        public async Task UpdateSession(UpdateSessionRequest SessionRequest)
        {
            try
            {
                var SessionUpdate = GetSessionByID(SessionRequest.SessionID).Result;

                if (SessionUpdate == null)
                {
                    throw new Exception(ErrorMessage.SessionError.SESSION_NOT_FOUND);
                }

                ValidationResult result = new UpdateSessionRequestValidator().Validate(SessionRequest);
                if (!result.IsValid)
                {
                    throw new Exception(ErrorMessage.CommonError.INVALID_REQUEST);
                }

                Session SessionCheck = _SessionRepository.GetFirstOrDefaultAsync(x => x.SessionName == SessionRequest.SessionName).Result;

                if (SessionCheck != null)
                {
                    throw new Exception(ErrorMessage.SessionError.SESSION_EXISTED);
                }

                SessionUpdate.SessionName = SessionRequest.SessionName;
                SessionUpdate.ItemId = SessionRequest.ItemId;
                SessionUpdate.BeginTime = SessionRequest.BeginTime;
                SessionUpdate.AuctionTime = SessionRequest.AuctionTime;
                SessionUpdate.EndTime = SessionRequest.EndTime;
                SessionUpdate.UpdateDate = DateTime.Now;
                SessionUpdate.Status = SessionRequest.Status;

                await _SessionRepository.UpdateAsync(SessionUpdate);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at update type: " + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteSession(Guid? SessionDeleteID)
        {
            try
            {
                if (SessionDeleteID == null)
                {
                    throw new Exception(ErrorMessage.CommonError.ID_IS_NULL);
                }

                Session SessionDelete = _SessionRepository.GetFirstOrDefaultAsync(x => x.SessionId == SessionDeleteID && x.Status != (int)SessionStatusEnum.Delete).Result;

                if (SessionDelete == null)
                {
                    throw new Exception(ErrorMessage.SessionError.SESSION_NOT_FOUND);
                }

                SessionDelete.Status = (int)SessionStatusEnum.Delete;
                await _SessionRepository.UpdateAsync(SessionDelete);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at delete type: " + ex.Message);
                throw new Exception(ex.Message);
            }
        }

    }
}
