using Business_Logic.Modules.RoleModule.Interface;
using Business_Logic.Modules.RoleModule.Request;
using Business_Logic.Modules.UserModule.Interface;
using Data_Access.Constant;
using Data_Access.Entities;
using FluentValidation.Results;

namespace Business_Logic.Modules.RoleModule
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _RoleRepository;
        public RoleService(IRoleRepository RoleRepository)
        {
            _RoleRepository = RoleRepository;
        }

        public async Task<ICollection<Role>> GetAll()
        {
            return await _RoleRepository.GetAll();
        }

        public Task<ICollection<Role>> GetRolesIsValid()
        {
            return _RoleRepository.GetRolesBy(x => x.Status == true);
        }

        public async Task<Role> GetRoleByID(int id)
        {
            if (id == null)
            {
                throw new Exception(ErrorMessage.CommonError.ID_IS_NULL);
            }
            var Role = await _RoleRepository.GetFirstOrDefaultAsync(x => x.RoleId == id);
            if (Role == null)
            {
                throw new Exception(ErrorMessage.RoleError.ROLE_NOT_FOUND);
            }
            return Role;
        }

        public async Task<Role> GetRoleByName(string RoleName)
        {
            if (RoleName == null)
            {
                throw new Exception(ErrorMessage.CommonError.NAME_IS_NULL);
            }
            var Role = await _RoleRepository.GetFirstOrDefaultAsync(x => x.RoleName == RoleName);
            if (Role == null)
            {
                throw new Exception(ErrorMessage.RoleError.ROLE_NOT_FOUND);
            }
            return Role;
        }

        public async Task<Role> AddNewRole(CreateRoleRequest RoleRequest)
        {

            ValidationResult result = new CreateRoleRequestValidator().Validate(RoleRequest);
            if (!result.IsValid)
            {
                throw new Exception(ErrorMessage.CommonError.INVALID_REQUEST);
            }

            Role roleCheck = _RoleRepository.GetFirstOrDefaultAsync(x => x.RoleName == RoleRequest.RoleName).Result;

            if (roleCheck != null)
            {
                throw new Exception(ErrorMessage.RoleError.ROLE_EXISTED);
            }

            var newRole = new Role();

            newRole.RoleName = RoleRequest.RoleName;
            newRole.Status = true;

            await _RoleRepository.AddAsync(newRole);
            return newRole;
        }

        public async Task<Role> UpdateRole(UpdateRoleRequest RoleRequest)
        {
            try
            {
                var RoleUpdate = GetRoleByID(RoleRequest.RoleId).Result;

                if (RoleUpdate == null)
                {
                    throw new Exception(ErrorMessage.RoleError.ROLE_NOT_FOUND);
                }

                ValidationResult result = new UpdateRoleRequestValidator().Validate(RoleRequest);
                if (!result.IsValid)
                {
                    throw new Exception(ErrorMessage.CommonError.INVALID_REQUEST);
                }

                Role roleCheck = _RoleRepository.GetFirstOrDefaultAsync(x => x.RoleName == RoleRequest.RoleName).Result;

                if (roleCheck != null)
                {
                    throw new Exception(ErrorMessage.RoleError.ROLE_EXISTED);
                }

                RoleUpdate.RoleName = RoleRequest.RoleName;
                RoleUpdate.Status = RoleRequest.Status;

                await _RoleRepository.UpdateAsync(RoleUpdate);
                return RoleUpdate;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at update type: " + ex.Message);
                throw new Exception(ex.Message);
            }

        }

        public async Task<Role> DeleteRole(int RoleDeleteID)
        {
            try
            {
                if (RoleDeleteID <= 0)
                {
                    throw new Exception(ErrorMessage.CommonError.ID_IS_NULL);
                }

                Role RoleDelete = _RoleRepository.GetFirstOrDefaultAsync(x => x.RoleId == RoleDeleteID && x.Status == true).Result;

                if (RoleDelete == null)
                {
                    throw new Exception(ErrorMessage.RoleError.ROLE_NOT_FOUND);
                }

                RoleDelete.Status = false;
                await _RoleRepository.UpdateAsync(RoleDelete);
                return RoleDelete;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at delete type: " + ex.Message);
                throw new Exception(ex.Message);
            }
        }

    }
}
