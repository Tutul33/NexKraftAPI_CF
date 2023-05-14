using API.DataAccess.ORM.CodeFirst;
//using API.DataAccess.ORM.MsSQLDataModels;
using API.RepositoryManagement.Base;
using API.RepositoryManagement.Repositories.Interfaces;
using API.ViewModel.ViewModels.Menu;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.RepositoryManagement.Repositories
{
    public class MenuPermissionRepository : BaseRepository<MenuPermission>, IMenuPermissionRepository
    {
        private NexKraftDbContextCF? NexKraftDbContext => _dbContext as NexKraftDbContextCF;
        public MenuPermissionRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public async Task<MenuPermission> SetMenuPermission(vmMenuPermission data)
        {
            MenuPermission obj = new MenuPermission()
            {
                MenuId= data.MenuId,
                CanCreate= data.CanCreate,
                CanDelete  = data.CanDelete,
                CanEdit= data.CanEdit,
                CanView= data.CanView,
                UserId= data.UserId,
                RoleId= data.RoleId,
                IsActive= data.IsActive
            };
            return await AddAsync(obj);
        }
        public async Task<List<MenuPermission>> GetMenuPermissionList()
        {
            return (await GetAllAsync()).ToList();
        }

        public async Task<bool> DeleteMenuPermission(int id)
        {
            return Convert.ToBoolean(DeleteAsync(await GetByIdAsync(id)).IsCompleted);
        }
        public async Task<MenuPermission?> GetMenuPermissionByPermissionId(int permissionId)
        {
            return (await GetManyAsync(filter: u => u.PermissionId == permissionId)).FirstOrDefault();
        }
    }
}
