using Microsoft.EntityFrameworkCore.Migrations;

namespace Car.Data.Migrations
{
    public partial class AddedUserStatisticTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserStatistic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    TotalKm = table.Column<int>(type: "int", nullable: false),
                    PassangerJourneysAmount = table.Column<int>(type: "int", nullable: false),
                    DriverJourneysAmount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserStatistic", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserStatistic_User_Id",
                        column: x => x.Id,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserStatistic");
        }
    }
}
