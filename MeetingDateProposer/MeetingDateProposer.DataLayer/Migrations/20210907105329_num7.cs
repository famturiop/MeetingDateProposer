using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace MeetingDateProposer.DataLayer.Migrations
{
    public partial class num7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Calendars_Users_UserId",
                table: "Calendars");

            migrationBuilder.DropTable(
                name: "MeetingUser");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.CreateTable(
                name: "ApplicationUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationUsers_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserMeeting",
                columns: table => new
                {
                    ConnectedUsersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserMeetingsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserMeeting", x => new { x.ConnectedUsersId, x.UserMeetingsId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserMeeting_ApplicationUsers_ConnectedUsersId",
                        column: x => x.ConnectedUsersId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserMeeting_Meetings_UserMeetingsId",
                        column: x => x.UserMeetingsId,
                        principalTable: "Meetings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserMeeting_UserMeetingsId",
                table: "ApplicationUserMeeting",
                column: "UserMeetingsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Calendars_ApplicationUsers_UserId",
                table: "Calendars",
                column: "UserId",
                principalTable: "ApplicationUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Calendars_ApplicationUsers_UserId",
                table: "Calendars");

            migrationBuilder.DropTable(
                name: "ApplicationUserMeeting");

            migrationBuilder.DropTable(
                name: "ApplicationUsers");

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

            migrationBuilder.CreateTable(
                name: "MeetingUser",
                columns: table => new
                {
                    ConnectedUsersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserMeetingsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetingUser", x => new { x.ConnectedUsersId, x.UserMeetingsId });
                    table.ForeignKey(
                        name: "FK_MeetingUser_Meetings_UserMeetingsId",
                        column: x => x.UserMeetingsId,
                        principalTable: "Meetings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MeetingUser_Users_ConnectedUsersId",
                        column: x => x.ConnectedUsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateIndex(
                name: "IX_MeetingUser_UserMeetingsId",
                table: "MeetingUser",
                column: "UserMeetingsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Calendars_Users_UserId",
                table: "Calendars",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
