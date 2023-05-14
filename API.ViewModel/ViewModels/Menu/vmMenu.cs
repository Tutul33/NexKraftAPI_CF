using API.ViewModel.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.ViewModel.ViewModels.Menu
{
    public class vmMenu: vmMenuPermission
    {
        public int MenuId { get; set; }

        public int? ModuleId { get; set; }

        public string? MenuName { get; set; }

        public int? ParentId { get; set; }

        public bool? IsSubParent { get; set; }

        public int? SubParentId { get; set; }

        public string? MenuIcon { get; set; }

        public string? MenuPath { get; set; }

        public int? MenuSequence { get; set; }

        public List<vmMenu>? menuList { get; set; }
    }
    public class vmMenus:CommonData
    {
        public int MenuId { get; set; }
        public string? MenuName { get; set; }
        public int? ModuleId { get; set; }
        public string? ModuleName { get; set; }

        public int? ParentId { get; set; }

        public bool? IsSubParent { get; set; }

        public int? SubParentId { get; set; }

        public string? MenuIcon { get; set; }

        public string? MenuPath { get; set; }

        public int? MenuSequence { get; set; }
    }
    public class vmMenuPermissionList : vmMenuPermission
    {
        public string? MenuName { get; set; }
        public int? ModuleId { get; set; }
        public string? ModuleName { get; set; }

        public int? ParentId { get; set; }

        public bool? IsSubParent { get; set; }

        public int? SubParentId { get; set; }

        public string? MenuIcon { get; set; }

        public string? MenuPath { get; set; }

        public int? MenuSequence { get; set; }
    }
    public class vmMenuSearch : CommonData
    {
        public int? ModuleId { get; set;}
        public int? RoleId { get; set; }
        public int? UserId { get; set; }
    }
}
