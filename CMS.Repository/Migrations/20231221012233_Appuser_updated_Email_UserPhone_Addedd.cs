using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMS.Repository.Migrations
{
    public partial class Appuser_updated_Email_UserPhone_Addedd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserPhone",
                table: "ApplicationUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserPhone",
                table: "ApplicationUsers");
        }
    }
}
