using API.BusinessLogic.Interface.IRole;
using API.DataAccess.ORM.CodeFirst;
using API.RepositoryManagement.UnityOfWork.Interfaces;
using API.ViewModel.ViewModels.Roles;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.BusinessLogic.Services.Roles
{
    public class RoleServiceCommands : IRoleCommands
    {
        private readonly IUnitOfWork _unitOfWork; private readonly IMapper _mapper;
        public RoleServiceCommands(IUnitOfWork _unitOfWork, IMapper mapper)
        {
            this._unitOfWork = _unitOfWork;
            this._mapper = mapper;
        }
        #region Commands
        public async Task<object?> AddRole(vmRole role)
        {
            string message = string.Empty; bool resstate = false; 
            try
            {
                Role objRole = new Role
                {
                    RoleName = role.RoleName,
                    Sequence = role.Sequence,
                    IsActive=true
                };
                var objCreate = await _unitOfWork.RoleRepository.CreateRole(objRole);
                await _unitOfWork.CompleteAsync();

                message = "Created Successfully.";
                resstate = true;

            }
            catch (Exception ex)
            {
                message = "Failed."; resstate = false;
            }
            return new
            {
                message,
                isSuccess = resstate
            };
        }

        public async Task<object?> DeleteRole(int id)
        {
            string message = string.Empty; bool resstate = false; int total = 0;
                try
                {                    
                    resstate = await _unitOfWork.RoleRepository.DeleteRole(id);
                    await _unitOfWork.CompleteAsync();
                    message = "Deleted Successfully.";
                    total = (await _unitOfWork.RoleRepository.GetRoleList()).ToList().Count();
                }
                catch (Exception ex)
                {
                    message = "Failed."; resstate = false;
                }
                return new
                {
                    message,
                    isSuccess = resstate,
                    total
                };
            
        }

        public async Task<object?> UpdateRole(vmRole role)
        {
            string message = string.Empty; bool resstate = false;
            try
            {
                var objRole = await _unitOfWork.RoleRepository.GetRoleByID(role.RoleId);
                if (objRole != null)
                {
                    objRole.RoleName = role.RoleName;
                    await _unitOfWork.CompleteAsync();
                }
                message = "Updated Successfully.";
                resstate = true;
            }
            catch (Exception ex)
            {
                ex.ToString();
                message = "Failed.";
                resstate = false;
            }
            return new { message, isSuccess = resstate };
        }
        #endregion
    }
}
