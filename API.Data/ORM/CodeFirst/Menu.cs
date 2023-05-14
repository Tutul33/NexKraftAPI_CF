using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.DataAccess.ORM.CodeFirst
{
    public class Menu
    {
        public int MenuId { get; set; }

        public int? ModuleId { get; set; }

        public string? MenuName { get; set; }

        public int? ParentId { get; set; }

        public int? SubParentId { get; set; }

        public bool? IsSubParent { get; set; }

        public string? MenuIcon { get; set; }

        public string? MenuPath { get; set; }

        public int? MenuSequence { get; set; }

        public bool? IsActive { get; set; }
        public virtual Module? Module { get; set; }
        public virtual List<MenuPermission>? MenuPermissions { get; set; }
    }
}
