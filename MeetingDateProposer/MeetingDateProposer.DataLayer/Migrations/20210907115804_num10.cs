using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MeetingDateProposer.DataLayer.Migrations
{
    public partial class num10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("d8dcd30c-1005-405a-d397-08d96a226c76"), "fc343ed8-2418-4994-bcfc-572faa808437", "admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("5d106043-53f8-4a1b-8459-e5409d1b2b0a"), "26efb17d-4d86-4c3f-9e83-dabc30e57f6c", "user", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("4f4d9c6c-e823-457e-9bfa-b2d15922ca17"), 0, "39015412-eb96-4113-a447-cf447107d691", "test@test.com", true, true, null, "TEST@TEST.COM", "TEST@TEST.COM", "AQAAAAEAACcQAAAAEDpEaIuFD85WQ15+4F2tFjxMaLhDLJFpvUOCT/22vy0rNOsRyFaDWkMIWdVAa/3xfQ==", null, false, "b79f4c60-d19d-42c5-90a6-efb865ef8cc4", false, "test@test.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("d8dcd30c-1005-405a-d397-08d96a226c76"), new Guid("4f4d9c6c-e823-457e-9bfa-b2d15922ca17") });
        }
    }
}
