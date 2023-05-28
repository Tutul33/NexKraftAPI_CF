using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.DataAccess.ORM.CodeFirst
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    CustomerId = 1,
                    FullName = "Tutul Chakma",
                    Country = "",
                    Email = "tutulcou@gmail.com",
                    FirstName = "Tutul",
                    LastName = "Chakma",
                    Phone = "01914570198",
                    Address = "Dhaka,Bangladesh",
                    Dob = DateTime.Now.AddMonths(410),
                    FilePath = "FB_IMG_1602836847801_20230430151425779.jpg",
                    FileExtension = "jpg",
                    IsActive = true
                }
            );
            modelBuilder.Entity<UserLogin>().HasData(
                new UserLogin
                {
                    LoginId = 1,
                    CustomerId = 1,
                    UserName = "tutulcou@gmail.com",
                    Password = "123456",
                    HashPassword = "MTIzNDU2",
                    IsActive = true
                }
            );
            modelBuilder.Entity<Role>().HasData(
               new Role
               {
                   RoleId = 1,
                   RoleName = "SuperAdmin",
                   Sequence = 1,
                   IsActive = true
               },
                new Role
                {
                    RoleId = 2,
                    RoleName = "Admin",
                    Sequence = 1,
                    IsActive = true
                },
                 new Role
                 {
                     RoleId = 3,
                     RoleName = "User",
                     Sequence = 1,
                     IsActive = true
                 }
           );
            modelBuilder.Entity<UserRole>().HasData(
              new UserRole
              {
                  UserRoleId = 1,
                  RoleId = 1,
                  LoginId = 1,
              }
            );
            modelBuilder.Entity<Module>().HasData(
             new Module
             {
                 ModuleId = 1,
                 ModuleName = "Dashboard",
                 Description = "Dashboard",
                 ModuleIcon = "fas fa-tachometer-alt",
                 ModuleColor = "",
                 ModulePath = "/",
                 ModuleSequence = 1,
                 IsActive = true,
             },
             new Module
             {
                 ModuleId = 2,
                 ModuleName = "User Management",
                 Description = "User Management",
                 ModuleIcon = "fa fa-user",
                 ModuleColor = "",
                 ModulePath = "/users",
                 ModuleSequence = 2,
                 IsActive = true,
             },
              new Module
              {
                  ModuleId = 3,
                  ModuleName = "Module Management",
                  Description = "Module Management",
                  ModuleIcon = "fas fa-columns",
                  ModuleColor = "",
                  ModulePath = "/modules",
                  ModuleSequence = 4,
                  IsActive = true,
              },
              new Module
              {
                  ModuleId = 4,
                  ModuleName = "Role",
                  Description = "Role Management",
                  ModuleIcon = "fa fa-tasks",
                  ModuleColor = "",
                  ModulePath = "/roles",
                  ModuleSequence = 3,
                  IsActive = true,
              }
            );
            modelBuilder.Entity<Menu>().HasData(
            new Menu
            {
                MenuId = 1,
                ModuleId = 1,
                MenuName = "Home",
                ParentId = 0,
                SubParentId = 0,
                IsSubParent = false,
                MenuIcon = "fas fa-tachometer-alt",
                MenuPath = "/",
                MenuSequence = 1,
                IsActive = true
            },
             new Menu
             {
                 MenuId = 2,
                 ModuleId = 2,
                 MenuName = "Users",
                 ParentId = 0,
                 SubParentId = 0,
                 IsSubParent = false,
                 MenuIcon = "fa fa-user",
                 MenuPath = "/users",
                 MenuSequence = 1,
                 IsActive = true
             },
              new Menu
              {
                  MenuId = 3,
                  ModuleId = 3,
                  MenuName = "Modules",
                  ParentId = 0,
                  SubParentId = 0,
                  IsSubParent = false,
                  MenuIcon = "fas fa-columns",
                  MenuPath = "/modules",
                  MenuSequence = 1,
                  IsActive = true
              },
              new Menu
              {
                  MenuId = 4,
                  ModuleId = 3,
                  MenuName = "Menus",
                  ParentId = 3,
                  SubParentId = 0,
                  IsSubParent = false,
                  MenuIcon = "fa fa-bars",
                  MenuPath = "/modules/menus",
                  MenuSequence = 2,
                  IsActive = true
              },
               new Menu
               {
                   MenuId = 5,
                   ModuleId = 3,
                   MenuName = "Permissions",
                   ParentId = 3,
                   SubParentId = 0,
                   IsSubParent = false,
                   MenuIcon = "fa fa-lock",
                   MenuPath = "/modules/permissions",
                   MenuSequence = 3,
                   IsActive = true
               },
               new Menu
               {
                   MenuId = 6,
                   ModuleId = 4,
                   MenuName = "Roles",
                   ParentId = 0,
                   SubParentId = 0,
                   IsSubParent = false,
                   MenuIcon = "fa fa-tasks",
                   MenuPath = "/roles",
                   MenuSequence = 1,
                   IsActive = true
               }
           );
            modelBuilder.Entity<MenuPermission>().HasData(
           new MenuPermission
           {
               PermissionId = 1,
               CanCreate = true,
               CanEdit = true,
               CanDelete = true,
               CanView = true,
               MenuId = 1,
               UserId = 0,
               RoleId = 1,
               IsActive = true,
           },
           new MenuPermission
           {
               PermissionId = 2,
               CanCreate = true,
               CanEdit = true,
               CanDelete = true,
               CanView = true,
               MenuId = 2,
               UserId = 0,
               RoleId = 1,
               IsActive = true,
           },
           new MenuPermission
           {
               PermissionId = 3,
               CanCreate = true,
               CanEdit = true,
               CanDelete = true,
               CanView = true,
               MenuId = 3,
               UserId = 0,
               RoleId = 1,
               IsActive = true,
           },
           new MenuPermission
           {
               PermissionId = 4,
               CanCreate = true,
               CanEdit = true,
               CanDelete = true,
               CanView = true,
               MenuId = 4,
               UserId = 0,
               RoleId = 1,
               IsActive = true,
           },
           new MenuPermission
           {
               PermissionId = 5,
               CanCreate = true,
               CanEdit = true,
               CanDelete = true,
               CanView = true,
               MenuId = 1,
               UserId = 0,
               RoleId = 2,
               IsActive = true,
           },
            new MenuPermission
            {
                PermissionId = 6,
                CanCreate = true,
                CanEdit = true,
                CanDelete = true,
                CanView = true,
                MenuId = 2,
                UserId = 0,
                RoleId = 2,
                IsActive = true,
            },
            new MenuPermission
            {
                PermissionId = 7,
                CanCreate = true,
                CanEdit = true,
                CanDelete = true,
                CanView = true,
                MenuId = 3,
                UserId = 0,
                RoleId = 2,
                IsActive = true,
            },
            new MenuPermission
            {
                PermissionId = 8,
                CanCreate = false,
                CanEdit = false,
                CanDelete = false,
                CanView = false,
                MenuId = 1,
                UserId = 0,
                RoleId = 3,
                IsActive = true,
            },
            new MenuPermission
            {
                PermissionId = 9,
                CanCreate = false,
                CanEdit = false,
                CanDelete = false,
                CanView = false,
                MenuId = 2,
                UserId = 0,
                RoleId = 3,
                IsActive = true,
            },
            new MenuPermission
            {
                PermissionId = 10,
                CanCreate = true,
                CanEdit = true,
                CanDelete = true,
                CanView = true,
                MenuId = 5,
                UserId = 0,
                RoleId = 1,
                IsActive = true,
            },
            new MenuPermission
            {
                PermissionId = 11,
                CanCreate = true,
                CanEdit = true,
                CanDelete = true,
                CanView = true,
                MenuId = 6,
                UserId = 0,
                RoleId = 1,
                IsActive = true,
            }
           );
        }
    }
}
