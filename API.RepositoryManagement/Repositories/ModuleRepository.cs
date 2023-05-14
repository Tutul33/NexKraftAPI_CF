//using API.DataAccess.ORM.MsSQLDataModels;
using API.DataAccess.ORM.CodeFirst;
using API.RepositoryManagement.Base;
using API.RepositoryManagement.Repositories.Interfaces;
using API.Settings;
using API.ViewModel.ViewModels.Common;
using API.ViewModel.ViewModels.Customers;
using API.ViewModel.ViewModels.Menu;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.RepositoryManagement.Repositories
{
    public class ModuleRepository : BaseRepository<Module>, IModuleRepository
    {
        private NexKraftDbContextCF? NexKraftDbContext => _dbContext as NexKraftDbContextCF;
        public ModuleRepository(NexKraftDbContextCF dbContext) : base(dbContext)
        {
        }

        public async Task<Module?> GetModuleInfo(int moduleId)
        {
            return (await GetManyAsync(filter: u => u.ModuleId == moduleId)).FirstOrDefault();
        }

        public async Task<List<Module>> GetModuleList(vmModule param)
        {
            CommonData commonData = new CommonData()
            {
                PageNumber = param.PageNumber,
                PageSize = param.PageSize,
            };
            return (await GetManyAsync(
                filter: x => (
                !string.IsNullOrEmpty(param.Search) ? (x.ModuleName.Contains(param.Search) || x.Description.Contains(param.Search)) : true
                ),
                orderBy: x => x.OrderBy(t => t.ModuleId),
                top: param.PageSize,
                skip: Common.Skip(commonData)
                )).ToList();
        }

        public async Task<List<Module>> GetModuleList()
        {
            return (await GetAllAsync()).ToList();
        }

        public async Task<int> GetModuleTotal()
        {
            return (await GetAllAsync()).Count();
        }

        public async Task<bool> DeleteModule(int id)
        {
            return Convert.ToBoolean(DeleteAsync(await GetByIdAsync(id)).IsCompleted);
        }

        public async Task<Module> CreateModule(vmModule data)
        {
            Module obj = new Module()
            {
                ModuleName = data.ModuleName,
                ModulePath = data.ModulePath,
                ModuleSequence = data.ModuleSequence,
                ModuleColor = data.ModuleColor,
                ModuleIcon = data.ModuleIcon,
                Description = data.Description
            };
            return await AddAsync(obj);
        }
    }
}
