using Microsoft.EntityFrameworkCore.Migrations;

namespace Car.Data.Migrations
{
    public partial class AddedPassangersCountAndRenamedFcmToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FCMToken_User_UserId",
                table: "FCMToken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FCMToken",
                table: "FCMToken");

            migrationBuilder.RenameTable(
                name: "FCMToken",
                newName: "FcmToken");

            migrationBuilder.RenameIndex(
                name: "IX_FCMToken_UserId",
                table: "FcmToken",
                newName: "IX_FcmToken_UserId");

            migrationBuilder.AddColumn<int>(
                name: "PassangersCount",
                table: "JourneyUser",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FcmToken",
                table: "FcmToken",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FcmToken_User_UserId",
                table: "FcmToken",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FcmToken_User_UserId",
                table: "FcmToken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FcmToken",
                table: "FcmToken");

            migrationBuilder.DropColumn(
                name: "PassangersCount",
                table: "JourneyUser");

            migrationBuilder.RenameTable(
                name: "FcmToken",
                newName: "FCMToken");

            migrationBuilder.RenameIndex(
                name: "IX_FcmToken_UserId",
                table: "FCMToken",
                newName: "IX_FCMToken_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FCMToken",
                table: "FCMToken",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FCMToken_User_UserId",
                table: "FCMToken",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
