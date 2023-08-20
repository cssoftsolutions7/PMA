using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PMA_Data.Migrations
{
    /// <inheritdoc />
    public partial class JSR : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PMA_Projects",
                columns: table => new
                {
                    ProjectID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PMA_Projects", x => x.ProjectID);
                });

            migrationBuilder.CreateTable(
                name: "PMA_UserRoles",
                columns: table => new
                {
                    RoleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PMA_UserRoles", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "PMA_Tasks",
                columns: table => new
                {
                    TaskID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PMA_Tasks", x => x.TaskID);
                    table.ForeignKey(
                        name: "FK_PMA_Tasks_PMA_Projects_ProjectID",
                        column: x => x.ProjectID,
                        principalTable: "PMA_Projects",
                        principalColumn: "ProjectID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PMA_Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    JoinDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProfileImageURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PMA_Users", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_PMA_Users_PMA_UserRoles_RoleID",
                        column: x => x.RoleID,
                        principalTable: "PMA_UserRoles",
                        principalColumn: "RoleID");
                });

            migrationBuilder.CreateTable(
                name: "PMA_DailyMemos",
                columns: table => new
                {
                    MemoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemoDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MemoText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PMA_DailyMemos", x => x.MemoID);
                    table.ForeignKey(
                        name: "FK_PMA_DailyMemos_PMA_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "PMA_Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PMA_DailyProgresses",
                columns: table => new
                {
                    ProgressID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProgressDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HoursWorked = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    TaskID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PMA_DailyProgresses", x => x.ProgressID);
                    table.ForeignKey(
                        name: "FK_PMA_DailyProgresses_PMA_Tasks_TaskID",
                        column: x => x.TaskID,
                        principalTable: "PMA_Tasks",
                        principalColumn: "TaskID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PMA_DailyProgresses_PMA_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "PMA_Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PMA_DailyMemos_UserID",
                table: "PMA_DailyMemos",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_PMA_DailyProgresses_TaskID",
                table: "PMA_DailyProgresses",
                column: "TaskID");

            migrationBuilder.CreateIndex(
                name: "IX_PMA_DailyProgresses_UserID",
                table: "PMA_DailyProgresses",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_PMA_Tasks_ProjectID",
                table: "PMA_Tasks",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_PMA_Users_RoleID",
                table: "PMA_Users",
                column: "RoleID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PMA_DailyMemos");

            migrationBuilder.DropTable(
                name: "PMA_DailyProgresses");

            migrationBuilder.DropTable(
                name: "PMA_Tasks");

            migrationBuilder.DropTable(
                name: "PMA_Users");

            migrationBuilder.DropTable(
                name: "PMA_Projects");

            migrationBuilder.DropTable(
                name: "PMA_UserRoles");
        }
    }
}
