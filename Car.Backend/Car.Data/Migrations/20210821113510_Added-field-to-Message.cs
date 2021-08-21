using Microsoft.EntityFrameworkCore.Migrations;

namespace Car.Data.Migrations
{
    public partial class AddedfieldtoMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRead",
                table: "Message",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FCMToken",
                table: "User");

            migrationBuilder.DropColumn(
                name: "IsRead",
                table: "Message");
        }
    }
}
