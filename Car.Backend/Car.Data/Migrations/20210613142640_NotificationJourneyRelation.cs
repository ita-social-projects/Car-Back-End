using Microsoft.EntityFrameworkCore.Migrations;

namespace Car.Data.Migrations
{
    public partial class NotificationJourneyRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "JourneyId",
                table: "Notification",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsCancelled",
                table: "Journey",
                type: "bit",
                nullable: false,
                defaultValue: false);

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
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notification_Journey_JourneyId",
                table: "Notification");

            migrationBuilder.DropIndex(
                name: "IX_Notification_JourneyId",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "JourneyId",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "IsCancelled",
                table: "Journey");
        }
    }
}
