using API.BusinessLogic.Interface.IRole;
using API.DataAccess.ORM.CodeFirst;
using API.RepositoryManagement.UnityOfWork.Interfaces;
using API.Settings;
using API.ViewModel.ViewModels.Common;
using API.ViewModel.ViewModels.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.BusinessLogic.Services.Roles
{
    public class RoleServiceQueries : IRoleQueries
    {
        private readonly IUnitOfWork _unitOfWork;
        public RoleServiceQueries(IUnitOfWork _unitOfWork)
        {
            this._unitOfWork = _unitOfWork;
        }
        #region Queries
        public async Task<object?> GetRoleList(vmRole role)
        {
            object list = new object(); int total = 0;
            CommonData common = new CommonData()
            {
                PageNumber = role.PageNumber,
                PageSize = role.PageSize,
            };
            try
            {
                var roleList = await _unitOfWork.RoleRepository.GetRoleList();
                total = (from r in roleList
                         where
                         string.IsNullOrEmpty(role.Search) ? true
                :
                (

                           (r.RoleName == null ? r.RoleName == null : r.RoleName.Contains(role.Search))
                          )
                         select r).ToList().Count();
                list = (from r in roleList
                        where
                        string.IsNullOrEmpty(role.Search) ? true
                :
                (

                           (r.RoleName == null ? r.RoleName == null : r.RoleName.Contains(role.Search))
                          )
                        select new
                        {
                            //Role
                            roleId = r.RoleId,
                            rolename = r.RoleName,
                            isActive = r.IsActive,
                            isDeleting = false,
                            total
                        }
                                ).OrderByDescending(x => x.roleId).Skip(Common.Skip(common)).Take(common.PageSize).ToList();

            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return new
            {
                list,
                total
            };
        }
        #endregion
    }
}
