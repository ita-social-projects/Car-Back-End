using Microsoft.EntityFrameworkCore.Migrations;

namespace Car.Data.Migrations
{
    public partial class AddedUserStopManyToManyRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stop_User_UserId",
                table: "Stop");

            migrationBuilder.DropIndex(
                name: "IX_Stop_UserId",
                table: "Stop");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Stop");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Stop");

            migrationBuilder.CreateTable(
                name: "UserStop",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    StopId = table.Column<int>(type: "int", nullable: false),
                    StopType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserStop", x => new { x.UserId, x.StopId });
                    table.ForeignKey(
                        name: "FK_UserStop_Stop_StopId",
                        column: x => x.StopId,
                        principalTable: "Stop",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserStop_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserStop_StopId",
                table: "UserStop",
                column: "StopId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserStop");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Stop",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Stop",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Stop_UserId",
                table: "Stop",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stop_User_UserId",
                table: "Stop",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
