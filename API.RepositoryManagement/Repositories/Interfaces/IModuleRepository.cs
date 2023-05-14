//using API.DataAccess.ORM.MsSQLDataModels;
using API.DataAccess.ORM.CodeFirst;
using API.ViewModel.ViewModels.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.RepositoryManagement.Repositories.Interfaces
{
    public interface IModuleRepository
    {
        Task<Module?> GetModuleInfo(int customerId);
        Task<List<Module>> GetModuleList(vmModule param);
        Task<List<Module>> GetModuleList();
        Task<int> GetModuleTotal();
        Task<bool> DeleteModule(int id);
        Task<Module> CreateModule(vmModule data);
    }
}
