using Microsoft.EntityFrameworkCore.Migrations;

namespace Car.Data.Migrations
{
    public partial class JourneyStopRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stops_User_UserId",
                table: "Stops");

            migrationBuilder.DropIndex(
                name: "IX_Stops_AddressId",
                table: "Stops");

            migrationBuilder.DropIndex(
                name: "IX_Stops_UserId",
                table: "Stops");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Stops",
                newName: "Type");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Addresses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Stops_AddressId",
                table: "Stops",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_UserId",
                table: "Addresses",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_User_UserId",
                table: "Addresses",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_User_UserId",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Stops_AddressId",
                table: "Stops");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_UserId",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Addresses");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Stops",
                newName: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Stops_AddressId",
                table: "Stops",
                column: "AddressId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stops_UserId",
                table: "Stops",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stops_User_UserId",
                table: "Stops",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
