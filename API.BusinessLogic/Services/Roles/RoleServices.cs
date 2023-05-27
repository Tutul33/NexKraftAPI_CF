using API.BusinessLogic.Interface.IRole;
using API.ViewModel.ViewModels.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.BusinessLogic.Services.Roles
{
    public class RoleServices : IRoleCommands,IRoleQueries
    {
        #region Queries
        public Task<object?> GetRoleList(vmRole search)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region Commands
        public Task<object?> AddRole(vmRole role)
        {
            throw new NotImplementedException();
        }

        public Task<object?> DeleteRole(int id)
        {
            throw new NotImplementedException();
        }

        public Task<object?> UpdateRole(vmRole role)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
