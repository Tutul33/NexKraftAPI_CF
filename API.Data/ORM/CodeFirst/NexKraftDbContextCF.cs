using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.DataAccess.ORM.CodeFirst
{
    public class NexKraftDbContextCF : DbContext
    {
        protected readonly IConfiguration Configuration;
        public NexKraftDbContextCF(IConfiguration _Configuration)
        {
            Configuration = _Configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        }
        public virtual DbSet<Customer> Customers{get;set;}
        public virtual DbSet<UserLogin> UserLogins { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<Module> Modules { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<MenuPermission> MenuPermissions { get; set; }
    }
}
