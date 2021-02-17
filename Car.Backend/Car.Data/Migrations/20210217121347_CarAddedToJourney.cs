using Microsoft.EntityFrameworkCore.Migrations;

namespace Car.Data.Migrations
{
    public partial class CarAddedToJourney : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notification_Journey_JourneyId",
                table: "Notification");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Notification",
                newName: "Data");

            migrationBuilder.AddColumn<int>(
                name: "JourneyCount",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "JourneyId",
                table: "Notification",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CarId",
                table: "Journey",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Journey_CarId",
                table: "Journey",
                column: "CarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Journey_Car_CarId",
                table: "Journey",
                column: "CarId",
                principalTable: "Car",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_Journey_JourneyId",
                table: "Notification",
                column: "JourneyId",
                principalTable: "Journey",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Journey_Car_CarId",
                table: "Journey");

            migrationBuilder.DropForeignKey(
                name: "FK_Notification_Journey_JourneyId",
                table: "Notification");

            migrationBuilder.DropIndex(
                name: "IX_Journey_CarId",
                table: "Journey");

            migrationBuilder.DropColumn(
                name: "JourneyCount",
                table: "User");

            migrationBuilder.DropColumn(
                name: "CarId",
                table: "Journey");

            migrationBuilder.RenameColumn(
                name: "Data",
                table: "Notification",
                newName: "Description");

            migrationBuilder.AlterColumn<int>(
                name: "JourneyId",
                table: "Notification",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_Journey_JourneyId",
                table: "Notification",
                column: "JourneyId",
                principalTable: "Journey",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
