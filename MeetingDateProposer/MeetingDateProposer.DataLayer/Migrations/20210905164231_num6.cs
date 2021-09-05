using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MeetingDateProposer.DataLayer.Migrations
{
    public partial class num6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Calendars_AspNetUsers_UserId",
                table: "Calendars");

            migrationBuilder.DropForeignKey(
                name: "FK_MeetingUser_AspNetUsers_ConnectedUsersId",
                table: "MeetingUser");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "NEWID()");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("5d106043-53f8-4a1b-8459-e5409d1b2b0a"),
                column: "ConcurrencyStamp",
                value: "f977dea7-1109-4e64-9de6-f793dd3bc6a4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d8dcd30c-1005-405a-d397-08d96a226c76"),
                column: "ConcurrencyStamp",
                value: "737c9ada-f95c-4fc1-ac0a-1ba84d4c24fa");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("4f4d9c6c-e823-457e-9bfa-b2d15922ca17"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d3b1bf7a-0748-45e7-9cac-e2fdb7758c33", "AQAAAAEAACcQAAAAECR+5jn8gD0PAQUO1fQKYx2yxAiLgnqslNGMeZGG2ybX5eX4jqjCqJGZNUVd8OsZxQ==", "812e8c86-7de5-491b-8e15-9cd39c3a9f59" });

            migrationBuilder.AddForeignKey(
                name: "FK_Calendars_Users_UserId",
                table: "Calendars",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingUser_Users_ConnectedUsersId",
                table: "MeetingUser",
                column: "ConnectedUsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Calendars_Users_UserId",
                table: "Calendars");

            migrationBuilder.DropForeignKey(
                name: "FK_MeetingUser_Users_ConnectedUsersId",
                table: "MeetingUser");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWID()",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("5d106043-53f8-4a1b-8459-e5409d1b2b0a"),
                column: "ConcurrencyStamp",
                value: "b8f3a301-b1e4-4908-ab09-5e982a15c485");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d8dcd30c-1005-405a-d397-08d96a226c76"),
                column: "ConcurrencyStamp",
                value: "56362957-f475-4816-8eef-18c41da2d177");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("4f4d9c6c-e823-457e-9bfa-b2d15922ca17"),
                columns: new[] { "ConcurrencyStamp", "Name", "PasswordHash", "SecurityStamp" },
                values: new object[] { "151fd546-dbb8-4fea-acde-fd92a9c36dd6", "admin", "AQAAAAEAACcQAAAAEMIBDzfr1r8BE3b/Ev0Z2rR7dn8yik3zA4eu4rAO04oGExaP4izKHFwF7KoKiuxt9A==", "fb8284b5-4496-4d08-b99b-f0aeece1e0da" });

            migrationBuilder.AddForeignKey(
                name: "FK_Calendars_AspNetUsers_UserId",
                table: "Calendars",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingUser_AspNetUsers_ConnectedUsersId",
                table: "MeetingUser",
                column: "ConnectedUsersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
