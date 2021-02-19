using Microsoft.EntityFrameworkCore.Migrations;

namespace Car.Data.Migrations
{
    public partial class NotificationDataRenamed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notification_Journey_JourneyId",
                table: "Notification");

            migrationBuilder.DropIndex(
                name: "IX_Notification_JourneyId",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "Data",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "JourneyId",
                table: "Notification");

            migrationBuilder.AddColumn<string>(
                name: "JsonData",
                table: "Notification",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: string.Empty);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JsonData",
                table: "Notification");

            migrationBuilder.AddColumn<string>(
                name: "Data",
                table: "Notification",
                type: "nvarchar(400)",
                maxLength: 400,
                nullable: false,
                defaultValue: string.Empty);

            migrationBuilder.AddColumn<int>(
                name: "JourneyId",
                table: "Notification",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notification_JourneyId",
                table: "Notification",
                column: "JourneyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_Journey_JourneyId",
                table: "Notification",
                column: "JourneyId",
                principalTable: "Journey",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
