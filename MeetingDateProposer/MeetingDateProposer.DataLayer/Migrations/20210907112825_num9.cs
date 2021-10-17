using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace MeetingDateProposer.DataLayer.Migrations
{
    public partial class num9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ApplicationUsers_AccountUserId",
                table: "ApplicationUsers");

            migrationBuilder.AlterColumn<Guid>(
                name: "AccountUserId",
                table: "ApplicationUsers",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("5d106043-53f8-4a1b-8459-e5409d1b2b0a"),
                column: "ConcurrencyStamp",
                value: "26efb17d-4d86-4c3f-9e83-dabc30e57f6c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d8dcd30c-1005-405a-d397-08d96a226c76"),
                column: "ConcurrencyStamp",
                value: "fc343ed8-2418-4994-bcfc-572faa808437");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("4f4d9c6c-e823-457e-9bfa-b2d15922ca17"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "39015412-eb96-4113-a447-cf447107d691", "AQAAAAEAACcQAAAAEDpEaIuFD85WQ15+4F2tFjxMaLhDLJFpvUOCT/22vy0rNOsRyFaDWkMIWdVAa/3xfQ==", "b79f4c60-d19d-42c5-90a6-efb865ef8cc4" });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUsers_AccountUserId",
                table: "ApplicationUsers",
                column: "AccountUserId",
                unique: true,
                filter: "[AccountUserId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ApplicationUsers_AccountUserId",
                table: "ApplicationUsers");

            migrationBuilder.AlterColumn<Guid>(
                name: "AccountUserId",
                table: "ApplicationUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("5d106043-53f8-4a1b-8459-e5409d1b2b0a"),
                column: "ConcurrencyStamp",
                value: "82e39fc8-7828-4d40-8c88-4a64c113c424");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d8dcd30c-1005-405a-d397-08d96a226c76"),
                column: "ConcurrencyStamp",
                value: "c325895f-6cae-4abc-80fe-bc7324771ed4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("4f4d9c6c-e823-457e-9bfa-b2d15922ca17"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d29e8a72-995f-47b4-8ec9-f739716884a1", "AQAAAAEAACcQAAAAEHTU7kAsNifgesKiXJ2INlBOuln6F6zDqCulIXZTs0Nfx5qwybNZL1790RWUkjIxXA==", "7e18619e-f825-4826-805b-121758c66498" });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUsers_AccountUserId",
                table: "ApplicationUsers",
                column: "AccountUserId",
                unique: true);
        }
    }
}
