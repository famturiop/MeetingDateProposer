using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MeetingDateProposer.DataLayer.Migrations
{
    public partial class num8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUsers_AspNetUsers_Id",
                table: "ApplicationUsers");

            migrationBuilder.AddColumn<Guid>(
                name: "AccountUserId",
                table: "ApplicationUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUsers_AspNetUsers_AccountUserId",
                table: "ApplicationUsers",
                column: "AccountUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUsers_AspNetUsers_AccountUserId",
                table: "ApplicationUsers");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationUsers_AccountUserId",
                table: "ApplicationUsers");

            migrationBuilder.DropColumn(
                name: "AccountUserId",
                table: "ApplicationUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("5d106043-53f8-4a1b-8459-e5409d1b2b0a"),
                column: "ConcurrencyStamp",
                value: "565b7a34-5380-4d86-9138-ac80ea046a13");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d8dcd30c-1005-405a-d397-08d96a226c76"),
                column: "ConcurrencyStamp",
                value: "db31b03b-fb64-4a89-88e9-113008f2f0f5");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("4f4d9c6c-e823-457e-9bfa-b2d15922ca17"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ff60d0df-154a-4231-9802-43acb465a40d", "AQAAAAEAACcQAAAAEHr1eCPZWwB/h6KqPov+tDQKPcKDDnAxPOsrhULmh5H3dCLhhcwWDDVPCg4QV0kcDw==", "cd0b3ed8-a45d-4caf-8132-11930264246a" });

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUsers_AspNetUsers_Id",
                table: "ApplicationUsers",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
