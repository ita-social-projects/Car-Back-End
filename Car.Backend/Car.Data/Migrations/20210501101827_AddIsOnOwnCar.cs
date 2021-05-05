using Microsoft.EntityFrameworkCore.Migrations;

namespace Car.Data.Migrations
{
    public partial class AddIsOnOwnCar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsOnOwnCar",
                table: "Journey",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOnOwnCar",
                table: "Journey");
        }
    }
}
