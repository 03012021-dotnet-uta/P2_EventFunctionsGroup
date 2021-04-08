using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class init1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Users",
                newName: "LName");

            migrationBuilder.AddColumn<string>(
                name: "FName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FName",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "LName",
                table: "Users",
                newName: "Name");
        }
    }
}
