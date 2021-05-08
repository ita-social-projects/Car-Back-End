using Microsoft.EntityFrameworkCore.Migrations;

namespace Car.Data.Migrations
{
    public partial class NoActionDeleteCar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Journey_Car_CarId",
                table: "Journey");

            migrationBuilder.AddForeignKey(
                name: "FK_Journey_Car_CarId",
                table: "Journey",
                column: "CarId",
                principalTable: "Car",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Journey_Car_CarId",
                table: "Journey");

            migrationBuilder.AddForeignKey(
                name: "FK_Journey_Car_CarId",
                table: "Journey",
                column: "CarId",
                principalTable: "Car",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
