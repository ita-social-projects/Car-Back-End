using Microsoft.EntityFrameworkCore.Migrations;

namespace Car.Data.Migrations
{
    public partial class AddedBrandsAndModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Brand",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Model",
                table: "Cars");

            migrationBuilder.AddColumn<int>(
                name: "BrandId",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ModelId",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Models",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Models", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Models_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "BMW" },
                    { 17, "Porsche" },
                    { 16, "Mazda" },
                    { 15, "Lexus" },
                    { 14, "Volkswagen" },
                    { 13, "Hyundai" },
                    { 12, "Nissan" },
                    { 11, "ZAZ" },
                    { 18, "Mercedez-Benz" },
                    { 10, "Honda" },
                    { 8, "Skoda" },
                    { 7, "Volvo" },
                    { 6, "Geely" },
                    { 5, "Ford" },
                    { 4, "Fiat" },
                    { 3, "Chevrolet" },
                    { 2, "Audi" },
                    { 9, "Mitsubishi" },
                    { 19, "Toyota" },
                });

            migrationBuilder.InsertData(
                table: "Models",
                columns: new[] { "Id", "BrandId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "1 Series" },
                    { 131, 12, "JUKE" },
                    { 132, 12, "LEAF" },
                    { 133, 12, "Maxima" },
                    { 134, 12, "Murano" },
                    { 135, 13, "Accent" },
                    { 136, 13, "Azera" },
                    { 137, 13, "Elantra" },
                    { 138, 13, "Entourage" },
                    { 139, 13, "Equus" },
                    { 140, 13, "Genesis" },
                    { 141, 13, "Genesis Coupe" },
                    { 142, 13, "Ioniq Electric" },
                    { 143, 13, "Kona" },
                    { 144, 13, "NEXO" },
                    { 145, 14, "Arteon" },
                    { 146, 14, "Beetle" },
                    { 147, 14, "CC" },
                    { 148, 14, "Eos" },
                    { 149, 14, "GTI" },
                    { 150, 14, "Golf" },
                    { 151, 14, "Jetta" },
                    { 130, 12, "GT-R" },
                    { 129, 12, "Frontier King Cab" },
                    { 128, 12, "Armada" },
                    { 127, 12, "Altima" },
                    { 105, 10, "Accord" },
                    { 106, 10, "Accord Hybrid" },
                    { 107, 10, "CR-V" },
                    { 108, 10, "Civic" },
                    { 109, 10, "Element" },
                    { 110, 10, "Fit" },
                    { 111, 10, "HR-V" },
                    { 112, 10, "Insight" },
                    { 113, 10, "Odyssey" },
                    { 114, 10, "Passport" },
                    { 152, 14, "New Beetle" },
                    { 115, 11, "Tavria" },
                    { 117, 11, "Lanos" },
                    { 118, 11, "Forza" },
                    { 119, 11, "Vida" },
                    { 120, 11, "Sens" },
                });

            migrationBuilder.InsertData(
                table: "Models",
                columns: new[] { "Id", "BrandId", "Name" },
                values: new object[,]
                {
                    { 121, 11, "Pick-up" },
                    { 122, 11, "Dana" },
                    { 123, 11, "Vida Cargo" },
                    { 124, 11, "Lanos Cargo" },
                    { 125, 12, "350Z" },
                    { 126, 12, "370Z" },
                    { 116, 11, "Slavuta" },
                    { 104, 9, "Raider Double Cab" },
                    { 153, 14, "Passat" },
                    { 155, 15, "ES" },
                    { 182, 17, "718 Boxster" },
                    { 183, 17, "718 Cayman" },
                    { 184, 17, "Cayenne Coupe" },
                    { 185, 18, "A-Class" },
                    { 186, 18, "B-Class" },
                    { 187, 18, "C-Class" },
                    { 188, 18, "CL-Class" },
                    { 189, 18, "CLS" },
                    { 190, 18, "E-Class" },
                    { 191, 18, "G-Class" },
                    { 192, 18, "GLA" },
                    { 193, 18, "M-Class" },
                    { 194, 18, "S-Class" },
                    { 195, 19, "4Runner" },
                    { 196, 19, "Avalon" },
                    { 197, 19, "86" },
                    { 198, 19, "Camry" },
                    { 199, 19, "Camry Hybrid" },
                    { 200, 19, "Corolla" },
                    { 201, 19, "Highlander" },
                    { 202, 19, "Land Cruiser" },
                    { 181, 17, "Taycan" },
                    { 180, 17, "Panamera" },
                    { 179, 17, "Macan" },
                    { 178, 17, "Cayman" },
                    { 156, 15, "CT" },
                    { 157, 15, "GS" },
                    { 158, 15, "GX" },
                    { 159, 15, "IS" },
                    { 160, 15, "LS" },
                    { 161, 15, "LC" },
                    { 162, 15, "RC" },
                });

            migrationBuilder.InsertData(
                table: "Models",
                columns: new[] { "Id", "BrandId", "Name" },
                values: new object[,]
                {
                    { 163, 15, "LX" },
                    { 164, 15, "SC" },
                    { 165, 16, "CX-3" },
                    { 154, 14, "Rabbit" },
                    { 166, 16, "CX-30" },
                    { 168, 16, "CX-7" },
                    { 169, 16, "CX-9" },
                    { 170, 16, "MAZDA2" },
                    { 171, 16, "MAZDA3" },
                    { 172, 16, "MAZDA5" },
                    { 173, 16, "MAZDA6" },
                    { 174, 16, "MX-5 Miata" },
                    { 175, 17, "911" },
                    { 176, 17, "Boxster" },
                    { 177, 17, "Cayenne" },
                    { 167, 16, "CX-5" },
                    { 103, 9, "i-MiEV" },
                    { 102, 9, "Outlander" },
                    { 101, 9, "Mirage" },
                    { 28, 2, "A5" },
                    { 29, 2, "A6" },
                    { 30, 2, "A7" },
                    { 31, 2, "A8" },
                    { 32, 2, "Q3" },
                    { 33, 2, "Q5" },
                    { 34, 2, "Q7" },
                    { 35, 2, "Q8" },
                    { 36, 3, "Aveo" },
                    { 37, 3, "Avalanche 1500" },
                    { 38, 3, "Avalanche" },
                    { 39, 3, "Avalanche 2500" },
                    { 40, 3, "Cavalier" },
                    { 41, 3, "Classic" },
                    { 42, 3, "Cobalt" },
                    { 43, 3, "Colorado Crew Cab" },
                    { 44, 3, "Corvette" },
                    { 45, 3, "Cruze" },
                    { 46, 3, "Equinox" },
                    { 47, 4, "124 Spider" },
                    { 48, 4, "500" },
                    { 27, 2, "A4" },
                    { 26, 2, "A3" },
                });

            migrationBuilder.InsertData(
                table: "Models",
                columns: new[] { "Id", "BrandId", "Name" },
                values: new object[,]
                {
                    { 25, 2, "90" },
                    { 24, 2, "80" },
                    { 2, 1, "2 Series" },
                    { 3, 1, "3 Series" },
                    { 4, 1, "4 Series" },
                    { 5, 1, "5 Series" },
                    { 6, 1, "6 Series" },
                    { 7, 1, "7 Series" },
                    { 8, 1, "8 Series" },
                    { 9, 1, "M" },
                    { 10, 1, "M2" },
                    { 11, 1, "M3" },
                    { 49, 4, "500 Abarth" },
                    { 12, 1, "M4" },
                    { 14, 1, "M6" },
                    { 15, 1, "X1" },
                    { 16, 1, "X2" },
                    { 17, 1, "X3" },
                    { 18, 1, "X4" },
                    { 19, 1, "X5" },
                    { 20, 1, "X6" },
                    { 21, 1, "X6 M" },
                    { 22, 1, "X7" },
                    { 23, 2, "100" },
                    { 13, 1, "M5" },
                    { 50, 4, "500L" },
                    { 51, 4, "500X" },
                    { 52, 4, "500c" },
                    { 79, 7, "S70" },
                    { 80, 7, "S80" },
                    { 81, 7, "S90" },
                    { 82, 7, "V40" },
                    { 83, 7, "V50" },
                    { 84, 7, "V60" },
                    { 85, 8, "Fabia" },
                    { 86, 8, "SUPERB" },
                    { 87, 8, "OCTAVIA" },
                    { 88, 8, "KAROQ" },
                    { 78, 7, "S60" },
                    { 89, 8, "KODIAQ" },
                    { 91, 8, "KAMIQ" },
                    { 92, 8, "Rapid TSI" },
                });

            migrationBuilder.InsertData(
                table: "Models",
                columns: new[] { "Id", "BrandId", "Name" },
                values: new object[,]
                {
                    { 93, 8, "New Octavia" },
                    { 94, 8, "Vision In Concept" },
                    { 95, 9, "Diamante" },
                    { 96, 9, "Eclipse" },
                    { 97, 9, "Expo" },
                    { 98, 9, "Galant" },
                    { 99, 9, "Endeavor" },
                    { 100, 9, "Lancer" },
                    { 90, 8, "SCALA" },
                    { 203, 19, "Mirai" },
                    { 77, 7, "S40" },
                    { 75, 7, "C30" },
                    { 53, 4, "500e" },
                    { 54, 5, "E150 Cargo" },
                    { 55, 5, "E150 Passenger" },
                    { 56, 5, "E150 Super Duty Cargo" },
                    { 57, 5, "E250 Cargo" },
                    { 58, 5, "E350 Super Duty Cargo" },
                    { 59, 5, "Econoline E150 Cargo" },
                    { 60, 5, "Econoline E250 Cargo" },
                    { 61, 5, "Edge" },
                    { 62, 5, "Escape" },
                    { 76, 7, "C70" },
                    { 63, 5, "Escort" },
                    { 65, 6, "ATLAS" },
                    { 66, 6, "GC6" },
                    { 67, 6, "GX7" },
                    { 68, 6, "MK" },
                    { 69, 6, "EMGRAND" },
                    { 70, 6, "EMGRAND 7" },
                    { 71, 6, "EMGRAND EC7" },
                    { 72, 6, "EMGRAND GS" },
                    { 73, 6, "TUGELLA" },
                    { 74, 6, "PREFACE" },
                    { 64, 5, "Excursion" },
                    { 204, 19, "Prius" },
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_BrandId",
                table: "Cars",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_ModelId",
                table: "Cars",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Models_BrandId",
                table: "Models",
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Brands_BrandId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Models_ModelId",
                table: "Cars");

            migrationBuilder.DropTable(
                name: "Models");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropIndex(
                name: "IX_Cars_BrandId",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_ModelId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "BrandId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "ModelId",
                table: "Cars");

            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
