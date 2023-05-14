//using API.DataAccess.ORM.MsSQLDataModels;
using API.DataAccess.ORM.CodeFirst;
using API.ViewModel.ViewModels.Menu;

namespace API.RepositoryManagement.Repositories.Interfaces
{
    public interface IMenuPermissionRepository
    {
        Task<MenuPermission> SetMenuPermission(vmMenuPermission data);
        Task<List<MenuPermission>> GetMenuPermissionList();
        Task<bool> DeleteMenuPermission(int id);
        Task<MenuPermission?> GetMenuPermissionByPermissionId(int permissionId);
    }
}
