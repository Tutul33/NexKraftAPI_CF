using API.BusinessLogic.Interface.IModuleAndMenu;
using API.RepositoryManagement.UnityOfWork.Interfaces;
using API.Settings;
using API.ViewModel.ViewModels.Common;
using API.ViewModel.ViewModels.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace API.BusinessLogic.Services.ModuleAndMenu
{
    public class ModuleAndMenuServiceQueries : IModuleAndMenuServiceQueries
    {
        private readonly IUnitOfWork _unitOfWork;
        public ModuleAndMenuServiceQueries(IUnitOfWork _unitOfWork)
        {
            this._unitOfWork = _unitOfWork;
        }
        public async Task<object?> GetMenuByMenuID(int id)
        {
            vmMenu? menu = new vmMenu();
            try
            {
                var objMenu = await _unitOfWork.MenuRepository.GetMenuInfo(id);
                if (objMenu != null)
                {
                    menu.MenuId = objMenu.MenuId;
                    menu.MenuName = objMenu?.MenuName;
                    menu.SubParentId = objMenu?.SubParentId;
                    menu.ParentId = objMenu?.ParentId;
                    menu.IsSubParent = objMenu?.IsSubParent;
                    menu.MenuPath = objMenu?.MenuPath;
                    menu.MenuSequence = objMenu?.MenuSequence;
                    menu.MenuIcon = objMenu?.MenuIcon;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return menu;
        }
        public async Task<object?> GetMenuList(vmMenus menu)
        {
            object list = new object(); int total = 0;
            CommonData common = new CommonData()
            {
                PageNumber = menu.PageNumber,
                PageSize = menu.PageSize,
            };
            try
            {
                total = (from l in await _unitOfWork.MenuRepository.GetMenuList()
                         join m in await _unitOfWork.ModuleRepository.GetModuleList() on l.ModuleId equals m.ModuleId
                         where
                         string.IsNullOrEmpty(menu.Search) ? true
                         :
                         (
                           (m.ModuleName == null ? m.ModuleName == null : m.ModuleName.Contains(menu.Search)) ||
                           (l.MenuName == null ? l.MenuName == null : l.MenuName.Contains(menu.Search))
                          )
                         select m).ToList().Count();
                list = (from l in await _unitOfWork.MenuRepository.GetMenuList()
                        join m in await _unitOfWork.ModuleRepository.GetModuleList() on l.ModuleId equals m.ModuleId
                        where
                        string.IsNullOrEmpty(menu.Search) ? true
                        :
                        (
                          (m.ModuleName == null ? m.ModuleName == null : m.ModuleName.Contains(menu.Search)) ||
                          (l.MenuName == null ? l.MenuName == null : l.MenuName.Contains(menu.Search))
                         )
                        select new
                        {
                            //Module
                            moduleId = m.ModuleId,
                            moduleName = m.ModuleName,
                            modulePath = m.ModulePath,
                            moduleSequence = m.ModuleSequence,
                            description = m.Description,
                            //Menu
                            menuId = l.MenuId,
                            menuName = l.MenuName,
                            menuPath = l.MenuPath,
                            menuSequence = l.MenuSequence,
                            isSubParent = l.IsSubParent,
                            parentId = l.ParentId,
                            subParentId = l.SubParentId,
                            menuIctive = l.IsActive,
                            menuIcon = l.MenuIcon,
                            ////Permission
                            //permissionId = p.PermissionId,
                            //roleId = p.RoleId,
                            //userId = p.UserId,
                            //canCreate = p.CanCreate,
                            //canDelete = p.CanDelete,
                            //canEdit = p.Can3Edit,
                            //canView = p.CanView,
                            //pActive = p.IsActive,
                            isDeleting = false,
                            total
                        }
                                ).OrderByDescending(x => x.menuId).Skip(Common.Skip(common)).Take(common.PageSize).ToList();

            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return new
            {
                list,
                total
            };
        }
        public Task<object?> GetModuleByModuleID(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<object?> GetModuleList(vmModuleSearch module)
        {
            object list = new object(); int total = 0;
            CommonData common = new CommonData()
            {
                PageNumber = module.PageNumber,
                PageSize = module.PageSize,
            };
            try
            {
                if (module.PageSize > 0)
                {
                    total = (from m in await _unitOfWork.ModuleRepository.GetModuleList()
                             where
                             string.IsNullOrEmpty(module.Search) ? true
                             : (m.ModuleName == null ? m.ModuleName == null : m.ModuleName.Contains(module.Search))
                             select m).ToList().Count();
                    list = (from m in await _unitOfWork.ModuleRepository.GetModuleList()
                            where
                            string.IsNullOrEmpty(module.Search) ? true
                            : (m.ModuleName == null ? m.ModuleName == null : m.ModuleName.Contains(module.Search))

                            select new
                            {
                                //Module
                                moduleId = m.ModuleId,
                                moduleName = m.ModuleName,
                                modulePath = m.ModulePath,
                                moduleSequence = m.ModuleSequence,
                                description = m.Description,
                                moduleIcon = m.ModuleIcon,
                                active = m.IsActive,
                                isDeleting = false,
                                total
                            }
                                    ).OrderByDescending(x => x.moduleId).Skip(Common.Skip(common)).Take(common.PageSize).ToList();

                }
                else
                {
                    list = (from m in await _unitOfWork.ModuleRepository.GetModuleList()
                            select new
                            {
                                //Module
                                moduleId = m.ModuleId,
                                moduleName = m.ModuleName,
                                modulePath = m.ModulePath,
                                moduleSequence = m.ModuleSequence,
                                description = m.Description,
                                moduleIcon = m.ModuleIcon,
                                active = m.IsActive,
                                isDeleting = false,

                            }
                             ).ToList();
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return new
            {
                list,
                total
            };
        }        
        public async Task<object?> GetMenuPermissionList(vmMenuSearch menu)
        {
            object list = new object(); int total = 0;
            CommonData common = new CommonData()
            {
                PageNumber = menu.PageNumber,
                PageSize = menu.PageSize,
            };
            try
            {
                var roleList = await _unitOfWork.RoleRepository.GetRoleList();
                var RoleWiseMenuList = (from m in await _unitOfWork.MenuRepository.GetMenuList()
                                        from r in roleList
                                        select new
                                        {
                                            m.MenuId,
                                            m.MenuName,
                                            m.MenuPath,
                                            m.MenuIcon,
                                            m.MenuSequence,
                                            m.ModuleId,
                                            m.ParentId,
                                            m.SubParentId,
                                            m.IsSubParent,
                                            m.IsActive,
                                            r.RoleName,
                                            r.RoleId,
                                            r.Sequence
                                        }).ToList();
                total = (from l in RoleWiseMenuList
                         join m in await _unitOfWork.ModuleRepository.GetModuleList() on l.ModuleId equals m.ModuleId
                         join p in await _unitOfWork.MenuPermissionRepository.GetMenuPermissionList() on l.MenuId equals p.MenuId
                         into lp
                         from p in lp.DefaultIfEmpty()
                         where
                        ((string.IsNullOrEmpty(menu.Search) ? true
                         : (m.ModuleName == null ? m.ModuleName == null : m.ModuleName.Contains(menu.Search))
                         || (l.MenuName == null ? l.MenuName == null : l.MenuName.Contains(menu.Search))
                         ||(l.RoleName == null ? l.RoleName == null : l.RoleName.Contains(menu.Search))
                          )
                          ||
                          (string.IsNullOrEmpty(menu.Search) ? true
                         :
                           (l.MenuName == null ? l.MenuName == null : l.MenuName.Contains(menu.Search))
                          )
                          ||
                          (string.IsNullOrEmpty(menu.Search) ? true
                         :
                           (l.RoleName == null ? l.RoleName == null : l.RoleName.Contains(menu.Search))
                          ))
                          && (Convert.ToInt32(menu.ModuleId) == 0 ? true : (l.ModuleId == menu.ModuleId))
                          && (Convert.ToInt32(menu.RoleId) == 0 ? true : (l.RoleId == menu.RoleId))
                         select m).ToList().Count();
                list = (from l in RoleWiseMenuList
                        join m in await _unitOfWork.ModuleRepository.GetModuleList() on l.ModuleId equals m.ModuleId
                        join p in await _unitOfWork.MenuPermissionRepository.GetMenuPermissionList() on l.MenuId equals p.MenuId
                        into lp
                        from p in lp.DefaultIfEmpty()
                        where
                         ((string.IsNullOrEmpty(menu.Search) ? true
                         : (m.ModuleName == null ? m.ModuleName == null : m.ModuleName.Contains(menu.Search))
                          )
                          ||
                          (string.IsNullOrEmpty(menu.Search) ? true
                         :
                           (l.MenuName == null ? l.MenuName == null : l.MenuName.Contains(menu.Search))
                          )
                          ||
                          (string.IsNullOrEmpty(menu.Search) ? true
                         :
                           (l.RoleName == null ? l.RoleName == null : l.RoleName.Contains(menu.Search))
                          ))
                          && (Convert.ToInt32(menu.ModuleId) == 0 ? true : (l.ModuleId == menu.ModuleId))
                          && (Convert.ToInt32(menu.RoleId) == 0 ? true : (l.RoleId == menu.RoleId))
                        select new
                        {
                            //Module
                            moduleId = m.ModuleId,
                            moduleName = m.ModuleName,
                            modulePath = m.ModulePath,
                            moduleSequence = m.ModuleSequence,
                            description = m.Description,
                            //Menu
                            menuId = l.MenuId,
                            menuName = l.MenuName,
                            menuPath = l.MenuPath,
                            menuSequence = l.MenuSequence,
                            isSubParent = l.IsSubParent,
                            parentId = l.ParentId,
                            subParentId = l.SubParentId,
                            menuIctive = l.IsActive,
                            ////Permission
                            permissionId = p?.PermissionId,
                            roleId = p?.RoleId,
                            roleSequence = l?.Sequence,
                            roleName = l?.RoleName,
                            userId = p?.UserId,
                            canCreate = p?.CanCreate,
                            canDelete = p?.CanDelete,
                            canEdit = p?.CanEdit,
                            canView = p?.CanView,
                            pActive = p?.IsActive,
                            isDeleting = false,
                            total
                        }
                                ).OrderBy(x => Convert.ToInt32(x.roleSequence)).Skip(Common.Skip(common)).Take(common.PageSize).ToList();

            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return new
            {
                list,
                total
            };
        }
        
    }
}