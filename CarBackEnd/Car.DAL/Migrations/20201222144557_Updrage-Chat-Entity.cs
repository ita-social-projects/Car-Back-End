using Microsoft.EntityFrameworkCore.Migrations;

namespace Car.DAL.Migrations
{
    public partial class UpdrageChatEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NameOfUserId",
                table: "Chats",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Chats_NameOfUserId",
                table: "Chats",
                column: "NameOfUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_User_NameOfUserId",
                table: "Chats",
                column: "NameOfUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_User_NameOfUserId",
                table: "Chats");

            migrationBuilder.DropIndex(
                name: "IX_Chats_NameOfUserId",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "NameOfUserId",
                table: "Chats");
        }
    }
}
