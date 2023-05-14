using API.RepositoryManagement.Base;
using API.RepositoryManagement.Repositories.Interfaces;
//using API.DataAccess.ORM.MsSQLDataModels;
using API.DataAccess.ORM.CodeFirst;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.ViewModel.ViewModels.Common;
using API.ViewModel.ViewModels.Customers;
using API.Settings;
using API.ViewModel.ViewModels.UserRole;

namespace API.RepositoryManagement.Repositories
{
    public class UserRoleRepository : BaseRepository<UserRole>, IUserRoleRepository
    {
        private NexKraftDbContextCF? NexKraftDbContext => _dbContext as NexKraftDbContextCF;
        public UserRoleRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<UserRole>> GetUserRoleList()
        {
            return (await GetAllAsync()).ToList();
        }
        public async Task<UserRole?> GetUserRoleById(int userRoleId)
        {
            return await GetByIdAsync(userRoleId);
        }
        public async Task<UserRole?> GetUserRoleByLoginId(int loginId)
        {
            return (await GetManyAsync(filter: u => u.LoginId == loginId)).FirstOrDefault();
        }
        public async Task<bool> DeleteUserRole(int id)
        {
            return Convert.ToBoolean(DeleteAsync(await GetByIdAsync(id)).IsCompleted);
        }
        public async Task<UserRole> CreateUserRole(CreateUserRole data)
        {
            UserRole obj = new UserRole()
            {
                RoleId = data.RoleId,
                LoginId = data.LoginId,
            };
            return await AddAsync(obj);
        }
        

    }
}
