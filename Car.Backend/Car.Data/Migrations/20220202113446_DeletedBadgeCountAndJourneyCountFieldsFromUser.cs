using Microsoft.EntityFrameworkCore.Migrations;

namespace Car.Data.Migrations
{
    public partial class DeletedBadgeCountAndJourneyCountFieldsFromUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Journey_Chat_ChatId",
                table: "Journey");

            migrationBuilder.DropColumn(
                name: "BadgeCount",
                table: "User");

            migrationBuilder.DropColumn(
                name: "JourneyCount",
                table: "User");

            migrationBuilder.AddForeignKey(
                name: "FK_Journey_Chat_ChatId",
                table: "Journey",
                column: "ChatId",
                principalTable: "Chat",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Journey_Chat_ChatId",
                table: "Journey");

            migrationBuilder.AddColumn<int>(
                name: "BadgeCount",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "JourneyCount",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Journey_Chat_ChatId",
                table: "Journey",
                column: "ChatId",
                principalTable: "Chat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
