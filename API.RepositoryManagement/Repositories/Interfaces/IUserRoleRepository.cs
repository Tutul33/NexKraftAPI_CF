using API.DataAccess.ORM.CodeFirst;
using API.ViewModel.ViewModels.Customers;
using API.ViewModel.ViewModels.UserRole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.RepositoryManagement.Repositories.Interfaces
{
    public interface IUserRoleRepository
    {
        Task<UserRole> CreateUserRole(CreateUserRole data);
        Task<List<UserRole>> GetUserRoleList();
        Task<bool> DeleteUserRole(int id);
    }
}
