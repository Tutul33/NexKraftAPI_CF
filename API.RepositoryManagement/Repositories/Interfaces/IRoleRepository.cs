//using API.DataAccess.ORM.MsSQLDataModels;
using API.DataAccess.ORM.CodeFirst;
using API.ViewModel.ViewModels.Customers;
using API.ViewModel.ViewModels.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.RepositoryManagement.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        Task<List<Role>> GetRoleList();
        Task<Role?> GetRoleByID(int roleId);
        Task<bool> DeleteRole(int id);
        Task<Role> CreateRole(vmRole data);
    }
}
