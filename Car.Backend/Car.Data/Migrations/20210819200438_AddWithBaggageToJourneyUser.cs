using Microsoft.EntityFrameworkCore.Migrations;

namespace Car.Data.Migrations
{
    public partial class AddWithBaggageToJourneyUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JourneyUser_Journey_ParticipantJourneysId",
                table: "JourneyUser");

            migrationBuilder.DropForeignKey(
                name: "FK_JourneyUser_User_ParticipantsId",
                table: "JourneyUser");

            migrationBuilder.RenameColumn(
                name: "ParticipantsId",
                table: "JourneyUser",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "ParticipantJourneysId",
                table: "JourneyUser",
                newName: "JourneyId");

            migrationBuilder.RenameIndex(
                name: "IX_JourneyUser_ParticipantsId",
                table: "JourneyUser",
                newName: "IX_JourneyUser_UserId");

            migrationBuilder.AddColumn<bool>(
                name: "WithBaggage",
                table: "JourneyUser",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_JourneyUser_Journey_JourneyId",
                table: "JourneyUser",
                column: "JourneyId",
                principalTable: "Journey",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JourneyUser_User_UserId",
                table: "JourneyUser",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JourneyUser_Journey_JourneyId",
                table: "JourneyUser");

            migrationBuilder.DropForeignKey(
                name: "FK_JourneyUser_User_UserId",
                table: "JourneyUser");

            migrationBuilder.DropColumn(
                name: "WithBaggage",
                table: "JourneyUser");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "JourneyUser",
                newName: "ParticipantsId");

            migrationBuilder.RenameColumn(
                name: "JourneyId",
                table: "JourneyUser",
                newName: "ParticipantJourneysId");

            migrationBuilder.RenameIndex(
                name: "IX_JourneyUser_UserId",
                table: "JourneyUser",
                newName: "IX_JourneyUser_ParticipantsId");

            migrationBuilder.AddForeignKey(
                name: "FK_JourneyUser_Journey_ParticipantJourneysId",
                table: "JourneyUser",
                column: "ParticipantJourneysId",
                principalTable: "Journey",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JourneyUser_User_ParticipantsId",
                table: "JourneyUser",
                column: "ParticipantsId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
