using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PMA_Data.Migrations
{
    /// <inheritdoc />
    public partial class AddingAssignedUserFKinTaskTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AssignedUserId",
                table: "PMA_Tasks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PMA_Tasks_AssignedUserId",
                table: "PMA_Tasks",
                column: "AssignedUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PMA_Tasks_PMA_Users_AssignedUserId",
                table: "PMA_Tasks",
                column: "AssignedUserId",
                principalTable: "PMA_Users",
                principalColumn: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PMA_Tasks_PMA_Users_AssignedUserId",
                table: "PMA_Tasks");

            migrationBuilder.DropIndex(
                name: "IX_PMA_Tasks_AssignedUserId",
                table: "PMA_Tasks");

            migrationBuilder.DropColumn(
                name: "AssignedUserId",
                table: "PMA_Tasks");
        }
    }
}
