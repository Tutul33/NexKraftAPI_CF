using API.ViewModel.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.ViewModel.ViewModels.Menu
{
    public class vmModule: CommonData
    {
        public int ModuleId { get; set; }

        public string ModuleName { get; set; } = null!;

        public string? Description { get; set; }

        public string? ModuleIcon { get; set; }

        public string? ModuleColor { get; set; }

        public string ModulePath { get; set; } = null!;

        public int? ModuleSequence { get; set; }

        public bool? IsActive { get; set; }
        public List<vmMenu>? menus { get; set; }
    }
    public class vmModuleSearch:CommonData
    {
        
    }
}
