using API.ViewModel.ViewModels.Menu;
namespace API.BusinessLogic.Interface.IModuleAndMenu
{
    public interface IModuleAndMenuServiceCommands
    {
        #region Module
        Task<object?> DeleteModule(int id);
        Task<object?> CreateModule(vmModule module);
        Task<object?> UpdateModule(vmModule module);
        #endregion

        #region Menu
        Task<object?> DeleteMenu(int id);
        Task<object?> CreateMenu(vmMenu menu);
        Task<object?> UpdateMenu(vmMenu menu);
        #endregion

        #region MenuPermision
        Task<object?> SetMenuPermission(vmMenuPermission permission);
        Task<object?> UpdateMenuMermission(vmMenuPermission permission);
        Task<object?> DeleteMenuPermission(int id);
        #endregion
    }
}
