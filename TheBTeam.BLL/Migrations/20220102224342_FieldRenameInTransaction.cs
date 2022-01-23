using Microsoft.EntityFrameworkCore.Migrations;

namespace TheBTeam.BLL.Migrations
{
    public partial class FieldRenameInTransaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Transactions",
                newName: "Description");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Transactions",
                newName: "Title");
        }
    }
}
