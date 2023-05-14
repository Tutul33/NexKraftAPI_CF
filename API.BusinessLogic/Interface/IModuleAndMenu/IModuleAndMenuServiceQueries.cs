using API.ViewModel.ViewModels.Menu;
namespace API.BusinessLogic.Interface.IModuleAndMenu
{
    public interface IModuleAndMenuServiceQueries
    {
        #region Module
        Task<object?> GetModuleByModuleID(int id);
        Task<object?> GetModuleList(vmModuleSearch module);
        #endregion

        #region Menu
        Task<object?> GetMenuByMenuID(int id);
        Task<object?> GetMenuList(vmMenus menu);
        #endregion

        #region MenuPermision
        Task<object?> GetMenuPermissionList(vmMenuSearch menu);
        #endregion
    }
}
