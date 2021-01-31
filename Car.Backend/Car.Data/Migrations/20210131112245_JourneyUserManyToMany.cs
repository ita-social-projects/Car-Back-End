using Microsoft.EntityFrameworkCore.Migrations;

namespace Car.Data.Migrations
{
    public partial class JourneyUserManyToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Journeys_User_DriverId",
                table: "Journeys");

            migrationBuilder.DropIndex(
                name: "IX_Stops_UserId",
                table: "Stops");

            migrationBuilder.RenameColumn(
                name: "DriverId",
                table: "Journeys",
                newName: "OrganizerId");

            migrationBuilder.RenameIndex(
                name: "IX_Journeys_DriverId",
                table: "Journeys",
                newName: "IX_Journeys_OrganizerId");

            migrationBuilder.CreateIndex(
                name: "IX_Stops_UserId",
                table: "Stops",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Journeys_User_OrganizerId",
                table: "Journeys",
                column: "OrganizerId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Journeys_User_OrganizerId",
                table: "Journeys");

            migrationBuilder.DropIndex(
                name: "IX_Stops_UserId",
                table: "Stops");

            migrationBuilder.RenameColumn(
                name: "OrganizerId",
                table: "Journeys",
                newName: "DriverId");

            migrationBuilder.RenameIndex(
                name: "IX_Journeys_OrganizerId",
                table: "Journeys",
                newName: "IX_Journeys_DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Stops_UserId",
                table: "Stops",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Journeys_User_DriverId",
                table: "Journeys",
                column: "DriverId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
