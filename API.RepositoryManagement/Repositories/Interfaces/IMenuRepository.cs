//using API.DataAccess.ORM.MsSQLDataModels;
using API.DataAccess.ORM.CodeFirst;
using API.ViewModel.ViewModels.Menu;
namespace API.RepositoryManagement.Repositories.Interfaces
{
    public interface IMenuRepository
    {
        Task<Menu?> GetMenuInfo(int customerId);
        Task<List<Menu>> GetMenuList(vmMenu param);
        Task<List<Menu>> GetMenuList();
        Task<int> GetMenuTotal();
        Task<bool> DeleteMenu(int id);
        Task<Menu> CreateMenu(vmMenu data);
    }
}
