using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.DataAccess.ORM.CodeFirst
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }

        public string? RoleName { get; set; }

        public int? Sequence { get; set; }

        public bool? IsActive { get; set; }
    }
}
