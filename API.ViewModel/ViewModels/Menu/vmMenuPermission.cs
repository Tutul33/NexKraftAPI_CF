using API.ViewModel.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.ViewModel.ViewModels.Menu
{
    public class vmMenuPermission
    {
        public int PermissionId { get; set; }

        public bool? CanCreate { get; set; }

        public bool? CanEdit { get; set; }

        public bool? CanDelete { get; set; }

        public bool? CanView { get; set; }

        public int? MenuId { get; set; }

        public int? UserId { get; set; }

        public int? RoleId { get; set; }

        public bool? IsActive { get; set; }
    }
}
