using API.ViewModel.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.ViewModel.ViewModels.Roles
{
    public class vmRole: CommonData
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public int Sequence { get; set; }
        public bool IsActive { get; set; }
    }
}
