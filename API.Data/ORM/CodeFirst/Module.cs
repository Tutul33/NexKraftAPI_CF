using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.DataAccess.ORM.CodeFirst
{
    public class Module
    {
        [Key]
        public int ModuleId { get; set; }

        public string? ModuleName { get; set; }

        public string? Description { get; set; }

        public string? ModuleIcon { get; set; }

        public string? ModuleColor { get; set; }

        public string? ModulePath { get; set; }

        public int? ModuleSequence { get; set; }

        public bool? IsActive { get; set; }
        public virtual List<Menu>? Menus { get; set; }
    }
}
