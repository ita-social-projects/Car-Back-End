using Microsoft.EntityFrameworkCore.Migrations;

namespace Car.Data.Migrations
{
    public partial class RenamedImageAttributes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageAvatar",
                table: "User",
                newName: "ImageId");

            migrationBuilder.RenameColumn(
                name: "ImageCar",
                table: "Cars",
                newName: "ImageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageId",
                table: "User",
                newName: "ImageAvatar");

            migrationBuilder.RenameColumn(
                name: "ImageId",
                table: "Cars",
                newName: "ImageCar");
        }
    }
}
