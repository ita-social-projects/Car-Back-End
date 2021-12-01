using Microsoft.EntityFrameworkCore.Migrations;

namespace Car.Data.Migrations
{
    public partial class ChangeJourneyAddChatId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chat_Journey_Id",
                table: "Chat");

            migrationBuilder.AddColumn<int>(
                name: "ChatId",
                table: "Journey",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Journey_ChatId",
                table: "Journey",
                column: "ChatId");

            migrationBuilder.AddForeignKey(
                name: "FK_Journey_Chat_ChatId",
                table: "Journey",
                column: "ChatId",
                principalTable: "Chat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Journey_Chat_ChatId",
                table: "Journey");

            migrationBuilder.DropIndex(
                name: "IX_Journey_ChatId",
                table: "Journey");

            migrationBuilder.DropColumn(
                name: "ChatId",
                table: "Journey");

            migrationBuilder.AddForeignKey(
                name: "FK_Chat_Journey_Id",
                table: "Chat",
                column: "Id",
                principalTable: "Journey",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
