using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PMA_Data.Migrations
{
    /// <inheritdoc />
    public partial class renameProjectJunctiontoPMA_ProjectJunction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectUserJunctions_PMA_Projects_ProjectID",
                table: "ProjectUserJunctions");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectUserJunctions_PMA_Users_UserID",
                table: "ProjectUserJunctions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectUserJunctions",
                table: "ProjectUserJunctions");

            migrationBuilder.RenameTable(
                name: "ProjectUserJunctions",
                newName: "PMA_ProjectUserJunctions");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectUserJunctions_UserID",
                table: "PMA_ProjectUserJunctions",
                newName: "IX_PMA_ProjectUserJunctions_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectUserJunctions_ProjectID",
                table: "PMA_ProjectUserJunctions",
                newName: "IX_PMA_ProjectUserJunctions_ProjectID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PMA_ProjectUserJunctions",
                table: "PMA_ProjectUserJunctions",
                column: "ProjectUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_PMA_ProjectUserJunctions_PMA_Projects_ProjectID",
                table: "PMA_ProjectUserJunctions",
                column: "ProjectID",
                principalTable: "PMA_Projects",
                principalColumn: "ProjectID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PMA_ProjectUserJunctions_PMA_Users_UserID",
                table: "PMA_ProjectUserJunctions",
                column: "UserID",
                principalTable: "PMA_Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PMA_ProjectUserJunctions_PMA_Projects_ProjectID",
                table: "PMA_ProjectUserJunctions");

            migrationBuilder.DropForeignKey(
                name: "FK_PMA_ProjectUserJunctions_PMA_Users_UserID",
                table: "PMA_ProjectUserJunctions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PMA_ProjectUserJunctions",
                table: "PMA_ProjectUserJunctions");

            migrationBuilder.RenameTable(
                name: "PMA_ProjectUserJunctions",
                newName: "ProjectUserJunctions");

            migrationBuilder.RenameIndex(
                name: "IX_PMA_ProjectUserJunctions_UserID",
                table: "ProjectUserJunctions",
                newName: "IX_ProjectUserJunctions_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_PMA_ProjectUserJunctions_ProjectID",
                table: "ProjectUserJunctions",
                newName: "IX_ProjectUserJunctions_ProjectID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectUserJunctions",
                table: "ProjectUserJunctions",
                column: "ProjectUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectUserJunctions_PMA_Projects_ProjectID",
                table: "ProjectUserJunctions",
                column: "ProjectID",
                principalTable: "PMA_Projects",
                principalColumn: "ProjectID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectUserJunctions_PMA_Users_UserID",
                table: "ProjectUserJunctions",
                column: "UserID",
                principalTable: "PMA_Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
