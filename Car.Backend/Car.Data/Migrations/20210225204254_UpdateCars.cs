using Microsoft.EntityFrameworkCore.Migrations;

namespace Car.Data.Migrations
{
    public partial class UpdateCars : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Acura");

            migrationBuilder.UpdateData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Alfa Romeo");

            migrationBuilder.UpdateData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Aston Martin");

            migrationBuilder.UpdateData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Audi");

            migrationBuilder.UpdateData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "BMW");

            migrationBuilder.UpdateData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "Bentley");

            migrationBuilder.UpdateData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: 7,
                column: "Name",
                value: "Buick");

            migrationBuilder.UpdateData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: 8,
                column: "Name",
                value: "Cadillac");

            migrationBuilder.UpdateData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: 9,
                column: "Name",
                value: "Chevrolet");

            migrationBuilder.UpdateData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: 10,
                column: "Name",
                value: "Chrysler");

            migrationBuilder.UpdateData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: 11,
                column: "Name",
                value: "Daewoo");

            migrationBuilder.UpdateData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: 12,
                column: "Name",
                value: "Daihatsu");

            migrationBuilder.UpdateData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: 13,
                column: "Name",
                value: "Dodge");

            migrationBuilder.UpdateData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: 14,
                column: "Name",
                value: "Eagle");

            migrationBuilder.UpdateData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: 15,
                column: "Name",
                value: "FIAT");

            migrationBuilder.UpdateData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: 16,
                column: "Name",
                value: "Ferrari");

            migrationBuilder.UpdateData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: 17,
                column: "Name",
                value: "Fisker");

            migrationBuilder.UpdateData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: 18,
                column: "Name",
                value: "Ford");

            migrationBuilder.UpdateData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: 19,
                column: "Name",
                value: "Freightliner");

            migrationBuilder.InsertData(
                table: "Brand",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 21, "Genesis" },
                    { 62, "smart" },
                    { 61, "Volvo" },
                    { 60, "Volkswagen" },
                    { 59, "Toyota" },
                    { 58, "Tesla" },
                    { 57, "Suzuki" },
                    { 56, "Subaru" },
                    { 55, "Scion" },
                    { 54, "Saturn" },
                    { 53, "Saab" },
                    { 52, "SRT" },
                    { 22, "Geo" },
                    { 50, "Ram" },
                    { 23, "HUMMER" },
                    { 51, "Rolls-Royce" },
                    { 25, "Hyundai" },
                    { 26, "INFINITI" },
                    { 27, "Isuzu" },
                    { 28, "Jaguar" },
                    { 29, "Jeep" },
                    { 30, "Kia" },
                    { 31, "Lamborghini" }
                });

            migrationBuilder.InsertData(
                table: "Brand",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 32, "Land Rover" },
                    { 33, "Lexus" },
                    { 34, "Lincoln" },
                    { 35, "Lotus" },
                    { 36, "MAZDA" },
                    { 24, "Honda" },
                    { 38, "Maserati" },
                    { 37, "MINI" },
                    { 49, "Porsche" },
                    { 48, "Pontiac" },
                    { 47, "Plymouth" },
                    { 46, "Panoz" },
                    { 45, "Oldsmobile" },
                    { 20, "GMC" },
                    { 43, "Mitsubishi" },
                    { 42, "Mercury" },
                    { 41, "Mercedes-Benz" },
                    { 40, "McLaren" },
                    { 44, "Nissan" },
                    { 39, "Maybach" }
                });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "CL");

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "ILX");

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Integra");

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Legend");

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "MDX");

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "MDX Sport Hybrid");

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 7,
                column: "Name",
                value: "NSX");

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 8,
                column: "Name",
                value: "RDX");

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 9,
                column: "Name",
                value: "RL");

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 10,
                column: "Name",
                value: "RLX");

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 11,
                column: "Name",
                value: "RLX Sport Hybrid");

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 12,
                column: "Name",
                value: "RSX");

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 13,
                column: "Name",
                value: "SLX");

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 14,
                column: "Name",
                value: "TL");

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 15,
                column: "Name",
                value: "TLX");

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 16,
                column: "Name",
                value: "TSX");

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 17,
                column: "Name",
                value: "Vigor");

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 18,
                column: "Name",
                value: "ZDX");

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 2, "164" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 2, "4C" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 2, "4C Spider" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 2, "Giulia" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 23,
                column: "Name",
                value: "Spider");

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 24,
                column: "Name",
                value: "Stelvio");

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 3, "DB11" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 3, "DB9" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 3, "DB9 GT" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 3, "DBS" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 3, "Rapide" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 3, "Rapide S" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 3, "Vanquish" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 3, "Vanquish S" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 3, "Vantage" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 3, "Virage" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 4, "100" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 4, "80" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 4, "90" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 4, "A3" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 4, "A3 Sportback e-tron" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 4, "A4" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 4, "A4 (2005.5)" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 4, "A4 allroad" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 4, "A5" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 44,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 4, "A5 Sport" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 45,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 4, "A6" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 46,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 4, "A7" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 47,
                column: "Name",
                value: "A8");

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 48,
                column: "Name",
                value: "Cabriolet");

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 49,
                column: "Name",
                value: "Q3");

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 50,
                column: "Name",
                value: "Q5");

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 51,
                column: "Name",
                value: "Q7");

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 52,
                column: "Name",
                value: "Q8");

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 53,
                column: "Name",
                value: "Quattro");

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 54,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 4, "R8" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 55,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 4, "RS 3" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 56,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 4, "RS 4" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 57,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 4, "RS 5" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 58,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 4, "RS 6" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 59,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 4, "RS 7" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 60,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 4, "S3" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 61,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 4, "S4" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 62,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 4, "S4 (2005.5)" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 63,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 4, "S5" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 64,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 4, "S6" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 65,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 4, "S7" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 66,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 4, "S8" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 67,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 4, "SQ5" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 68,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 4, "TT" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 69,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 4, "allroad" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 70,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 4, "e-tron" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 71,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 5, "1 Series" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 72,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 5, "2 Series" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 73,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 5, "3 Series" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 74,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 5, "4 Series" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 75,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 5, "5 Series" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 76,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 5, "6 Series" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 77,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 5, "7 Series" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 78,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 5, "8 Series" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 79,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 5, "Alpina B7" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 80,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 5, "M" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 81,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 5, "M2" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 82,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 5, "M3" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 83,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 5, "M4" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 84,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 5, "M5" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 85,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 5, "M6" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 86,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 5, "X1" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 87,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 5, "X2" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 88,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 5, "X3" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 89,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 5, "X4" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 90,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 5, "X5" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 91,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 5, "X5 M" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 92,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 5, "X6" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 93,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 5, "X6 M" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 94,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 5, "X7" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 95,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 5, "Z3" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 96,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 5, "Z4" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 97,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 5, "Z4 M" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 98,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 5, "Z8" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 99,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 5, "i3" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 100,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 5, "i8" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 101,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 6, "Arnage" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 102,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 6, "Azure" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 103,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 6, "Azure T" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 104,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 6, "Bentayga" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 105,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 6, "Brooklands" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 106,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 6, "Continental" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 107,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 6, "Flying Spur" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 108,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 6, "Mulsanne" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 109,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 7, "Cascada" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 110,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 7, "Century" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 111,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 7, "Enclave" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 112,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 7, "Encore" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 113,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 7, "Envision" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 114,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 7, "LaCrosse" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 115,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 7, "LeSabre" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 116,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 7, "Lucerne" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 117,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 7, "Park Avenue" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 118,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 7, "Rainier" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 119,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 7, "Regal" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 120,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 7, "Regal Sportback" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 121,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 7, "Regal TourX" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 122,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 7, "Rendezvous" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 123,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 7, "Riviera" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 124,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 7, "Roadmaster" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 125,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 7, "Skylark" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 126,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 7, "Terraza" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 127,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 7, "Verano" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 128,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 8, "ATS" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 129,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 8, "ATS-V" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 130,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 8, "Allante" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 131,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 8, "Brougham" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 132,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 8, "CT5" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 133,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 8, "CT6" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 134,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 8, "CT6-V" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 135,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 8, "CTS" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 136,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 8, "CTS-V" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 137,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 8, "Catera" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 138,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 8, "DTS" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 139,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 8, "DeVille" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 140,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 8, "ELR" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 141,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 8, "Eldorado" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 142,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 8, "Escalade" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 143,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 8, "Escalade ESV" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 144,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 8, "Escalade EXT" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 145,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 8, "Fleetwood" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 146,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 8, "SRX" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 147,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 8, "STS" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 148,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 8, "Seville" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 149,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 8, "Sixty Special" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 150,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 8, "XLR" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 151,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 8, "XT4" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 152,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 8, "XT5" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 153,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 8, "XT6" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 154,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 8, "XTS" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 155,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 9, "1500 Extended Cab" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 156,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 9, "1500 Regular Cab" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 157,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 9, "2500 Crew Cab" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 158,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 9, "2500 Extended Cab" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 159,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 9, "2500 HD Extended Cab" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 160,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 9, "2500 HD Regular Cab" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 161,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 9, "2500 Regular Cab" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 162,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 9, "3500 Crew Cab" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 163,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 9, "3500 Extended Cab" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 164,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 9, "3500 HD Extended Cab" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 165,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 9, "3500 HD Regular Cab" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 166,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 9, "3500 Regular Cab" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 167,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 9, "APV Cargo" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 168,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 9, "Astro Cargo" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 169,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 9, "Astro Passenger" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 170,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 9, "Avalanche" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 171,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 9, "Avalanche 1500" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 172,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 9, "Avalanche 2500" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 173,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 9, "Aveo" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 174,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 9, "Beretta" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 175,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 9, "Blazer" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 176,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 9, "Bolt EV" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 177,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 9, "Camaro" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 178,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 9, "Caprice" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 179,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 9, "Caprice Classic" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 180,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 9, "Captiva Sport" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 181,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 9, "Cavalier" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 182,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 9, "City Express" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 183,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 9, "Classic" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 184,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 9, "Cobalt" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 185,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 9, "Colorado Crew Cab" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 186,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 9, "Colorado Extended Cab" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 187,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 9, "Colorado Regular Cab" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 188,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 9, "Corsica" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 189,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 9, "Corvette" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 190,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 9, "Cruze" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 191,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 9, "Cruze Limited" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 192,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 9, "Equinox" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 193,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 9, "Express 1500 Cargo" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 194,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 9, "Express 1500 Passenger" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 195,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 9, "Express 2500 Cargo" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 196,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 9, "Express 2500 Passenger" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 197,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 9, "Express 3500 Cargo" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 198,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 9, "Express 3500 Passenger" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 199,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 9, "G-Series 1500" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 200,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 9, "G-Series 2500" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 201,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 9, "G-Series 3500" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 202,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 9, "G-Series G10" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 203,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 9, "G-Series G20" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 204,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 9, "G-Series G30" });

            migrationBuilder.InsertData(
                table: "Model",
                columns: new[] { "Id", "BrandId", "Name" },
                values: new object[,]
                {
                    { 428, 18, "Explorer Sport" },
                    { 306, 13, "Colt" },
                    { 305, 13, "Charger" },
                    { 304, 13, "Challenger" },
                    { 303, 13, "Caravan Passenger" },
                    { 302, 13, "Caravan Cargo" },
                    { 301, 13, "Caliber" },
                    { 300, 13, "Avenger" },
                    { 299, 12, "Rocky" },
                    { 298, 12, "Charade" },
                    { 297, 11, "Nubira" },
                    { 296, 11, "Leganza" },
                    { 295, 11, "Lanos" },
                    { 294, 10, "Voyager" },
                    { 293, 10, "Town & Country" },
                    { 292, 10, "Sebring" },
                    { 291, 10, "Prowler" },
                    { 290, 10, "Pacifica Hybrid" },
                    { 276, 10, "300" },
                    { 277, 10, "300M" },
                    { 278, 10, "Aspen" },
                    { 279, 10, "Cirrus" },
                    { 280, 10, "Concorde" },
                    { 281, 10, "Crossfire" },
                    { 307, 13, "D150 Club Cab" },
                    { 282, 10, "Fifth Ave" },
                    { 284, 10, "Imperial" },
                    { 285, 10, "LHS" }
                });

            migrationBuilder.InsertData(
                table: "Model",
                columns: new[] { "Id", "BrandId", "Name" },
                values: new object[,]
                {
                    { 286, 10, "LeBaron" },
                    { 287, 10, "New Yorker" },
                    { 288, 10, "PT Cruiser" },
                    { 289, 10, "Pacifica" },
                    { 283, 10, "Grand Voyager" },
                    { 308, 13, "D150 Regular Cab" },
                    { 309, 13, "D250 Club Cab" },
                    { 310, 13, "D250 Regular Cab" },
                    { 329, 13, "Nitro" },
                    { 330, 13, "Ram 1500 Club Cab" },
                    { 331, 13, "Ram 1500 Crew Cab" },
                    { 332, 13, "Ram 1500 Mega Cab" },
                    { 333, 13, "Ram 1500 Quad Cab" },
                    { 334, 13, "Ram 1500 Regular Cab" },
                    { 328, 13, "Neon" },
                    { 335, 13, "Ram 2500 Club Cab" },
                    { 337, 13, "Ram 2500 Mega Cab" },
                    { 338, 13, "Ram 2500 Quad Cab" },
                    { 339, 13, "Ram 2500 Regular Cab" },
                    { 340, 13, "Ram 3500 Club Cab" },
                    { 341, 13, "Ram 3500 Crew Cab" },
                    { 342, 13, "Ram 3500 Mega Cab" },
                    { 336, 13, "Ram 2500 Crew Cab" },
                    { 275, 10, "200" },
                    { 327, 13, "Monaco" },
                    { 325, 13, "Journey" },
                    { 311, 13, "D350 Club Cab" },
                    { 312, 13, "D350 Regular Cab" },
                    { 313, 13, "Dakota Club Cab" },
                    { 314, 13, "Dakota Crew Cab" },
                    { 315, 13, "Dakota Extended Cab" },
                    { 316, 13, "Dakota Quad Cab" },
                    { 326, 13, "Magnum" },
                    { 317, 13, "Dakota Regular Cab" },
                    { 319, 13, "Daytona" },
                    { 320, 13, "Durango" },
                    { 321, 13, "Dynasty" },
                    { 322, 13, "Grand Caravan Cargo" },
                    { 323, 13, "Grand Caravan Passenger" },
                    { 324, 13, "Intrepid" },
                    { 318, 13, "Dart" },
                    { 343, 13, "Ram 3500 Quad Cab" }
                });

            migrationBuilder.InsertData(
                table: "Model",
                columns: new[] { "Id", "BrandId", "Name" },
                values: new object[,]
                {
                    { 274, 9, "Volt" },
                    { 272, 9, "Venture Cargo" },
                    { 235, 9, "Silverado 1500 Double Cab" },
                    { 234, 9, "Silverado 1500 Crew Cab" },
                    { 233, 9, "Silverado (Classic) 3500 Regular Cab" },
                    { 232, 9, "Silverado (Classic) 3500 Extended Cab" },
                    { 231, 9, "Silverado (Classic) 3500 Crew Cab" },
                    { 230, 9, "Silverado (Classic) 2500 HD Regular Cab" },
                    { 229, 9, "Silverado (Classic) 2500 HD Extended Cab" },
                    { 228, 9, "Silverado (Classic) 2500 HD Crew Cab" },
                    { 227, 9, "Silverado (Classic) 1500 Regular Cab" },
                    { 226, 9, "Silverado (Classic) 1500 HD Crew Cab" },
                    { 225, 9, "Silverado (Classic) 1500 Extended Cab" },
                    { 224, 9, "Silverado (Classic) 1500 Crew Cab" },
                    { 223, 9, "SSR" },
                    { 222, 9, "SS" },
                    { 221, 9, "S10 Regular Cab" },
                    { 220, 9, "S10 Extended Cab" },
                    { 219, 9, "S10 Crew Cab" },
                    { 205, 9, "HHR" },
                    { 206, 9, "Impala" },
                    { 207, 9, "Impala Limited" },
                    { 208, 9, "Lumina" },
                    { 209, 9, "Lumina APV" },
                    { 210, 9, "Lumina Cargo" },
                    { 236, 9, "Silverado 1500 Extended Cab" },
                    { 211, 9, "Lumina Passenger" },
                    { 213, 9, "Malibu (Classic)" },
                    { 214, 9, "Malibu Limited" },
                    { 215, 9, "Metro" },
                    { 216, 9, "Monte Carlo" },
                    { 217, 9, "Prizm" },
                    { 218, 9, "S10 Blazer" },
                    { 212, 9, "Malibu" },
                    { 237, 9, "Silverado 1500 HD Crew Cab" },
                    { 238, 9, "Silverado 1500 LD Double Cab" },
                    { 239, 9, "Silverado 1500 Regular Cab" },
                    { 258, 9, "Sportvan G20" },
                    { 259, 9, "Sportvan G30" },
                    { 260, 9, "Suburban" },
                    { 261, 9, "Suburban 1500" },
                    { 262, 9, "Suburban 2500" }
                });

            migrationBuilder.InsertData(
                table: "Model",
                columns: new[] { "Id", "BrandId", "Name" },
                values: new object[,]
                {
                    { 263, 9, "Suburban 3500HD" },
                    { 257, 9, "Sportvan G10" },
                    { 264, 9, "Tahoe" },
                    { 266, 9, "Tracker" },
                    { 267, 9, "TrailBlazer" },
                    { 268, 9, "Traverse" },
                    { 269, 9, "Trax" },
                    { 270, 9, "Uplander Cargo" },
                    { 271, 9, "Uplander Passenger" },
                    { 265, 9, "Tahoe (New)" },
                    { 273, 9, "Venture Passenger" },
                    { 256, 9, "Spark EV" },
                    { 254, 9, "Sonic" },
                    { 240, 9, "Silverado 2500 Crew Cab" },
                    { 241, 9, "Silverado 2500 Extended Cab" },
                    { 242, 9, "Silverado 2500 HD Crew Cab" },
                    { 243, 9, "Silverado 2500 HD Double Cab" },
                    { 244, 9, "Silverado 2500 HD Extended Cab" },
                    { 245, 9, "Silverado 2500 HD Regular Cab" },
                    { 255, 9, "Spark" },
                    { 246, 9, "Silverado 2500 Regular Cab" },
                    { 248, 9, "Silverado 3500 Extended Cab" },
                    { 249, 9, "Silverado 3500 HD Crew Cab" },
                    { 250, 9, "Silverado 3500 HD Double Cab" },
                    { 251, 9, "Silverado 3500 HD Extended Cab" },
                    { 252, 9, "Silverado 3500 HD Regular Cab" },
                    { 253, 9, "Silverado 3500 Regular Cab" },
                    { 247, 9, "Silverado 3500 Crew Cab" },
                    { 427, 18, "Explorer" },
                    { 344, 13, "Ram 3500 Regular Cab" },
                    { 346, 13, "Ram Van 1500" },
                    { 465, 18, "Ranger SuperCrew" },
                    { 466, 18, "Taurus" },
                    { 467, 18, "Taurus X" },
                    { 468, 18, "Tempo" },
                    { 469, 18, "Thunderbird" },
                    { 470, 18, "Transit 150 Van" },
                    { 471, 18, "Transit 150 Wagon" },
                    { 472, 18, "Transit 250 Van" },
                    { 473, 18, "Transit 350 HD Van" },
                    { 474, 18, "Transit 350 Van" },
                    { 475, 18, "Transit 350 Wagon" }
                });

            migrationBuilder.InsertData(
                table: "Model",
                columns: new[] { "Id", "BrandId", "Name" },
                values: new object[,]
                {
                    { 476, 18, "Transit Connect Cargo" },
                    { 477, 18, "Transit Connect Passenger" },
                    { 478, 18, "Windstar Cargo" },
                    { 479, 18, "Windstar Passenger" },
                    { 480, 18, "ZX2" },
                    { 481, 19, "Sprinter 2500 Cargo" },
                    { 418, 18, "Econoline E350 Super Duty Cargo" },
                    { 419, 18, "Econoline E350 Super Duty Passenger" },
                    { 420, 18, "Edge" },
                    { 421, 18, "Escape" },
                    { 422, 18, "Escort" },
                    { 423, 18, "Excursion" },
                    { 464, 18, "Ranger SuperCab" },
                    { 424, 18, "Expedition" },
                    { 487, 19, "Sprinter WORKER Passenger" },
                    { 486, 19, "Sprinter WORKER Cargo" },
                    { 485, 19, "Sprinter 3500XD Cargo" },
                    { 484, 19, "Sprinter 3500 Cargo" },
                    { 483, 19, "Sprinter 2500 Passenger" },
                    { 482, 19, "Sprinter 2500 Crew" },
                    { 425, 18, "Expedition EL" },
                    { 463, 18, "Ranger Super Cab" },
                    { 462, 18, "Ranger Regular Cab" },
                    { 461, 18, "Probe" },
                    { 442, 18, "F350 Regular Cab" },
                    { 441, 18, "F350 Crew Cab" },
                    { 440, 18, "F250 Super Duty Super Cab" },
                    { 439, 18, "F250 Super Duty Regular Cab" },
                    { 438, 18, "F250 Super Duty Crew Cab" },
                    { 437, 18, "F250 Super Cab" },
                    { 443, 18, "F350 Super Cab" },
                    { 426, 18, "Expedition MAX" },
                    { 434, 18, "F150 SuperCrew Cab" },
                    { 433, 18, "F150 Super Cab" },
                    { 432, 18, "F150 Regular Cab" },
                    { 431, 18, "F150 (Heritage) Super Cab" },
                    { 430, 18, "F150 (Heritage) Regular Cab" },
                    { 429, 18, "Explorer Sport Trac" },
                    { 435, 18, "F250 Crew Cab" },
                    { 417, 18, "Econoline E350 Cargo" },
                    { 444, 18, "F350 Super Duty Crew Cab" },
                    { 446, 18, "F350 Super Duty Super Cab" }
                });

            migrationBuilder.InsertData(
                table: "Model",
                columns: new[] { "Id", "BrandId", "Name" },
                values: new object[,]
                {
                    { 460, 18, "Mustang" },
                    { 459, 18, "GT" },
                    { 458, 18, "Fusion Energi" },
                    { 457, 18, "Fusion" },
                    { 456, 18, "Freestyle" },
                    { 455, 18, "Freestar Passenger" },
                    { 445, 18, "F350 Super Duty Regular Cab" },
                    { 454, 18, "Freestar Cargo" },
                    { 452, 18, "Focus" },
                    { 451, 18, "Flex" },
                    { 450, 18, "Five Hundred" },
                    { 449, 18, "Fiesta" },
                    { 448, 18, "Festiva" },
                    { 447, 18, "F450 Super Duty Crew Cab" },
                    { 453, 18, "Focus ST" },
                    { 345, 13, "Ram 50 Regular Cab" },
                    { 416, 18, "Econoline E250 Cargo" },
                    { 414, 18, "Econoline E150 Cargo" },
                    { 377, 15, "500c Abarth" },
                    { 376, 15, "500c" },
                    { 375, 15, "500X" },
                    { 374, 15, "500L" },
                    { 373, 15, "500 Abarth" },
                    { 372, 15, "500" },
                    { 371, 15, "124 Spider" },
                    { 370, 14, "Vision" },
                    { 369, 14, "Talon" },
                    { 368, 14, "Summit" },
                    { 367, 14, "Premier" },
                    { 366, 13, "Viper" },
                    { 365, 13, "Stratus" },
                    { 364, 13, "Stealth" },
                    { 363, 13, "Sprinter 3500 Cargo" },
                    { 362, 13, "Sprinter 2500 Passenger" },
                    { 361, 13, "Sprinter 2500 Cargo" },
                    { 347, 13, "Ram Van 2500" },
                    { 348, 13, "Ram Van 3500" },
                    { 349, 13, "Ram Van B150" },
                    { 350, 13, "Ram Van B250" },
                    { 351, 13, "Ram Van B350" },
                    { 352, 13, "Ram Wagon 1500" },
                    { 378, 15, "500e" }
                });

            migrationBuilder.InsertData(
                table: "Model",
                columns: new[] { "Id", "BrandId", "Name" },
                values: new object[,]
                {
                    { 353, 13, "Ram Wagon 2500" },
                    { 355, 13, "Ram Wagon B150" },
                    { 356, 13, "Ram Wagon B250" },
                    { 357, 13, "Ram Wagon B350" },
                    { 358, 13, "Ramcharger" },
                    { 359, 13, "Shadow" },
                    { 360, 13, "Spirit" },
                    { 354, 13, "Ram Wagon 3500" },
                    { 379, 16, "430 Scuderia" },
                    { 380, 16, "458 Italia" },
                    { 381, 16, "458 Speciale" },
                    { 400, 18, "C-MAX Energi" },
                    { 401, 18, "C-MAX Hybrid" },
                    { 402, 18, "Club Wagon" },
                    { 403, 18, "Contour" },
                    { 404, 18, "Crown Victoria" },
                    { 405, 18, "E150 Cargo" },
                    { 399, 18, "Bronco" },
                    { 406, 18, "E150 Passenger" },
                    { 408, 18, "E150 Super Duty Passenger" },
                    { 409, 18, "E250 Cargo" },
                    { 410, 18, "E250 Super Duty Cargo" },
                    { 411, 18, "E350 Super Duty Cargo" },
                    { 412, 18, "E350 Super Duty Passenger" },
                    { 413, 18, "EcoSport" },
                    { 407, 18, "E150 Super Duty Cargo" },
                    { 415, 18, "Econoline E150 Passenger" },
                    { 398, 18, "Aspire" },
                    { 396, 18, "Aerostar Cargo" },
                    { 382, 16, "458 Spider" },
                    { 383, 16, "488 GTB" },
                    { 384, 16, "488 Spider" },
                    { 385, 16, "599 GTB Fiorano" },
                    { 386, 16, "599 GTO" },
                    { 387, 16, "612 Scaglietti" },
                    { 397, 18, "Aerostar Passenger" },
                    { 388, 16, "812 Superfast" },
                    { 390, 16, "F12berlinetta" },
                    { 391, 16, "F430" },
                    { 392, 16, "FF" },
                    { 393, 16, "GTC4Lusso" },
                    { 394, 16, "Portofino" }
                });

            migrationBuilder.InsertData(
                table: "Model",
                columns: new[] { "Id", "BrandId", "Name" },
                values: new object[] { 395, 17, "Karma" });

            migrationBuilder.InsertData(
                table: "Model",
                columns: new[] { "Id", "BrandId", "Name" },
                values: new object[] { 389, 16, "California" });

            migrationBuilder.InsertData(
                table: "Model",
                columns: new[] { "Id", "BrandId", "Name" },
                values: new object[] { 436, 18, "F250 Regular Cab" });

            migrationBuilder.InsertData(
                table: "Model",
                columns: new[] { "Id", "BrandId", "Name" },
                values: new object[,]
                {
                    { 488, 20, "1500 Club Coupe" },
                    { 988, 45, "Bravada" },
                    { 989, 45, "Ciera" },
                    { 990, 45, "Custom Cruiser" },
                    { 991, 45, "Cutlass" },
                    { 992, 45, "Cutlass Ciera" },
                    { 993, 45, "Cutlass Cruiser" },
                    { 994, 45, "Cutlass Supreme" },
                    { 995, 45, "Intrigue" },
                    { 996, 45, "LSS" },
                    { 997, 45, "Regency" },
                    { 998, 45, "Silhouette" },
                    { 999, 45, "Toronado" },
                    { 1000, 46, "Esperante" },
                    { 1001, 47, "Acclaim" },
                    { 1002, 47, "Breeze" },
                    { 1003, 47, "Colt" },
                    { 1004, 47, "Colt Vista" },
                    { 1005, 47, "Grand Voyager" },
                    { 1006, 47, "Laser" },
                    { 987, 45, "Aurora" },
                    { 986, 45, "Alero" },
                    { 985, 45, "Achieva" },
                    { 984, 45, "98" },
                    { 964, 44, "Pathfinder" },
                    { 965, 44, "Pathfinder Armada" },
                    { 966, 44, "Quest" },
                    { 967, 44, "Regular Cab" },
                    { 968, 44, "Rogue" },
                    { 969, 44, "Rogue Select" },
                    { 970, 44, "Rogue Sport" },
                    { 971, 44, "Sentra" },
                    { 972, 44, "Stanza" },
                    { 1007, 47, "Neon" },
                    { 973, 44, "TITAN Single Cab" },
                    { 975, 44, "TITAN XD King Cab" },
                    { 976, 44, "TITAN XD Single Cab" },
                    { 977, 44, "Titan Crew Cab" },
                    { 978, 44, "Titan King Cab" },
                    { 979, 44, "Versa" },
                    { 980, 44, "Versa Note" },
                    { 981, 44, "Xterra" }
                });

            migrationBuilder.InsertData(
                table: "Model",
                columns: new[] { "Id", "BrandId", "Name" },
                values: new object[,]
                {
                    { 982, 44, "cube" },
                    { 983, 45, "88" },
                    { 974, 44, "TITAN XD Crew Cab" },
                    { 963, 44, "NX" },
                    { 1008, 47, "Prowler" },
                    { 1010, 47, "Voyager" },
                    { 1035, 49, "968" },
                    { 1036, 49, "Boxster" },
                    { 1037, 49, "Carrera GT" },
                    { 1038, 49, "Cayenne" },
                    { 1039, 49, "Cayenne Coupe" },
                    { 1040, 49, "Cayman" },
                    { 1041, 49, "Macan" },
                    { 1042, 49, "Panamera" },
                    { 1043, 49, "Taycan" },
                    { 1044, 50, "1500 Classic Crew Cab" },
                    { 1045, 50, "1500 Classic Quad Cab" },
                    { 1046, 50, "1500 Classic Regular Cab" },
                    { 1047, 50, "1500 Crew Cab" },
                    { 1048, 50, "1500 Quad Cab" },
                    { 1049, 50, "1500 Regular Cab" },
                    { 1050, 50, "2500 Crew Cab" },
                    { 1051, 50, "2500 Mega Cab" },
                    { 1052, 50, "2500 Regular Cab" },
                    { 1053, 50, "3500 Crew Cab" },
                    { 1034, 49, "928" },
                    { 1033, 49, "911" },
                    { 1032, 49, "718 Cayman" },
                    { 1031, 49, "718 Boxster" },
                    { 1011, 48, "Aztek" },
                    { 1012, 48, "Bonneville" },
                    { 1013, 48, "Firebird" },
                    { 1014, 48, "G3" },
                    { 1015, 48, "G5" },
                    { 1016, 48, "G6" },
                    { 1017, 48, "G6 (2009.5)" },
                    { 1018, 48, "G8" },
                    { 1019, 48, "GTO" },
                    { 1009, 47, "Sundance" },
                    { 1020, 48, "Grand Am" },
                    { 1022, 48, "LeMans" },
                    { 1023, 48, "Montana" }
                });

            migrationBuilder.InsertData(
                table: "Model",
                columns: new[] { "Id", "BrandId", "Name" },
                values: new object[,]
                {
                    { 1024, 48, "Montana SV6" },
                    { 1025, 48, "Solstice" },
                    { 1026, 48, "Sunbird" },
                    { 1027, 48, "Sunfire" },
                    { 1028, 48, "Torrent" },
                    { 1029, 48, "Trans Sport" },
                    { 1030, 48, "Vibe" },
                    { 1021, 48, "Grand Prix" },
                    { 962, 44, "NV3500 HD Passenger" },
                    { 961, 44, "NV3500 HD Cargo" },
                    { 960, 44, "NV2500 HD Cargo" },
                    { 893, 41, "SLK" },
                    { 894, 41, "SLK-Class" },
                    { 895, 41, "SLR McLaren" },
                    { 896, 41, "SLS-Class" },
                    { 897, 41, "Sprinter 2500 Cargo" },
                    { 898, 41, "Sprinter 2500 Crew" },
                    { 899, 41, "Sprinter 2500 Passenger" },
                    { 900, 41, "Sprinter 3500 Cargo" },
                    { 901, 41, "Sprinter 3500 XD Cargo" },
                    { 902, 41, "Sprinter WORKER Cargo" },
                    { 903, 41, "Sprinter WORKER Passenger" },
                    { 904, 42, "Capri" },
                    { 905, 42, "Cougar" },
                    { 906, 42, "Grand Marquis" },
                    { 907, 42, "Marauder" },
                    { 908, 42, "Mariner" },
                    { 909, 42, "Milan" },
                    { 910, 42, "Montego" },
                    { 911, 42, "Monterey" },
                    { 892, 41, "SLC" },
                    { 891, 41, "SL-Class" },
                    { 890, 41, "SL" },
                    { 889, 41, "S-Class" },
                    { 869, 41, "Mercedes-AMG E-Class" },
                    { 870, 41, "Mercedes-AMG G-Class" },
                    { 871, 41, "Mercedes-AMG GLA" },
                    { 872, 41, "Mercedes-AMG GLC" },
                    { 873, 41, "Mercedes-AMG GLC Coupe" },
                    { 874, 41, "Mercedes-AMG GLE" },
                    { 875, 41, "Mercedes-AMG GLE Coupe" },
                    { 876, 41, "Mercedes-AMG GLS" }
                });

            migrationBuilder.InsertData(
                table: "Model",
                columns: new[] { "Id", "BrandId", "Name" },
                values: new object[,]
                {
                    { 877, 41, "Mercedes-AMG GT" },
                    { 912, 42, "Mountaineer" },
                    { 878, 41, "Mercedes-AMG S-Class" },
                    { 880, 41, "Mercedes-AMG SLC" },
                    { 881, 41, "Mercedes-AMG SLK" },
                    { 882, 41, "Mercedes-Maybach S 600" },
                    { 883, 41, "Mercedes-Maybach S-Class" },
                    { 884, 41, "Metris Cargo" },
                    { 885, 41, "Metris Passenger" },
                    { 886, 41, "Metris WORKER Cargo" },
                    { 887, 41, "Metris WORKER Passenger" },
                    { 888, 41, "R-Class" },
                    { 879, 41, "Mercedes-AMG SL" },
                    { 913, 42, "Mystique" },
                    { 914, 42, "Sable" },
                    { 915, 42, "Topaz" },
                    { 940, 44, "200SX" },
                    { 941, 44, "240SX" },
                    { 942, 44, "300ZX" },
                    { 943, 44, "350Z" },
                    { 944, 44, "370Z" },
                    { 945, 44, "Altima" },
                    { 946, 44, "Armada" },
                    { 947, 44, "Frontier Crew Cab" },
                    { 948, 44, "Frontier King Cab" },
                    { 939, 43, "i-MiEV" },
                    { 949, 44, "Frontier Regular Cab" },
                    { 951, 44, "JUKE" },
                    { 952, 44, "Kicks" },
                    { 953, 44, "King Cab" },
                    { 954, 44, "LEAF" },
                    { 955, 44, "Maxima" },
                    { 956, 44, "Murano" },
                    { 957, 44, "NV1500 Cargo" },
                    { 958, 44, "NV200" },
                    { 959, 44, "NV200 Taxi" },
                    { 950, 44, "GT-R" },
                    { 1054, 50, "3500 Mega Cab" },
                    { 938, 43, "Raider Extended Cab" },
                    { 936, 43, "Precis" },
                    { 916, 42, "Tracer" },
                    { 917, 42, "Villager" }
                });

            migrationBuilder.InsertData(
                table: "Model",
                columns: new[] { "Id", "BrandId", "Name" },
                values: new object[,]
                {
                    { 918, 43, "3000GT" },
                    { 919, 43, "Diamante" },
                    { 920, 43, "Eclipse" },
                    { 921, 43, "Eclipse Cross" },
                    { 922, 43, "Endeavor" },
                    { 923, 43, "Expo" },
                    { 924, 43, "Galant" },
                    { 937, 43, "Raider Double Cab" },
                    { 925, 43, "Lancer" },
                    { 927, 43, "Mighty Max Macro Cab" },
                    { 928, 43, "Mighty Max Regular Cab" },
                    { 929, 43, "Mirage" },
                    { 930, 43, "Mirage G4" },
                    { 931, 43, "Montero" },
                    { 932, 43, "Montero Sport" },
                    { 933, 43, "Outlander" },
                    { 934, 43, "Outlander PHEV" },
                    { 935, 43, "Outlander Sport" },
                    { 926, 43, "Lancer Evolution" },
                    { 1055, 50, "3500 Regular Cab" },
                    { 1056, 50, "C/V" },
                    { 1057, 50, "C/V Tradesman" },
                    { 1177, 59, "Venza" },
                    { 1178, 59, "Xtra Cab" },
                    { 1179, 59, "Yaris" },
                    { 1180, 59, "Yaris Hatchback" },
                    { 1181, 59, "Yaris iA" },
                    { 1182, 60, "Arteon" },
                    { 1183, 60, "Atlas" },
                    { 1184, 60, "Beetle" },
                    { 1185, 60, "CC" },
                    { 1186, 60, "Cabrio" },
                    { 1187, 60, "Cabrio (New)" },
                    { 1188, 60, "Cabriolet" },
                    { 1189, 60, "Corrado" },
                    { 1190, 60, "Eos" },
                    { 1191, 60, "Eurovan" },
                    { 1192, 60, "Fox" },
                    { 1193, 60, "GLI" },
                    { 1194, 60, "GTI" },
                    { 1195, 60, "GTI (New)" },
                    { 1176, 59, "Tundra Regular Cab" }
                });

            migrationBuilder.InsertData(
                table: "Model",
                columns: new[] { "Id", "BrandId", "Name" },
                values: new object[,]
                {
                    { 1175, 59, "Tundra Double Cab" },
                    { 1174, 59, "Tundra CrewMax" },
                    { 1173, 59, "Tundra Access Cab" },
                    { 1153, 59, "Previa" },
                    { 1154, 59, "Prius" },
                    { 1155, 59, "Prius Plug-in Hybrid" },
                    { 1156, 59, "Prius Prime" },
                    { 1157, 59, "Prius c" },
                    { 1158, 59, "Prius v" },
                    { 1159, 59, "RAV4" },
                    { 1160, 59, "RAV4 Hybrid" },
                    { 1161, 59, "Regular Cab" },
                    { 1196, 60, "Golf" },
                    { 1162, 59, "Sequoia" },
                    { 1164, 59, "Solara" },
                    { 1165, 59, "Supra" },
                    { 1166, 59, "T100 Regular Cab" },
                    { 1167, 59, "T100 Xtracab" },
                    { 1168, 59, "Tacoma Access Cab" },
                    { 1169, 59, "Tacoma Double Cab" },
                    { 1170, 59, "Tacoma Regular Cab" },
                    { 1171, 59, "Tacoma Xtracab" },
                    { 1172, 59, "Tercel" },
                    { 1163, 59, "Sienna" },
                    { 1197, 60, "Golf (New)" },
                    { 1198, 60, "Golf Alltrack" },
                    { 1199, 60, "Golf GTI" },
                    { 1224, 61, "960" },
                    { 1225, 61, "C30" },
                    { 1226, 61, "C70" },
                    { 1227, 61, "S40" },
                    { 1228, 61, "S40 (New)" },
                    { 1229, 61, "S60" },
                    { 1230, 61, "S70" },
                    { 1231, 61, "S80" },
                    { 1232, 61, "S90" },
                    { 1223, 61, "940" },
                    { 1233, 61, "V40" },
                    { 1235, 61, "V60" },
                    { 1236, 61, "V70" },
                    { 1237, 61, "V90" },
                    { 1238, 61, "XC40" }
                });

            migrationBuilder.InsertData(
                table: "Model",
                columns: new[] { "Id", "BrandId", "Name" },
                values: new object[,]
                {
                    { 1239, 61, "XC60" },
                    { 1240, 61, "XC70" },
                    { 1241, 61, "XC90" },
                    { 1242, 62, "fortwo" },
                    { 1243, 62, "fortwo cabrio" },
                    { 1234, 61, "V50" },
                    { 1152, 59, "Paseo" },
                    { 1222, 61, "850" },
                    { 1220, 61, "240" },
                    { 1200, 60, "Golf III" },
                    { 1201, 60, "Golf R" },
                    { 1202, 60, "Golf SportWagen" },
                    { 1203, 60, "Jetta" },
                    { 1204, 60, "Jetta (New)" },
                    { 1205, 60, "Jetta GLI" },
                    { 1206, 60, "Jetta III" },
                    { 1207, 60, "Jetta SportWagen" },
                    { 1208, 60, "New Beetle" },
                    { 1221, 61, "740" },
                    { 1209, 60, "Passat" },
                    { 1211, 60, "Phaeton" },
                    { 1212, 60, "R32" },
                    { 1213, 60, "Rabbit" },
                    { 1214, 60, "Routan" },
                    { 1215, 60, "Tiguan" },
                    { 1216, 60, "Tiguan Limited" },
                    { 1217, 60, "Touareg" },
                    { 1218, 60, "Touareg 2" },
                    { 1219, 60, "e-Golf" },
                    { 1210, 60, "Passat (New)" },
                    { 868, 41, "Mercedes-AMG CLS" },
                    { 1151, 59, "Mirai" },
                    { 1149, 59, "MR2" },
                    { 1082, 54, "Outlook" },
                    { 1083, 54, "Relay" },
                    { 1084, 54, "S-Series" },
                    { 1085, 54, "SKY" },
                    { 1086, 54, "VUE" },
                    { 1087, 55, "FR-S" },
                    { 1088, 55, "iA" },
                    { 1089, 55, "iM" },
                    { 1090, 55, "iQ" }
                });

            migrationBuilder.InsertData(
                table: "Model",
                columns: new[] { "Id", "BrandId", "Name" },
                values: new object[,]
                {
                    { 1091, 55, "tC" },
                    { 1092, 55, "xA" },
                    { 1093, 55, "xB" },
                    { 1094, 55, "xD" },
                    { 1095, 56, "Ascent" },
                    { 1096, 56, "B9 Tribeca" },
                    { 1097, 56, "BRZ" },
                    { 1098, 56, "Baja" },
                    { 1099, 56, "Crosstrek" },
                    { 1100, 56, "Forester" },
                    { 1081, 54, "L-Series" },
                    { 1080, 54, "Ion" },
                    { 1079, 54, "Aura" },
                    { 1078, 54, "Astra" },
                    { 1058, 50, "Dakota Crew Cab" },
                    { 1059, 50, "Dakota Extended Cab" },
                    { 1060, 50, "ProMaster 1500 Cargo" },
                    { 1061, 50, "ProMaster 2500 Cargo" },
                    { 1062, 50, "ProMaster 3500 Cargo" },
                    { 1063, 50, "ProMaster Cargo Van" },
                    { 1064, 50, "ProMaster City" },
                    { 1065, 50, "ProMaster Window Van" },
                    { 1066, 51, "Dawn" },
                    { 1101, 56, "Impreza" },
                    { 1067, 51, "Ghost" },
                    { 1069, 51, "Wraith" },
                    { 1070, 52, "Viper" },
                    { 1071, 53, "3-Sep" },
                    { 1072, 53, "5-Sep" },
                    { 1073, 53, "9-2X" },
                    { 1074, 53, "9-4X" },
                    { 1075, 53, "9-7X" },
                    { 1076, 53, "900" },
                    { 1077, 53, "9000" },
                    { 1068, 51, "Phantom" },
                    { 1102, 56, "Justy" },
                    { 1103, 56, "Legacy" },
                    { 1104, 56, "Loyale" },
                    { 1129, 58, "Model X" },
                    { 1130, 59, "4Runner" },
                    { 1131, 59, "86" },
                    { 1132, 59, "Avalon" }
                });

            migrationBuilder.InsertData(
                table: "Model",
                columns: new[] { "Id", "BrandId", "Name" },
                values: new object[,]
                {
                    { 1133, 59, "Avalon Hybrid" },
                    { 1134, 59, "C-HR" },
                    { 1135, 59, "Camry" },
                    { 1136, 59, "Camry Hybrid" },
                    { 1137, 59, "Celica" },
                    { 1128, 58, "Model S" },
                    { 1138, 59, "Corolla" },
                    { 1140, 59, "Corolla Hybrid" },
                    { 1141, 59, "Corolla iM" },
                    { 1142, 59, "Cressida" },
                    { 1143, 59, "Echo" },
                    { 1144, 59, "FJ Cruiser" },
                    { 1145, 59, "GR Supra" },
                    { 1146, 59, "Highlander" },
                    { 1147, 59, "Highlander Hybrid" },
                    { 1148, 59, "Land Cruiser" },
                    { 1139, 59, "Corolla Hatchback" },
                    { 1150, 59, "Matrix" },
                    { 1127, 58, "Model 3" },
                    { 1125, 57, "XL-7" },
                    { 1105, 56, "Outback" },
                    { 1106, 56, "SVX" },
                    { 1107, 56, "Tribeca" },
                    { 1108, 56, "WRX" },
                    { 1109, 56, "XV Crosstrek" },
                    { 1110, 57, "Aerio" },
                    { 1111, 57, "Equator Crew Cab" },
                    { 1112, 57, "Equator Extended Cab" },
                    { 1113, 57, "Esteem" },
                    { 1126, 57, "XL7" },
                    { 1114, 57, "Forenza" },
                    { 1116, 57, "Kizashi" },
                    { 1117, 57, "Reno" },
                    { 1118, 57, "SX4" },
                    { 1119, 57, "Samurai" },
                    { 1120, 57, "Sidekick" },
                    { 1121, 57, "Swift" },
                    { 1122, 57, "Verona" },
                    { 1123, 57, "Vitara" },
                    { 1124, 57, "X-90" },
                    { 1115, 57, "Grand Vitara" },
                    { 867, 41, "Mercedes-AMG CLA" }
                });

            migrationBuilder.InsertData(
                table: "Model",
                columns: new[] { "Id", "BrandId", "Name" },
                values: new object[,]
                {
                    { 866, 41, "Mercedes-AMG C-Class" },
                    { 865, 41, "M-Class" },
                    { 608, 25, "Elantra GT" },
                    { 609, 25, "Entourage" },
                    { 610, 25, "Equus" },
                    { 611, 25, "Excel" },
                    { 612, 25, "Genesis" },
                    { 613, 25, "Genesis Coupe" },
                    { 614, 25, "Ioniq Electric" },
                    { 615, 25, "Ioniq Hybrid" },
                    { 616, 25, "Ioniq Plug-in Hybrid" },
                    { 617, 25, "Kona" },
                    { 618, 25, "Kona Electric" },
                    { 619, 25, "NEXO" },
                    { 620, 25, "Palisade" },
                    { 621, 25, "Santa Fe" },
                    { 622, 25, "Santa Fe Sport" },
                    { 623, 25, "Santa Fe XL" },
                    { 624, 25, "Scoupe" },
                    { 625, 25, "Sonata" },
                    { 626, 25, "Sonata Hybrid" },
                    { 607, 25, "Elantra" },
                    { 606, 25, "Azera" },
                    { 605, 25, "Accent" },
                    { 604, 24, "del Sol" },
                    { 584, 24, "Accord Crosstour" },
                    { 585, 24, "Accord Hybrid" },
                    { 586, 24, "CR-V" },
                    { 587, 24, "CR-Z" },
                    { 588, 24, "Civic" },
                    { 589, 24, "Civic Type R" },
                    { 590, 24, "Clarity Electric" },
                    { 591, 24, "Clarity Fuel Cell" },
                    { 592, 24, "Clarity Plug-in Hybrid" },
                    { 627, 25, "Sonata Plug-in Hybrid" },
                    { 593, 24, "Crosstour" },
                    { 595, 24, "Fit" },
                    { 596, 24, "HR-V" },
                    { 597, 24, "Insight" },
                    { 598, 24, "Odyssey" },
                    { 599, 24, "Passport" },
                    { 600, 24, "Pilot" }
                });

            migrationBuilder.InsertData(
                table: "Model",
                columns: new[] { "Id", "BrandId", "Name" },
                values: new object[,]
                {
                    { 601, 24, "Prelude" },
                    { 602, 24, "Ridgeline" },
                    { 603, 24, "S2000" },
                    { 594, 24, "Element" },
                    { 628, 25, "Tiburon" },
                    { 629, 25, "Tucson" },
                    { 630, 25, "Tucson Fuel Cell" },
                    { 655, 27, "Ascender" },
                    { 656, 27, "Axiom" },
                    { 657, 27, "Hombre Regular Cab" },
                    { 658, 27, "Hombre Spacecab" },
                    { 659, 27, "Impulse" },
                    { 660, 27, "Oasis" },
                    { 661, 27, "Regular Cab" },
                    { 662, 27, "Rodeo" },
                    { 663, 27, "Rodeo Sport" },
                    { 654, 27, "Amigo" },
                    { 664, 27, "Spacecab" },
                    { 666, 27, "Trooper" },
                    { 667, 27, "VehiCROSS" },
                    { 668, 27, "i-280 Extended Cab" },
                    { 669, 27, "i-290 Extended Cab" },
                    { 670, 27, "i-350 Crew Cab" },
                    { 671, 27, "i-370 Crew Cab" },
                    { 672, 27, "i-370 Extended Cab" },
                    { 673, 28, "E-PACE" },
                    { 674, 28, "F-PACE" },
                    { 665, 27, "Stylus" },
                    { 583, 24, "Accord" },
                    { 653, 26, "QX80" },
                    { 651, 26, "QX60" },
                    { 631, 25, "Veloster" },
                    { 632, 25, "Venue" },
                    { 633, 25, "Veracruz" },
                    { 634, 25, "XG300" },
                    { 635, 25, "XG350" },
                    { 636, 26, "EX" },
                    { 637, 26, "FX" },
                    { 638, 26, "G" },
                    { 639, 26, "I" },
                    { 652, 26, "QX70" },
                    { 640, 26, "J" }
                });

            migrationBuilder.InsertData(
                table: "Model",
                columns: new[] { "Id", "BrandId", "Name" },
                values: new object[,]
                {
                    { 642, 26, "M" },
                    { 643, 26, "Q" },
                    { 644, 26, "Q40" },
                    { 645, 26, "Q50" },
                    { 646, 26, "Q60" },
                    { 647, 26, "Q70" },
                    { 648, 26, "QX" },
                    { 649, 26, "QX30" },
                    { 650, 26, "QX50" },
                    { 641, 26, "JX" },
                    { 675, 28, "F-TYPE" },
                    { 582, 23, "H3T" },
                    { 580, 23, "H2" },
                    { 513, 20, "Rally Wagon G3500" },
                    { 514, 20, "Safari Cargo" },
                    { 515, 20, "Safari Passenger" },
                    { 516, 20, "Savana 1500 Cargo" },
                    { 517, 20, "Savana 1500 Passenger" },
                    { 518, 20, "Savana 2500 Cargo" },
                    { 519, 20, "Savana 2500 Passenger" },
                    { 520, 20, "Savana 3500 Cargo" },
                    { 521, 20, "Savana 3500 Passenger" },
                    { 522, 20, "Sierra (Classic) 1500 Crew Cab" },
                    { 523, 20, "Sierra (Classic) 1500 Extended Cab" },
                    { 524, 20, "Sierra (Classic) 1500 HD Crew Cab" },
                    { 525, 20, "Sierra (Classic) 1500 Regular Cab" },
                    { 526, 20, "Sierra (Classic) 2500 Crew Cab" },
                    { 527, 20, "Sierra (Classic) 2500 HD Crew Cab" },
                    { 528, 20, "Sierra (Classic) 2500 HD Extended Cab" },
                    { 529, 20, "Sierra (Classic) 2500 HD Regular Cab" },
                    { 530, 20, "Sierra (Classic) 3500 Crew Cab" },
                    { 531, 20, "Sierra (Classic) 3500 Extended Cab" },
                    { 512, 20, "Rally Wagon G2500" },
                    { 511, 20, "Rally Wagon 3500" },
                    { 510, 20, "Rally Wagon 2500" },
                    { 509, 20, "Rally Wagon 1500" },
                    { 489, 20, "1500 Regular Cab" },
                    { 490, 20, "2500 Club Coupe" },
                    { 491, 20, "2500 Crew Cab" },
                    { 492, 20, "2500 HD Club Coupe" },
                    { 493, 20, "2500 HD Extended Cab" },
                    { 494, 20, "2500 HD Regular Cab" }
                });

            migrationBuilder.InsertData(
                table: "Model",
                columns: new[] { "Id", "BrandId", "Name" },
                values: new object[,]
                {
                    { 495, 20, "2500 Regular Cab" },
                    { 496, 20, "3500 Club Coupe" },
                    { 497, 20, "3500 Crew Cab" },
                    { 532, 20, "Sierra (Classic) 3500 Regular Cab" },
                    { 498, 20, "3500 Extended Cab" },
                    { 500, 20, "Acadia" },
                    { 501, 20, "Acadia Limited" },
                    { 502, 20, "Canyon Crew Cab" },
                    { 503, 20, "Canyon Extended Cab" },
                    { 504, 20, "Canyon Regular Cab" },
                    { 505, 20, "Envoy" },
                    { 506, 20, "Envoy XL" },
                    { 507, 20, "Envoy XUV" },
                    { 508, 20, "Jimmy" },
                    { 499, 20, "3500 Regular Cab" },
                    { 533, 20, "Sierra 1500 Crew Cab" },
                    { 534, 20, "Sierra 1500 Double Cab" },
                    { 535, 20, "Sierra 1500 Extended Cab" },
                    { 560, 20, "Terrain" },
                    { 561, 20, "Vandura 1500" },
                    { 562, 20, "Vandura 2500" },
                    { 563, 20, "Vandura 3500" },
                    { 564, 20, "Vandura G1500" },
                    { 565, 20, "Vandura G2500" },
                    { 566, 20, "Vandura G3500" },
                    { 567, 20, "Yukon" },
                    { 568, 20, "Yukon Denali" },
                    { 559, 20, "Suburban 2500" },
                    { 569, 20, "Yukon XL" },
                    { 571, 20, "Yukon XL 2500" },
                    { 572, 21, "G70" },
                    { 573, 21, "G80" },
                    { 574, 21, "G90" },
                    { 575, 22, "Metro" },
                    { 576, 22, "Prizm" },
                    { 577, 22, "Storm" },
                    { 578, 22, "Tracker" },
                    { 579, 23, "H1" },
                    { 570, 20, "Yukon XL 1500" },
                    { 581, 23, "H3" },
                    { 558, 20, "Suburban 1500" },
                    { 556, 20, "Sonoma Extended Cab" }
                });

            migrationBuilder.InsertData(
                table: "Model",
                columns: new[] { "Id", "BrandId", "Name" },
                values: new object[,]
                {
                    { 536, 20, "Sierra 1500 HD Crew Cab" },
                    { 537, 20, "Sierra 1500 Limited Double Cab" },
                    { 538, 20, "Sierra 1500 Regular Cab" },
                    { 539, 20, "Sierra 2500 Crew Cab" },
                    { 540, 20, "Sierra 2500 Extended Cab" },
                    { 541, 20, "Sierra 2500 HD Crew Cab" },
                    { 542, 20, "Sierra 2500 HD Double Cab" },
                    { 543, 20, "Sierra 2500 HD Extended Cab" },
                    { 544, 20, "Sierra 2500 HD Regular Cab" },
                    { 557, 20, "Sonoma Regular Cab" },
                    { 545, 20, "Sierra 2500 Regular Cab" },
                    { 547, 20, "Sierra 3500 Extended Cab" },
                    { 548, 20, "Sierra 3500 HD Crew Cab" },
                    { 549, 20, "Sierra 3500 HD Double Cab" },
                    { 550, 20, "Sierra 3500 HD Extended Cab" },
                    { 551, 20, "Sierra 3500 HD Regular Cab" },
                    { 552, 20, "Sierra 3500 Regular Cab" },
                    { 553, 20, "Sonoma Club Cab" },
                    { 554, 20, "Sonoma Club Coupe Cab" },
                    { 555, 20, "Sonoma Crew Cab" },
                    { 546, 20, "Sierra 3500 Crew Cab" },
                    { 1244, 62, "fortwo electric drive" },
                    { 676, 28, "I-PACE" },
                    { 678, 28, "X-Type" },
                    { 798, 36, "Protege" },
                    { 799, 36, "Protege5" },
                    { 800, 36, "RX-7" },
                    { 801, 36, "RX-8" },
                    { 802, 36, "Tribute" },
                    { 803, 37, "Clubman" },
                    { 804, 37, "Convertible" },
                    { 805, 37, "Cooper" },
                    { 806, 37, "Countryman" },
                    { 807, 37, "Coupe" },
                    { 808, 37, "Hardtop" },
                    { 809, 37, "Hardtop 2 Door" },
                    { 810, 37, "Hardtop 4 Door" },
                    { 811, 37, "Paceman" },
                    { 812, 37, "Roadster" },
                    { 813, 38, "Coupe" },
                    { 814, 38, "Ghibli" },
                    { 815, 38, "GranSport" }
                });

            migrationBuilder.InsertData(
                table: "Model",
                columns: new[] { "Id", "BrandId", "Name" },
                values: new object[,]
                {
                    { 816, 38, "GranTurismo" },
                    { 797, 36, "Navajo" },
                    { 796, 36, "Millenia" },
                    { 795, 36, "MX-6" },
                    { 794, 36, "MX-5 Miata RF" },
                    { 774, 35, "Exige" },
                    { 775, 35, "Exige S" },
                    { 776, 36, "323" },
                    { 777, 36, "626" },
                    { 778, 36, "929" },
                    { 779, 36, "B-Series Cab Plus" },
                    { 780, 36, "B-Series Extended Cab" },
                    { 781, 36, "B-Series Regular Cab" },
                    { 782, 36, "CX-3" },
                    { 817, 38, "Levante" },
                    { 783, 36, "CX-30" },
                    { 785, 36, "CX-7" },
                    { 786, 36, "CX-9" },
                    { 787, 36, "MAZDA2" },
                    { 788, 36, "MAZDA3" },
                    { 789, 36, "MAZDA5" },
                    { 790, 36, "MAZDA6" },
                    { 791, 36, "MPV" },
                    { 792, 36, "MX-3" },
                    { 793, 36, "MX-5 Miata" },
                    { 784, 36, "CX-5" },
                    { 818, 38, "Quattroporte" },
                    { 819, 39, "57" },
                    { 820, 39, "62" },
                    { 845, 41, "A-Class" },
                    { 846, 41, "B-Class" },
                    { 847, 41, "C-Class" },
                    { 848, 41, "CL-Class" },
                    { 849, 41, "CLA" },
                    { 850, 41, "CLA-Class" },
                    { 851, 41, "CLK-Class" },
                    { 852, 41, "CLS" },
                    { 853, 41, "CLS-Class" },
                    { 844, 41, "600 SL" },
                    { 854, 41, "E-Class" },
                    { 856, 41, "GL-Class" },
                    { 857, 41, "GLA" }
                });

            migrationBuilder.InsertData(
                table: "Model",
                columns: new[] { "Id", "BrandId", "Name" },
                values: new object[,]
                {
                    { 858, 41, "GLA-Class" },
                    { 859, 41, "GLC" },
                    { 860, 41, "GLC Coupe" },
                    { 861, 41, "GLE" },
                    { 862, 41, "GLE Coupe" },
                    { 863, 41, "GLK-Class" },
                    { 864, 41, "GLS" },
                    { 855, 41, "G-Class" },
                    { 773, 35, "Evora 400" },
                    { 843, 41, "600 SEL" },
                    { 841, 41, "500 SL" },
                    { 821, 40, "570GT" },
                    { 822, 40, "570S" },
                    { 823, 40, "650S" },
                    { 824, 40, "675LT" },
                    { 825, 40, "720S" },
                    { 826, 40, "MP4-12C" },
                    { 827, 41, "190 E" },
                    { 828, 41, "300 CE" },
                    { 829, 41, "300 D" },
                    { 842, 41, "600 SEC" },
                    { 830, 41, "300 E" },
                    { 832, 41, "300 SE" },
                    { 833, 41, "300 SL" },
                    { 834, 41, "300 TE" },
                    { 835, 41, "400 E" },
                    { 836, 41, "400 SE" },
                    { 837, 41, "400 SEL" },
                    { 838, 41, "500 E" },
                    { 839, 41, "500 SEC" },
                    { 840, 41, "500 SEL" },
                    { 831, 41, "300 SD" },
                    { 677, 28, "S-Type" },
                    { 772, 35, "Evora" },
                    { 770, 34, "Zephyr" },
                    { 703, 30, "Niro Plug-in Hybrid" },
                    { 704, 30, "Optima" },
                    { 705, 30, "Optima (2006.5)" },
                    { 706, 30, "Optima Hybrid" },
                    { 707, 30, "Optima Plug-in Hybrid" },
                    { 708, 30, "Rio" },
                    { 709, 30, "Rondo" }
                });

            migrationBuilder.InsertData(
                table: "Model",
                columns: new[] { "Id", "BrandId", "Name" },
                values: new object[,]
                {
                    { 710, 30, "Sedona" },
                    { 711, 30, "Sephia" },
                    { 712, 30, "Sorento" },
                    { 713, 30, "Soul" },
                    { 714, 30, "Soul EV" },
                    { 715, 30, "Spectra" },
                    { 716, 30, "Sportage" },
                    { 717, 30, "Stinger" },
                    { 718, 30, "Telluride" },
                    { 719, 31, "Aventador" },
                    { 720, 31, "Gallardo" },
                    { 721, 31, "Huracan" },
                    { 702, 30, "Niro EV" },
                    { 701, 30, "Niro" },
                    { 700, 30, "K900" },
                    { 699, 30, "Forte5" },
                    { 679, 28, "XE" },
                    { 680, 28, "XF" },
                    { 681, 28, "XJ" },
                    { 682, 28, "XK" },
                    { 683, 29, "Cherokee" },
                    { 684, 29, "Comanche Regular Cab" },
                    { 685, 29, "Commander" },
                    { 686, 29, "Compass" },
                    { 687, 29, "Gladiator" },
                    { 722, 31, "Murcielago" },
                    { 688, 29, "Grand Cherokee" },
                    { 690, 29, "Patriot" },
                    { 691, 29, "Renegade" },
                    { 692, 29, "Wrangler" },
                    { 693, 29, "Wrangler Unlimited" },
                    { 694, 30, "Amanti" },
                    { 695, 30, "Borrego" },
                    { 696, 30, "Cadenza" },
                    { 697, 30, "Forte" },
                    { 698, 30, "Forte Koup" },
                    { 689, 29, "Liberty" },
                    { 723, 31, "Murcielago LP640" },
                    { 724, 32, "Defender 110" },
                    { 725, 32, "Defender 90" },
                    { 750, 33, "RX" },
                    { 751, 33, "SC" }
                });

            migrationBuilder.InsertData(
                table: "Model",
                columns: new[] { "Id", "BrandId", "Name" },
                values: new object[,]
                {
                    { 752, 33, "UX" },
                    { 753, 34, "Aviator" },
                    { 754, 34, "Blackwood" },
                    { 755, 34, "Continental" },
                    { 756, 34, "Corsair" },
                    { 757, 34, "LS" },
                    { 758, 34, "MKC" },
                    { 749, 33, "RC" },
                    { 759, 34, "MKS" },
                    { 761, 34, "MKX" },
                    { 762, 34, "MKZ" },
                    { 763, 34, "Mark LT" },
                    { 764, 34, "Mark VII" },
                    { 765, 34, "Mark VIII" },
                    { 766, 34, "Nautilus" },
                    { 767, 34, "Navigator" },
                    { 768, 34, "Navigator L" },
                    { 769, 34, "Town Car" },
                    { 760, 34, "MKT" },
                    { 771, 35, "Elise" },
                    { 748, 33, "NX" },
                    { 746, 33, "LS" },
                    { 726, 32, "Discovery" },
                    { 727, 32, "Discovery Series II" },
                    { 728, 32, "Discovery Sport" },
                    { 729, 32, "Freelander" },
                    { 730, 32, "LR2" },
                    { 731, 32, "LR3" },
                    { 732, 32, "LR4" },
                    { 733, 32, "Range Rover" },
                    { 734, 32, "Range Rover Evoque" },
                    { 747, 33, "LX" },
                    { 735, 32, "Range Rover Sport" },
                    { 737, 33, "CT" },
                    { 738, 33, "ES" },
                    { 739, 33, "GS" },
                    { 740, 33, "GX" },
                    { 741, 33, "HS" },
                    { 742, 33, "IS" },
                    { 743, 33, "IS F" },
                    { 744, 33, "LC" },
                    { 745, 33, "LFA" }
                });

            migrationBuilder.InsertData(
                table: "Model",
                columns: new[] { "Id", "BrandId", "Name" },
                values: new object[] { 736, 32, "Range Rover Velar" });

            migrationBuilder.InsertData(
                table: "Model",
                columns: new[] { "Id", "BrandId", "Name" },
                values: new object[] { 1245, 62, "fortwo electric drive cabrio" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 205);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 206);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 207);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 208);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 209);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 210);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 211);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 212);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 213);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 214);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 215);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 216);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 217);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 218);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 219);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 220);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 221);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 222);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 223);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 224);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 225);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 226);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 227);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 228);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 229);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 230);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 231);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 232);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 233);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 234);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 235);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 236);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 237);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 238);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 239);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 240);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 241);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 242);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 243);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 244);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 245);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 246);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 247);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 248);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 249);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 250);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 251);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 252);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 253);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 254);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 255);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 256);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 257);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 258);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 259);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 260);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 261);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 262);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 263);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 264);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 265);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 266);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 267);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 268);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 269);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 270);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 271);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 272);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 273);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 274);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 275);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 276);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 277);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 278);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 279);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 280);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 281);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 282);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 283);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 284);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 285);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 286);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 287);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 288);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 289);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 290);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 291);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 292);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 293);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 294);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 295);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 296);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 297);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 298);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 299);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 300);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 301);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 302);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 303);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 304);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 305);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 306);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 307);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 308);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 309);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 310);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 311);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 312);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 313);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 314);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 315);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 316);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 317);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 318);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 319);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 320);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 321);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 322);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 323);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 324);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 325);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 326);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 327);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 328);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 329);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 330);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 331);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 332);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 333);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 334);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 335);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 336);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 337);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 338);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 339);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 340);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 341);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 342);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 343);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 344);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 345);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 346);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 347);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 348);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 349);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 350);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 351);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 352);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 353);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 354);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 355);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 356);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 357);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 358);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 359);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 360);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 361);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 362);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 363);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 364);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 365);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 366);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 367);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 368);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 369);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 370);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 371);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 372);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 373);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 374);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 375);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 376);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 377);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 378);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 379);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 380);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 381);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 382);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 383);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 384);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 385);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 386);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 387);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 388);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 389);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 390);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 391);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 392);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 393);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 394);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 395);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 396);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 397);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 398);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 399);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 400);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 401);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 402);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 403);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 404);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 405);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 406);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 407);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 408);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 409);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 410);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 411);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 412);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 413);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 414);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 415);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 416);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 417);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 418);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 419);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 420);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 421);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 422);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 423);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 424);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 425);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 426);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 427);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 428);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 429);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 430);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 431);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 432);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 433);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 434);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 435);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 436);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 437);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 438);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 439);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 440);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 441);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 442);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 443);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 444);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 445);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 446);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 447);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 448);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 449);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 450);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 451);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 452);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 453);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 454);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 455);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 456);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 457);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 458);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 459);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 460);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 461);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 462);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 463);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 464);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 465);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 466);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 467);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 468);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 469);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 470);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 471);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 472);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 473);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 474);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 475);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 476);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 477);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 478);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 479);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 480);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 481);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 482);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 483);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 484);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 485);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 486);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 487);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 488);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 489);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 490);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 491);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 492);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 493);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 494);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 495);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 496);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 497);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 498);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 499);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 500);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 501);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 502);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 503);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 504);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 505);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 506);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 507);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 508);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 509);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 510);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 511);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 512);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 513);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 514);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 515);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 516);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 517);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 518);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 519);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 520);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 521);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 522);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 523);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 524);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 525);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 526);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 527);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 528);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 529);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 530);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 531);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 532);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 533);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 534);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 535);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 536);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 537);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 538);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 539);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 540);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 541);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 542);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 543);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 544);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 545);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 546);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 547);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 548);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 549);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 550);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 551);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 552);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 553);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 554);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 555);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 556);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 557);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 558);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 559);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 560);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 561);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 562);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 563);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 564);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 565);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 566);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 567);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 568);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 569);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 570);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 571);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 572);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 573);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 574);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 575);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 576);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 577);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 578);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 579);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 580);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 581);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 582);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 583);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 584);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 585);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 586);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 587);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 588);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 589);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 590);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 591);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 592);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 593);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 594);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 595);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 596);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 597);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 598);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 599);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 600);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 601);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 602);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 603);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 604);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 605);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 606);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 607);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 608);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 609);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 610);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 611);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 612);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 613);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 614);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 615);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 616);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 617);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 618);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 619);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 620);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 621);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 622);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 623);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 624);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 625);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 626);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 627);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 628);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 629);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 630);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 631);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 632);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 633);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 634);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 635);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 636);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 637);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 638);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 639);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 640);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 641);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 642);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 643);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 644);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 645);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 646);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 647);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 648);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 649);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 650);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 651);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 652);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 653);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 654);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 655);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 656);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 657);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 658);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 659);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 660);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 661);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 662);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 663);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 664);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 665);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 666);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 667);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 668);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 669);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 670);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 671);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 672);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 673);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 674);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 675);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 676);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 677);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 678);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 679);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 680);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 681);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 682);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 683);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 684);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 685);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 686);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 687);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 688);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 689);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 690);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 691);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 692);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 693);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 694);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 695);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 696);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 697);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 698);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 699);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 700);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 701);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 702);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 703);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 704);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 705);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 706);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 707);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 708);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 709);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 710);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 711);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 712);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 713);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 714);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 715);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 716);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 717);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 718);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 719);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 720);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 721);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 722);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 723);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 724);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 725);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 726);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 727);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 728);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 729);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 730);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 731);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 732);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 733);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 734);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 735);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 736);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 737);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 738);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 739);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 740);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 741);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 742);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 743);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 744);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 745);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 746);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 747);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 748);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 749);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 750);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 751);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 752);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 753);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 754);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 755);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 756);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 757);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 758);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 759);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 760);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 761);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 762);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 763);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 764);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 765);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 766);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 767);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 768);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 769);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 770);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 771);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 772);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 773);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 774);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 775);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 776);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 777);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 778);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 779);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 780);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 781);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 782);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 783);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 784);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 785);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 786);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 787);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 788);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 789);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 790);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 791);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 792);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 793);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 794);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 795);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 796);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 797);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 798);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 799);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 800);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 801);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 802);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 803);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 804);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 805);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 806);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 807);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 808);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 809);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 810);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 811);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 812);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 813);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 814);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 815);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 816);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 817);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 818);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 819);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 820);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 821);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 822);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 823);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 824);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 825);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 826);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 827);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 828);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 829);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 830);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 831);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 832);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 833);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 834);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 835);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 836);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 837);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 838);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 839);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 840);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 841);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 842);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 843);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 844);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 845);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 846);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 847);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 848);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 849);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 850);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 851);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 852);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 853);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 854);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 855);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 856);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 857);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 858);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 859);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 860);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 861);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 862);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 863);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 864);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 865);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 866);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 867);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 868);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 869);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 870);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 871);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 872);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 873);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 874);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 875);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 876);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 877);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 878);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 879);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 880);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 881);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 882);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 883);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 884);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 885);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 886);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 887);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 888);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 889);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 890);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 891);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 892);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 893);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 894);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 895);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 896);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 897);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 898);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 899);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 900);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 901);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 902);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 903);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 904);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 905);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 906);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 907);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 908);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 909);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 910);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 911);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 912);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 913);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 914);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 915);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 916);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 917);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 918);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 919);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 920);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 921);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 922);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 923);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 924);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 925);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 926);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 927);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 928);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 929);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 930);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 931);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 932);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 933);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 934);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 935);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 936);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 937);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 938);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 939);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 940);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 941);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 942);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 943);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 944);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 945);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 946);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 947);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 948);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 949);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 950);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 951);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 952);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 953);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 954);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 955);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 956);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 957);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 958);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 959);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 960);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 961);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 962);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 963);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 964);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 965);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 966);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 967);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 968);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 969);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 970);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 971);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 972);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 973);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 974);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 975);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 976);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 977);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 978);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 979);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 980);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 981);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 982);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 983);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 984);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 985);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 986);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 987);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 988);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 989);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 990);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 991);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 992);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 993);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 994);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 995);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 996);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 997);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 998);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 999);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1000);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1001);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1002);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1003);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1004);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1005);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1006);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1007);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1008);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1009);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1010);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1011);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1012);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1013);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1014);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1015);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1016);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1017);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1018);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1019);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1020);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1021);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1022);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1023);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1024);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1025);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1026);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1027);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1028);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1029);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1030);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1031);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1032);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1033);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1034);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1035);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1036);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1037);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1038);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1039);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1040);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1041);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1042);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1043);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1044);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1045);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1046);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1047);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1048);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1049);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1050);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1051);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1052);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1053);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1054);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1055);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1056);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1057);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1058);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1059);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1060);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1061);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1062);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1063);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1064);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1065);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1066);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1067);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1068);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1069);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1070);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1071);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1072);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1073);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1074);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1075);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1076);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1077);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1078);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1079);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1080);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1081);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1082);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1083);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1084);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1085);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1086);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1087);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1088);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1089);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1090);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1091);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1092);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1093);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1094);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1095);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1096);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1097);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1098);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1099);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1100);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1101);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1102);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1103);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1104);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1105);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1106);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1107);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1108);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1109);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1110);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1111);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1112);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1113);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1114);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1115);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1116);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1117);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1118);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1119);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1120);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1121);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1122);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1123);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1124);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1125);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1126);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1127);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1128);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1129);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1130);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1131);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1132);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1133);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1134);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1135);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1136);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1137);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1138);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1139);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1140);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1141);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1142);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1143);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1144);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1145);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1146);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1147);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1148);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1149);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1150);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1151);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1152);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1153);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1154);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1155);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1156);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1157);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1158);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1159);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1160);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1161);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1162);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1163);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1164);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1165);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1166);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1167);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1168);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1169);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1170);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1171);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1172);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1173);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1174);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1175);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1176);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1177);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1178);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1179);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1180);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1181);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1182);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1183);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1184);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1185);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1186);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1187);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1188);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1189);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1190);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1191);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1192);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1193);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1194);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1195);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1196);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1197);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1198);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1199);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1200);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1201);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1202);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1203);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1204);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1205);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1206);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1207);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1208);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1209);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1210);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1211);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1212);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1213);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1214);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1215);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1216);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1217);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1218);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1219);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1220);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1221);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1222);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1223);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1224);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1225);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1226);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1227);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1228);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1229);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1230);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1231);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1232);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1233);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1234);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1235);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1236);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1237);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1238);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1239);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1240);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1241);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1242);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1243);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1244);

            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1245);

            migrationBuilder.DeleteData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: 62);

            migrationBuilder.UpdateData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "BMW");

            migrationBuilder.UpdateData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Audi");

            migrationBuilder.UpdateData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Chevrolet");

            migrationBuilder.UpdateData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Fiat");

            migrationBuilder.UpdateData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Ford");

            migrationBuilder.UpdateData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "Geely");

            migrationBuilder.UpdateData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: 7,
                column: "Name",
                value: "Volvo");

            migrationBuilder.UpdateData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: 8,
                column: "Name",
                value: "Skoda");

            migrationBuilder.UpdateData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: 9,
                column: "Name",
                value: "Mitsubishi");

            migrationBuilder.UpdateData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: 10,
                column: "Name",
                value: "Honda");

            migrationBuilder.UpdateData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: 11,
                column: "Name",
                value: "ZAZ");

            migrationBuilder.UpdateData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: 12,
                column: "Name",
                value: "Nissan");

            migrationBuilder.UpdateData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: 13,
                column: "Name",
                value: "Hyundai");

            migrationBuilder.UpdateData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: 14,
                column: "Name",
                value: "Volkswagen");

            migrationBuilder.UpdateData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: 15,
                column: "Name",
                value: "Lexus");

            migrationBuilder.UpdateData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: 16,
                column: "Name",
                value: "Mazda");

            migrationBuilder.UpdateData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: 17,
                column: "Name",
                value: "Porsche");

            migrationBuilder.UpdateData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: 18,
                column: "Name",
                value: "Mercedez-Benz");

            migrationBuilder.UpdateData(
                table: "Brand",
                keyColumn: "Id",
                keyValue: 19,
                column: "Name",
                value: "Toyota");

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "1 Series");

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "2 Series");

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "3 Series");

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "4 Series");

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "5 Series");

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "6 Series");

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 7,
                column: "Name",
                value: "7 Series");

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 8,
                column: "Name",
                value: "8 Series");

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 9,
                column: "Name",
                value: "M");

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 10,
                column: "Name",
                value: "M2");

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 11,
                column: "Name",
                value: "M3");

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 12,
                column: "Name",
                value: "M4");

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 13,
                column: "Name",
                value: "M5");

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 14,
                column: "Name",
                value: "M6");

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 15,
                column: "Name",
                value: "X1");

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 16,
                column: "Name",
                value: "X2");

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 17,
                column: "Name",
                value: "X3");

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 18,
                column: "Name",
                value: "X4");

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 1, "X5" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 1, "X6" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 1, "X6 M" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 1, "X7" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 23,
                column: "Name",
                value: "100");

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 24,
                column: "Name",
                value: "80");

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 2, "90" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 2, "A3" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 2, "A4" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 2, "A5" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 2, "A6" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 2, "A7" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 2, "A8" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 2, "Q3" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 2, "Q5" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 2, "Q7" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 2, "Q8" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 3, "Aveo" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 3, "Avalanche 1500" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 3, "Avalanche" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 3, "Avalanche 2500" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 3, "Cavalier" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 3, "Classic" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 3, "Cobalt" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 3, "Colorado Crew Cab" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 44,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 3, "Corvette" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 45,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 3, "Cruze" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 46,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 3, "Equinox" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 47,
                column: "Name",
                value: "124 Spider");

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 48,
                column: "Name",
                value: "500");

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 49,
                column: "Name",
                value: "500 Abarth");

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 50,
                column: "Name",
                value: "500L");

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 51,
                column: "Name",
                value: "500X");

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 52,
                column: "Name",
                value: "500c");

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 53,
                column: "Name",
                value: "500e");

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 54,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 5, "E150 Cargo" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 55,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 5, "E150 Passenger" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 56,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 5, "E150 Super Duty Cargo" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 57,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 5, "E250 Cargo" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 58,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 5, "E350 Super Duty Cargo" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 59,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 5, "Econoline E150 Cargo" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 60,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 5, "Econoline E250 Cargo" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 61,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 5, "Edge" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 62,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 5, "Escape" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 63,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 5, "Escort" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 64,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 5, "Excursion" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 65,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 6, "ATLAS" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 66,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 6, "GC6" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 67,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 6, "GX7" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 68,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 6, "MK" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 69,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 6, "EMGRAND" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 70,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 6, "EMGRAND 7" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 71,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 6, "EMGRAND EC7" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 72,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 6, "EMGRAND GS" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 73,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 6, "TUGELLA" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 74,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 6, "PREFACE" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 75,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 7, "C30" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 76,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 7, "C70" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 77,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 7, "S40" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 78,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 7, "S60" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 79,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 7, "S70" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 80,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 7, "S80" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 81,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 7, "S90" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 82,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 7, "V40" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 83,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 7, "V50" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 84,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 7, "V60" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 85,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 8, "Fabia" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 86,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 8, "SUPERB" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 87,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 8, "OCTAVIA" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 88,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 8, "KAROQ" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 89,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 8, "KODIAQ" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 90,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 8, "SCALA" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 91,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 8, "KAMIQ" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 92,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 8, "Rapid TSI" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 93,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 8, "New Octavia" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 94,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 8, "Vision In Concept" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 95,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 9, "Diamante" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 96,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 9, "Eclipse" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 97,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 9, "Expo" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 98,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 9, "Galant" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 99,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 9, "Endeavor" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 100,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 9, "Lancer" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 101,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 9, "Mirage" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 102,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 9, "Outlander" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 103,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 9, "i-MiEV" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 104,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 9, "Raider Double Cab" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 105,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 10, "Accord" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 106,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 10, "Accord Hybrid" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 107,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 10, "CR-V" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 108,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 10, "Civic" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 109,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 10, "Element" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 110,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 10, "Fit" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 111,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 10, "HR-V" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 112,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 10, "Insight" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 113,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 10, "Odyssey" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 114,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 10, "Passport" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 115,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 11, "Tavria" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 116,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 11, "Slavuta" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 117,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 11, "Lanos" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 118,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 11, "Forza" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 119,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 11, "Vida" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 120,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 11, "Sens" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 121,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 11, "Pick-up" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 122,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 11, "Dana" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 123,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 11, "Vida Cargo" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 124,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 11, "Lanos Cargo" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 125,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 12, "350Z" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 126,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 12, "370Z" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 127,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 12, "Altima" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 128,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 12, "Armada" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 129,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 12, "Frontier King Cab" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 130,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 12, "GT-R" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 131,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 12, "JUKE" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 132,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 12, "LEAF" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 133,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 12, "Maxima" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 134,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 12, "Murano" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 135,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 13, "Accent" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 136,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 13, "Azera" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 137,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 13, "Elantra" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 138,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 13, "Entourage" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 139,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 13, "Equus" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 140,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 13, "Genesis" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 141,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 13, "Genesis Coupe" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 142,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 13, "Ioniq Electric" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 143,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 13, "Kona" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 144,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 13, "NEXO" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 145,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 14, "Arteon" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 146,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 14, "Beetle" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 147,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 14, "CC" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 148,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 14, "Eos" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 149,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 14, "GTI" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 150,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 14, "Golf" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 151,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 14, "Jetta" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 152,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 14, "New Beetle" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 153,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 14, "Passat" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 154,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 14, "Rabbit" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 155,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 15, "ES" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 156,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 15, "CT" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 157,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 15, "GS" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 158,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 15, "GX" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 159,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 15, "IS" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 160,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 15, "LS" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 161,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 15, "LC" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 162,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 15, "RC" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 163,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 15, "LX" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 164,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 15, "SC" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 165,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 16, "CX-3" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 166,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 16, "CX-30" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 167,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 16, "CX-5" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 168,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 16, "CX-7" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 169,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 16, "CX-9" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 170,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 16, "MAZDA2" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 171,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 16, "MAZDA3" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 172,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 16, "MAZDA5" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 173,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 16, "MAZDA6" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 174,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 16, "MX-5 Miata" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 175,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 17, "911" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 176,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 17, "Boxster" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 177,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 17, "Cayenne" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 178,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 17, "Cayman" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 179,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 17, "Macan" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 180,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 17, "Panamera" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 181,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 17, "Taycan" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 182,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 17, "718 Boxster" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 183,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 17, "718 Cayman" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 184,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 17, "Cayenne Coupe" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 185,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 18, "A-Class" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 186,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 18, "B-Class" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 187,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 18, "C-Class" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 188,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 18, "CL-Class" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 189,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 18, "CLS" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 190,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 18, "E-Class" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 191,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 18, "G-Class" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 192,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 18, "GLA" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 193,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 18, "M-Class" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 194,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 18, "S-Class" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 195,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 19, "4Runner" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 196,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 19, "Avalon" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 197,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 19, "86" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 198,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 19, "Camry" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 199,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 19, "Camry Hybrid" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 200,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 19, "Corolla" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 201,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 19, "Highlander" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 202,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 19, "Land Cruiser" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 203,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 19, "Mirai" });

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 204,
                columns: new[] { "BrandId", "Name" },
                values: new object[] { 19, "Prius" });
        }
    }
}
