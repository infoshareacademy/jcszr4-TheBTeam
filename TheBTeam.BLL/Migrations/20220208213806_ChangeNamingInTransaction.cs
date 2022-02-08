using Microsoft.EntityFrameworkCore.Migrations;

namespace TheBTeam.BLL.Migrations
{
    public partial class ChangeNamingInTransaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WhenMade",
                table: "Transactions",
                newName: "Date");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Transactions",
                newName: "WhenMade");
        }
    }
}
