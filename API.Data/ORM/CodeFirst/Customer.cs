using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.DataAccess.ORM.CodeFirst
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        public string? FullName { get; set; }

        public string? Country { get; set; }

        public string? Email { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Phone { get; set; }

        public string? Address { get; set; }

        public DateTime? Dob { get; set; }

        public string? FilePath { get; set; }

        public string? FileExtension { get; set; }

        public bool? IsActive { get; set; }
        public List<UserLogin>? UserLogins { get; set; }

    }
}
