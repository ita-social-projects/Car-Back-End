using Microsoft.EntityFrameworkCore.Migrations;

namespace Car.Data.Migrations
{
    public partial class FixedManyToManyRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Brands_BrandId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Models_ModelId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_User_UserId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Journeys_User_DriverId",
                table: "Journeys");

            migrationBuilder.DropIndex(
                name: "IX_Stops_UserId",
                table: "Stops");

            migrationBuilder.DropIndex(
                name: "IX_Cars_BrandId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "BrandId",
                table: "Cars");

            migrationBuilder.RenameColumn(
                name: "DriverId",
                table: "Journeys",
                newName: "OrganizerId");

            migrationBuilder.RenameIndex(
                name: "IX_Journeys_DriverId",
                table: "Journeys",
                newName: "IX_Journeys_OrganizerId");

            migrationBuilder.RenameColumn(
                name: "ChatName",
                table: "Chats",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Cars",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Cars_UserId",
                table: "Cars",
                newName: "IX_Cars_OwnerId");

            migrationBuilder.AddColumn<bool>(
                name: "HasLuggage",
                table: "UserJourney",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Sender",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450);

            migrationBuilder.CreateIndex(
                name: "IX_Stops_UserId",
                table: "Stops",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Models_ModelId",
                table: "Cars",
                column: "ModelId",
                principalTable: "Models",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_User_OwnerId",
                table: "Cars",
                column: "OwnerId",
                principalTable: "Sender",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Journeys_User_OrganizerId",
                table: "Journeys",
                column: "OrganizerId",
                principalTable: "Sender",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Models_ModelId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_User_OwnerId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Journeys_User_OrganizerId",
                table: "Journeys");

            migrationBuilder.DropIndex(
                name: "IX_Stops_UserId",
                table: "Stops");

            migrationBuilder.DropColumn(
                name: "HasLuggage",
                table: "UserJourney");

            migrationBuilder.RenameColumn(
                name: "OrganizerId",
                table: "Journeys",
                newName: "DriverId");

            migrationBuilder.RenameIndex(
                name: "IX_Journeys_OrganizerId",
                table: "Journeys",
                newName: "IX_Journeys_DriverId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Chats",
                newName: "ChatName");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "Cars",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Cars_OwnerId",
                table: "Cars",
                newName: "IX_Cars_UserId");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Sender",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<int>(
                name: "BrandId",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Stops_UserId",
                table: "Stops",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cars_BrandId",
                table: "Cars",
                column: "BrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Brands_BrandId",
                table: "Cars",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Models_ModelId",
                table: "Cars",
                column: "ModelId",
                principalTable: "Models",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_User_UserId",
                table: "Cars",
                column: "UserId",
                principalTable: "Sender",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Journeys_User_DriverId",
                table: "Journeys",
                column: "DriverId",
                principalTable: "Sender",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
