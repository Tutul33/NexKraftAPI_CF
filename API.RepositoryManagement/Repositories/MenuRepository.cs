//using API.DataAccess.ORM.MsSQLDataModels;
using API.DataAccess.ORM.CodeFirst;
using API.RepositoryManagement.Base;
using API.RepositoryManagement.Repositories.Interfaces;
using API.Settings;
using API.ViewModel.ViewModels.Common;
using API.ViewModel.ViewModels.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace API.RepositoryManagement.Repositories
{
    public class MenuRepository :BaseRepository<Menu>, IMenuRepository
    {
        private NexKraftDbContextCF? NexKraftDbContext => _dbContext as NexKraftDbContextCF;
        public MenuRepository(NexKraftDbContextCF dbContext) : base(dbContext)
        {

        }
        public async Task<Menu> CreateMenu(vmMenu data)
        {
            Menu obj = new Menu()
            {
              MenuName= data.MenuName,
              MenuIcon= data.MenuIcon,
              MenuPath= data.MenuPath,
              MenuSequence= data.MenuSequence,
              ModuleId= data.ModuleId,
              ParentId= data.ParentId,
              SubParentId= data.SubParentId,
              IsSubParent= data.IsSubParent,
              IsActive= data.IsActive
            };
            return await AddAsync(obj);
        }

        public async Task<bool> DeleteMenu(int id)
        {
            return Convert.ToBoolean(DeleteAsync(await GetByIdAsync(id)).IsCompleted);
        }

        public async Task<Menu?> GetMenuInfo(int menuId)
        {
            return (await GetManyAsync(filter: u => u.MenuId == menuId)).FirstOrDefault();
        }

        public async Task<List<Menu>> GetMenuList(vmMenu param)
        {
            //CommonData commonData = new CommonData()
            //{
            //    PageNumber = param.PageNumber,
            //    PageSize = param.PageSize,
            //};
            //return (await GetManyAsync(
            //    filter: x => (
            //    !string.IsNullOrEmpty(param.Search) ? x.MenuName.Contains(param.Search): true
            //    ),
            //    orderBy: x => x.OrderBy(t => t.ModuleId),
            //    top: param.PageSize,
            //    skip: Common.Skip(commonData)
            //    )).ToList();
            return new List<Menu>();
        }

        public async Task<List<Menu>> GetMenuList()
        {
            return (await GetAllAsync()).ToList();
        }

        public async Task<int> GetMenuTotal()
        {
            return (await GetAllAsync()).Count();
        }
    }
}
