using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.DataAccess.ORM.CodeFirst
{
    public class UserLogin
    {
        [Key]
        public int LoginId { get; set; }

        public int? CustomerId { get; set; }

        public string? UserName { get; set; }

        public string? Password { get; set; }

        public string? HashPassword { get; set; }

        public bool? IsActive { get; set; }

        public virtual Customer? Customer { get; set; }
        public virtual List<UserRole>? UserRoles { get; set; }
    }
}
