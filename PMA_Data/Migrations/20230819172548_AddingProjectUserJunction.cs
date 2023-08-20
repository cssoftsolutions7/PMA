using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PMA_Data.Migrations
{
    /// <inheritdoc />
    public partial class AddingProjectUserJunction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProjectUserJunctions",
                columns: table => new
                {
                    ProjectUserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectUserJunctions", x => x.ProjectUserID);
                    table.ForeignKey(
                        name: "FK_ProjectUserJunctions_PMA_Projects_ProjectID",
                        column: x => x.ProjectID,
                        principalTable: "PMA_Projects",
                        principalColumn: "ProjectID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectUserJunctions_PMA_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "PMA_Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectUserJunctions_ProjectID",
                table: "ProjectUserJunctions",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectUserJunctions_UserID",
                table: "ProjectUserJunctions",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectUserJunctions");
        }
    }
}
