using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMS.Repository.Migrations
{
    public partial class Appuser_updated_IsAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "ApplicationUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "ApplicationUsers");
        }
    }
}
