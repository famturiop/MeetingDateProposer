using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MeetingDateProposer.DataLayer.Migrations
{
    public partial class num5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("d8dcd30c-1005-405a-d397-08d96a226c76"), "56362957-f475-4816-8eef-18c41da2d177", "admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("5d106043-53f8-4a1b-8459-e5409d1b2b0a"), "b8f3a301-b1e4-4908-ab09-5e982a15c485", "user", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("4f4d9c6c-e823-457e-9bfa-b2d15922ca17"), 0, "151fd546-dbb8-4fea-acde-fd92a9c36dd6", "test@test.com", true, true, null, "admin", "TEST@TEST.COM", "TEST@TEST.COM", "AQAAAAEAACcQAAAAEMIBDzfr1r8BE3b/Ev0Z2rR7dn8yik3zA4eu4rAO04oGExaP4izKHFwF7KoKiuxt9A==", null, false, "fb8284b5-4496-4d08-b99b-f0aeece1e0da", false, "test@test.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("d8dcd30c-1005-405a-d397-08d96a226c76"), new Guid("4f4d9c6c-e823-457e-9bfa-b2d15922ca17") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("5d106043-53f8-4a1b-8459-e5409d1b2b0a"));

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("d8dcd30c-1005-405a-d397-08d96a226c76"), new Guid("4f4d9c6c-e823-457e-9bfa-b2d15922ca17") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d8dcd30c-1005-405a-d397-08d96a226c76"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("4f4d9c6c-e823-457e-9bfa-b2d15922ca17"));
        }
    }
}
