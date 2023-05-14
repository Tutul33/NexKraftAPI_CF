using API.BusinessLogic.Interface.IModuleAndMenu;
//using API.DataAccess.ORM.MsSQLDataModels;
using API.DataAccess.ORM.CodeFirst;
using API.RepositoryManagement.UnityOfWork.Interfaces;
using API.Settings;
using API.ViewModel.ViewModels.Common;
using API.ViewModel.ViewModels.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.BusinessLogic.Services.ModuleAndMenu
{
    public class ModuleAndMenuServiceCommands : IModuleAndMenuServiceCommands
    {
        private readonly IUnitOfWork _unitOfWork;
        public ModuleAndMenuServiceCommands(IUnitOfWork _unitOfWork)
        {
            this._unitOfWork = _unitOfWork;
        }
        public async Task<object?> CreateMenu(vmMenu menu)
        {
            string message = string.Empty; bool resstate = false; Menu objMenu = new();
            try
            {
                objMenu = await _unitOfWork.MenuRepository.CreateMenu(menu);
                await _unitOfWork.CompleteAsync();

                message = "Created Successfully.";
                resstate = true;

            }
            catch (Exception ex)
            {
                message = "Failed."; resstate = false;
            }
            return new
            {
                message,
                isSuccess = resstate
            };
        }
        public async Task<object?> CreateModule(vmModule module)
        {
            string message = string.Empty; bool resstate = false; Module objModule = new();
            try
            {
                objModule = await _unitOfWork.ModuleRepository.CreateModule(module);
                await _unitOfWork.CompleteAsync();

                message = "Created Successfully.";
                resstate = true;

            }
            catch (Exception ex)
            {
                message = "Failed."; resstate = false;
            }
            return new
            {
                message,
                isSuccess = resstate
            };
        }
        public async Task<object?> DeleteMenu(int id)
        {
            string message = string.Empty; bool resstate = false; int total = 0;
            try
            {
                var objMenu = await _unitOfWork.MenuRepository.GetMenuInfo(id);
                if (objMenu != null)
                {
                    await _unitOfWork.MenuPermissionRepository.DeleteMenuPermission(objMenu.MenuId);
                }
                resstate = await _unitOfWork.MenuRepository.DeleteMenu(id);
                await _unitOfWork.CompleteAsync();
                message = "Deleted Successfully.";
                total = await _unitOfWork.MenuRepository.GetMenuTotal();
            }
            catch (Exception ex)
            {
                message = "Failed."; resstate = false;
            }
            return new
            {
                message,
                isSuccess = resstate,
                total
            };
        }
        public async Task<object?> DeleteModule(int id)
        {
            string message = string.Empty; bool resstate = false; int total = 0; List<Menu> menuList = new List<Menu>();
            try
            {
                menuList = (await _unitOfWork.MenuRepository.GetMenuList()).Where(x => x.ModuleId == id).ToList();
                if (menuList.Count > 0)
                {
                    await _unitOfWork.ModuleRepository.DeleteModule(id);
                    await _unitOfWork.CompleteAsync();
                    message = "Deleted Successfully.";
                    total = await _unitOfWork.ModuleRepository.GetModuleTotal();
                }
                else
                {
                    message = "Please delete all associated menus of this module.Then try again.";
                }
            }
            catch (Exception ex)
            {
                message = "Failed."; resstate = false;
            }
            return new
            {
                message,
                isSuccess = resstate,
                total
            };
        }
       
        
       
        public async Task<object?> UpdateMenu(vmMenu menu)
        {
            string message = string.Empty; bool resstate = false;
            try
            {
                var objMenu = await _unitOfWork.MenuRepository.GetMenuInfo(menu.MenuId);
                if (objMenu != null)
                {
                    objMenu.MenuName = menu.MenuName;
                    objMenu.MenuPath = menu.MenuPath;
                    objMenu.MenuSequence = menu.MenuSequence;
                    objMenu.MenuIcon = menu.MenuIcon;
                    objMenu.ModuleId = menu.ModuleId;
                    await _unitOfWork.CompleteAsync();
                }
                message = "Updated Successfully.";
                resstate = true;
            }
            catch (Exception ex)
            {
                ex.ToString();
                message = "Failed.";
                resstate = false;
            }
            return new { message, isSuccess = resstate };
        }
        public async Task<object?> UpdateModule(vmModule module)
        {
            string message = string.Empty; bool resstate = false;
            try
            {
                var objMenu = await _unitOfWork.ModuleRepository.GetModuleInfo(module.ModuleId);
                if (objMenu != null)
                {
                    objMenu.ModuleName = module.ModuleName;
                    objMenu.ModulePath = module.ModulePath;
                    objMenu.ModuleSequence = module.ModuleSequence;
                    objMenu.ModuleColor = module.ModuleColor;
                    objMenu.ModuleIcon = module.ModuleIcon;
                    objMenu.Description = module.Description;
                    await _unitOfWork.CompleteAsync();
                }
                message = "Updated Successfully.";
                resstate = true;
            }
            catch (Exception ex)
            {
                ex.ToString();
                message = "Failed.";
                resstate = false;
            }
            return new { message, isSuccess = resstate };
        }
        public async Task<object?> SetMenuPermission(vmMenuPermission permission)
        {
            string message = string.Empty; bool resstate = false; MenuPermission objMenu = new();
            try
            {
                objMenu = await _unitOfWork.MenuPermissionRepository.SetMenuPermission(permission);
                await _unitOfWork.CompleteAsync();

                message = "Created Successfully.";
                resstate = true;

            }
            catch (Exception ex)
            {
                message = "Failed."; resstate = false;
            }
            return new
            {
                message,
                isSuccess = resstate
            };
        }
       
        public async Task<object?> UpdateMenuMermission(vmMenuPermission perm)
        {
            string message = string.Empty; bool resstate = false;
            try
            {
                var objMenuPerm = await _unitOfWork.MenuPermissionRepository.GetMenuPermissionByPermissionId(perm.PermissionId);
                if (objMenuPerm != null)
                {
                    objMenuPerm.MenuId = perm.MenuId;
                    objMenuPerm.UserId = perm.UserId;
                    objMenuPerm.RoleId = perm.RoleId;
                    objMenuPerm.CanView = perm.CanView;
                    objMenuPerm.CanCreate = perm.CanCreate;
                    objMenuPerm.CanDelete = perm.CanDelete;
                    objMenuPerm.CanEdit = perm.CanEdit;
                    objMenuPerm.IsActive = perm.IsActive;
                    await _unitOfWork.CompleteAsync();
                }
                message = "Updated Successfully.";
                resstate = true;
            }
            catch (Exception ex)
            {
                ex.ToString();
                message = "Failed.";
                resstate = false;
            }
            return new { message, isSuccess = resstate };
        }
        public async Task<object?> DeleteMenuPermission(int id)
        {
            string message = string.Empty; bool resstate = false;
            try
            {
                await _unitOfWork.MenuPermissionRepository.DeleteMenuPermission(id);
                await _unitOfWork.CompleteAsync();
                message = "Deleted Successfully.";
            }
            catch (Exception ex)
            {
                message = "Failed."; resstate = false;
            }
            return new
            {
                message,
                isSuccess = resstate
            };
        }
    }
}
