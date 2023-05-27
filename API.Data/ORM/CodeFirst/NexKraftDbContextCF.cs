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
       // protected override void OnModelCreating(ModelBuilder modelBuilder)
       // {
       //     modelBuilder.Entity<Customer>().HasData(
       //         new Customer
       //         {
                   
       //         }
       //     );
       //     modelBuilder.Entity<UserLogin>().HasData(
       //         new UserLogin
       //         {

       //         }
       //     );
       //     modelBuilder.Entity<Role>().HasData(
       //        new Role
       //        {

       //        }
       //    );
       //     modelBuilder.Entity<UserRole>().HasData(
       //       new UserRole
       //       {

       //       }
       //   );
       //     modelBuilder.Entity<Module>().HasData(
       //      new Module
       //      {

       //      }
       //  );
       //     modelBuilder.Entity<Menu>().HasData(
       //     new Menu
       //     {

       //     }
       // );
       //     modelBuilder.Entity<MenuPermission>().HasData(
       //    new MenuPermission
       //    {

       //    }
       //);
       // }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            //options.UseSqlServer("Server=TUTULPC\\MSSQL_SRV_TUTUL;Database=NexKraftDB_CF;User Id=sa; Password=@Msi2023#;TrustServerCertificate=True");
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
