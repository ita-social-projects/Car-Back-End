using Microsoft.EntityFrameworkCore.Migrations;

namespace Car.DAL.Migrations
{
    public partial class UserJourneyRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Journeys_DriverId",
                table: "Journeys");

            migrationBuilder.CreateIndex(
                name: "IX_Journeys_DriverId",
                table: "Journeys",
                column: "DriverId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Journeys_DriverId",
                table: "Journeys");

            migrationBuilder.CreateIndex(
                name: "IX_Journeys_DriverId",
                table: "Journeys",
                column: "DriverId",
                unique: true,
                filter: "[DriverId] IS NOT NULL");
        }
    }
}
