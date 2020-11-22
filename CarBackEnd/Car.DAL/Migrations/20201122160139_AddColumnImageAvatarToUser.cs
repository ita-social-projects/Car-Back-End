using Microsoft.EntityFrameworkCore.Migrations;

namespace Car.DAL.Migrations
{
    public partial class AddColumnImageAvatarToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageAvatar",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageAvatar",
                table: "User");
        }
    }
}
