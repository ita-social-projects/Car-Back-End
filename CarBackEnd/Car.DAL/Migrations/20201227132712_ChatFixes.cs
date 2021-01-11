using Microsoft.EntityFrameworkCore.Migrations;

namespace Car.DAL.Migrations
{
    public partial class ChatFixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_ChatParticipants_ParticipantsId",
                table: "Chats");

            migrationBuilder.DropForeignKey(
                name: "FK_User_ChatParticipants_ChatParticipantsId",
                table: "User");

            migrationBuilder.DropTable(
                name: "ChatParticipants");

            migrationBuilder.DropIndex(
                name: "IX_User_ChatParticipantsId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_Chats_ParticipantsId",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "ChatParticipantsId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ParticipantsId",
                table: "Chats");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChatParticipantsId",
                table: "User",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParticipantsId",
                table: "Chats",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ChatParticipants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatParticipants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatParticipants_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_ChatParticipantsId",
                table: "User",
                column: "ChatParticipantsId");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_ParticipantsId",
                table: "Chats",
                column: "ParticipantsId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatParticipants_UserId",
                table: "ChatParticipants",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_ChatParticipants_ParticipantsId",
                table: "Chats",
                column: "ParticipantsId",
                principalTable: "ChatParticipants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_User_ChatParticipants_ChatParticipantsId",
                table: "User",
                column: "ChatParticipantsId",
                principalTable: "ChatParticipants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
