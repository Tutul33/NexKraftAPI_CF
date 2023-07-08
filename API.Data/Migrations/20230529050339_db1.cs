using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class db1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dob = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileExtension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "Modules",
                columns: table => new
                {
                    ModuleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModuleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModuleIcon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModuleColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModulePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModuleSequence = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modules", x => x.ModuleId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sequence = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserRoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    LoginId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.UserRoleId);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    LoginId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HashPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => x.LoginId);
                    table.ForeignKey(
                        name: "FK_UserLogins_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId");
                });

            migrationBuilder.CreateTable(
                name: "Menus",
                columns: table => new
                {
                    MenuId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModuleId = table.Column<int>(type: "int", nullable: true),
                    MenuName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    SubParentId = table.Column<int>(type: "int", nullable: true),
                    IsSubParent = table.Column<bool>(type: "bit", nullable: true),
                    MenuIcon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MenuPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MenuSequence = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.MenuId);
                    table.ForeignKey(
                        name: "FK_Menus_Modules_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Modules",
                        principalColumn: "ModuleId");
                });

            migrationBuilder.CreateTable(
                name: "MenuPermissions",
                columns: table => new
                {
                    PermissionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CanCreate = table.Column<bool>(type: "bit", nullable: true),
                    CanEdit = table.Column<bool>(type: "bit", nullable: true),
                    CanDelete = table.Column<bool>(type: "bit", nullable: true),
                    CanView = table.Column<bool>(type: "bit", nullable: true),
                    MenuId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuPermissions", x => x.PermissionId);
                    table.ForeignKey(
                        name: "FK_MenuPermissions_Menus_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menus",
                        principalColumn: "MenuId");
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Address", "Country", "Dob", "Email", "FileExtension", "FilePath", "FirstName", "FullName", "IsActive", "LastName", "Phone" },
                values: new object[] { 1, "Dhaka,Bangladesh", "", new DateTime(2057, 7, 29, 11, 3, 39, 779, DateTimeKind.Local).AddTicks(2060), "tutulcou@gmail.com", "jpg", "FB_IMG_1602836847801_20230430151425779.jpg", "Tutul", "Tutul Chakma", true, "Chakma", "01914570198" });

            migrationBuilder.InsertData(
                table: "Modules",
                columns: new[] { "ModuleId", "Description", "IsActive", "ModuleColor", "ModuleIcon", "ModuleName", "ModulePath", "ModuleSequence" },
                values: new object[,]
                {
                    { 1, "Dashboard", true, "", "fas fa-tachometer-alt", "Dashboard", "/", 1 },
                    { 2, "User Management", true, "", "fa fa-user", "User Management", "/users", 2 },
                    { 3, "Module Management", true, "", "fas fa-columns", "Module Management", "/modules", 4 },
                    { 4, "Role Management", true, "", "fa fa-tasks", "Role", "/roles", 3 }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "IsActive", "RoleName", "Sequence" },
                values: new object[,]
                {
                    { 1, true, "SuperAdmin", 1 },
                    { 2, true, "Admin", 1 },
                    { 3, true, "User", 1 }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "UserRoleId", "LoginId", "RoleId" },
                values: new object[] { 1, 1, 1 });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "MenuId", "IsActive", "IsSubParent", "MenuIcon", "MenuName", "MenuPath", "MenuSequence", "ModuleId", "ParentId", "SubParentId" },
                values: new object[,]
                {
                    { 1, true, false, "fas fa-tachometer-alt", "Home", "/", 1, 1, 0, 0 },
                    { 2, true, false, "fa fa-user", "Users", "/users", 1, 2, 0, 0 },
                    { 3, true, false, "fas fa-columns", "Modules", "/modules", 1, 3, 0, 0 },
                    { 4, true, false, "fa fa-bars", "Menus", "/modules/menus", 2, 3, 3, 0 },
                    { 5, true, false, "fa fa-lock", "Permissions", "/modules/permissions", 3, 3, 3, 0 },
                    { 6, true, false, "fa fa-tasks", "Roles", "/roles", 1, 4, 0, 0 }
                });

            migrationBuilder.InsertData(
                table: "UserLogins",
                columns: new[] { "LoginId", "CustomerId", "HashPassword", "IsActive", "Password", "UserName" },
                values: new object[] { 1, 1, "MTIzNDU2", true, "123456", "tutulcou@gmail.com" });

            migrationBuilder.InsertData(
                table: "MenuPermissions",
                columns: new[] { "PermissionId", "CanCreate", "CanDelete", "CanEdit", "CanView", "IsActive", "MenuId", "RoleId", "UserId" },
                values: new object[,]
                {
                    { 1, true, true, true, true, true, 1, 1, 0 },
                    { 2, true, true, true, true, true, 2, 1, 0 },
                    { 3, true, true, true, true, true, 3, 1, 0 },
                    { 4, true, true, true, true, true, 4, 1, 0 },
                    { 5, true, true, true, true, true, 1, 2, 0 },
                    { 6, true, true, true, true, true, 2, 2, 0 },
                    { 7, true, true, true, true, true, 3, 2, 0 },
                    { 8, false, false, false, false, true, 1, 3, 0 },
                    { 9, false, false, false, false, true, 2, 3, 0 },
                    { 10, true, true, true, true, true, 5, 1, 0 },
                    { 11, true, true, true, true, true, 6, 1, 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MenuPermissions_MenuId",
                table: "MenuPermissions",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_Menus_ModuleId",
                table: "Menus",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_CustomerId",
                table: "UserLogins",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MenuPermissions");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Menus");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Modules");
        }
    }
}
