using Microsoft.EntityFrameworkCore.Migrations;

namespace Car.Data.Migrations
{
    public partial class AddChildJourneys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "Journey",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Journey_ParentId",
                table: "Journey",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Journey_Schedule_ParentId",
                table: "Journey",
                column: "ParentId",
                principalTable: "Schedule",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Journey_Schedule_ParentId",
                table: "Journey");

            migrationBuilder.DropIndex(
                name: "IX_Journey_ParentId",
                table: "Journey");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Journey");
        }
    }
}
