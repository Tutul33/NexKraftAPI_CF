using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.ViewModel.ViewModels.UserRole
{
    public class CreateUserRole
    {
        public int UserRoleId { get; set; }
        public int RoleId { get; set; }
        public int LoginId { get; set; }
    }
}
