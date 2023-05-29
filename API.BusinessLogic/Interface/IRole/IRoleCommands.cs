using API.ViewModel.ViewModels.Menu;
using API.ViewModel.ViewModels.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.BusinessLogic.Interface.IRole
{
    public interface IRoleCommands
    {
        Task<object?> DeleteRole(int id);
        Task<object?> AddRole(vmRole role);
        Task<object?> UpdateRole(vmRole role);
    }
}
