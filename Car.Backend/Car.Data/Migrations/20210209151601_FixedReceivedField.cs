using Microsoft.EntityFrameworkCore.Migrations;

namespace Car.Data.Migrations
{
    public partial class FixedReceivedField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_User_ReciverId",
                table: "Chats");

            migrationBuilder.RenameColumn(
                name: "ReciverId",
                table: "Chats",
                newName: "ReceiverId");

            migrationBuilder.RenameIndex(
                name: "IX_Chats_ReciverId",
                table: "Chats",
                newName: "IX_Chats_ReceiverId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_User_ReceiverId",
                table: "Chats",
                column: "ReceiverId",
                principalTable: "Sender",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_User_ReceiverId",
                table: "Chats");

            migrationBuilder.RenameColumn(
                name: "ReceiverId",
                table: "Chats",
                newName: "ReciverId");

            migrationBuilder.RenameIndex(
                name: "IX_Chats_ReceiverId",
                table: "Chats",
                newName: "IX_Chats_ReciverId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_User_ReciverId",
                table: "Chats",
                column: "ReciverId",
                principalTable: "Sender",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
