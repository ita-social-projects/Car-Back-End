using Car.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Car.Data.EntityConfigurations
{
    public static class SeedData
    {
        public static void Seed(this ModelBuilder builder)
        {
            SeedBrands(builder.Entity<Brand>());
            SeedModels(builder.Entity<Model>());
        }

        public static void SeedBrands(EntityTypeBuilder<Brand> builder)
        {
            builder.HasData(
                    new Brand()
                    {
                        Id = 1,
                        Name = "BMW",
                    },
                    new Brand()
                    {
                        Id = 2,
                        Name = "Audi",
                    },
                    new Brand()
                    {
                        Id = 3,
                        Name = "Chevrolet",
                    },
                    new Brand()
                    {
                        Id = 4,
                        Name = "Fiat",
                    },
                    new Brand()
                    {
                        Id = 5,
                        Name = "Ford",
                    },
                    new Brand()
                    {
                        Id = 6,
                        Name = "Geely",
                    },
                    new Brand()
                    {
                        Id = 7,
                        Name = "Volvo",
                    },
                    new Brand()
                    {
                        Id = 8,
                        Name = "Skoda",
                    },
                    new Brand()
                    {
                        Id = 9,
                        Name = "Mitsubishi",
                    },
                    new Brand()
                    {
                        Id = 10,
                        Name = "Honda",
                    },
                    new Brand()
                    {
                        Id = 11,
                        Name = "ZAZ",
                    },
                    new Brand()
                    {
                        Id = 12,
                        Name = "Nissan",
                    },
                    new Brand()
                    {
                        Id = 13,
                        Name = "Hyundai",
                    },
                    new Brand()
                    {
                        Id = 14,
                        Name = "Volkswagen",
                    },
                    new Brand()
                    {
                        Id = 15,
                        Name = "Lexus",
                    },
                    new Brand()
                    {
                        Id = 16,
                        Name = "Mazda",
                    },
                    new Brand()
                    {
                        Id = 17,
                        Name = "Porsche",
                    },
                    new Brand()
                    {
                        Id = 18,
                        Name = "Mercedez-Benz",
                    },
                    new Brand()
                    {
                        Id = 19,
                        Name = "Toyota",
                    });
        }

        public static void SeedModels(EntityTypeBuilder<Model> builder)
        {
            builder.HasData(
               new Model()
               {
                   Id = 1,
                   Name = "1 Series",
                   BrandId = 1,
               },
               new Model()
               {
                   Id = 2,
                   Name = "2 Series",
                   BrandId = 1,
               },
               new Model()
               {
                   Id = 3,
                   Name = "3 Series",
                   BrandId = 1,
               },
               new Model()
               {
                   Id = 4,
                   Name = "4 Series",
                   BrandId = 1,
               },
               new Model()
               {
                   Id = 5,
                   Name = "5 Series",
                   BrandId = 1,
               },
               new Model()
               {
                   Id = 6,
                   Name = "6 Series",
                   BrandId = 1,
               },
               new Model()
               {
                   Id = 7,
                   Name = "7 Series",
                   BrandId = 1,
               },
               new Model()
               {
                   Id = 8,
                   Name = "8 Series",
                   BrandId = 1,
               },
               new Model()
               {
                   Id = 9,
                   Name = "M",
                   BrandId = 1,
               },
               new Model()
               {
                   Id = 10,
                   Name = "M2",
                   BrandId = 1,
               },
               new Model()
               {
                   Id = 11,
                   Name = "M3",
                   BrandId = 1,
               },
               new Model()
               {
                   Id = 12,
                   Name = "M4",
                   BrandId = 1,
               },
               new Model()
               {
                   Id = 13,
                   Name = "M5",
                   BrandId = 1,
               },
               new Model()
               {
                   Id = 14,
                   Name = "M6",
                   BrandId = 1,
               },
               new Model()
               {
                   Id = 15,
                   Name = "X1",
                   BrandId = 1,
               },
               new Model()
               {
                   Id = 16,
                   Name = "X2",
                   BrandId = 1,
               },
               new Model()
               {
                   Id = 17,
                   Name = "X3",
                   BrandId = 1,
               },
               new Model()
               {
                   Id = 18,
                   Name = "X4",
                   BrandId = 1,
               },
               new Model()
               {
                   Id = 19,
                   Name = "X5",
                   BrandId = 1,
               },
               new Model()
               {
                   Id = 20,
                   Name = "X6",
                   BrandId = 1,
               },
               new Model()
               {
                   Id = 21,
                   Name = "X6 M",
                   BrandId = 1,
               },
               new Model()
               {
                   Id = 22,
                   Name = "X7",
                   BrandId = 1,
               },
               new Model()
               {
                   Id = 23,
                   Name = "100",
                   BrandId = 2,
               },
               new Model()
               {
                   Id = 24,
                   Name = "80",
                   BrandId = 2,
               },
               new Model()
               {
                   Id = 25,
                   Name = "90",
                   BrandId = 2,
               },
               new Model()
               {
                   Id = 26,
                   Name = "A3",
                   BrandId = 2,
               },
               new Model()
               {
                   Id = 27,
                   Name = "A4",
                   BrandId = 2,
               },
               new Model()
               {
                   Id = 28,
                   Name = "A5",
                   BrandId = 2,
               },
               new Model()
               {
                   Id = 29,
                   Name = "A6",
                   BrandId = 2,
               },
               new Model()
               {
                   Id = 30,
                   Name = "A7",
                   BrandId = 2,
               },
               new Model()
               {
                   Id = 31,
                   Name = "A8",
                   BrandId = 2,
               },
               new Model()
               {
                   Id = 32,
                   Name = "Q3",
                   BrandId = 2,
               },
               new Model()
               {
                   Id = 33,
                   Name = "Q5",
                   BrandId = 2,
               },
               new Model()
               {
                   Id = 34,
                   Name = "Q7",
                   BrandId = 2,
               },
               new Model()
               {
                   Id = 35,
                   Name = "Q8",
                   BrandId = 2,
               },
               new Model()
               {
                   Id = 36,
                   Name = "Aveo",
                   BrandId = 3,
               },
               new Model()
               {
                   Id = 37,
                   Name = "Avalanche 1500",
                   BrandId = 3,
               },
               new Model()
               {
                   Id = 38,
                   Name = "Avalanche",
                   BrandId = 3,
               },
               new Model()
               {
                   Id = 39,
                   Name = "Avalanche 2500",
                   BrandId = 3,
               },
               new Model()
               {
                   Id = 40,
                   Name = "Cavalier",
                   BrandId = 3,
               },
               new Model()
               {
                   Id = 41,
                   Name = "Classic",
                   BrandId = 3,
               },
               new Model()
               {
                   Id = 42,
                   Name = "Cobalt",
                   BrandId = 3,
               },
               new Model()
               {
                   Id = 43,
                   Name = "Colorado Crew Cab",
                   BrandId = 3,
               },
               new Model()
               {
                   Id = 44,
                   Name = "Corvette",
                   BrandId = 3,
               },
               new Model()
               {
                   Id = 45,
                   Name = "Cruze",
                   BrandId = 3,
               },
               new Model()
               {
                   Id = 46,
                   Name = "Equinox",
                   BrandId = 3,
               },
               new Model()
               {
                   Id = 47,
                   Name = "124 Spider",
                   BrandId = 4,
               },
               new Model()
               {
                   Id = 48,
                   Name = "500",
                   BrandId = 4,
               },
               new Model()
               {
                   Id = 49,
                   Name = "500 Abarth",
                   BrandId = 4,
               },
               new Model()
               {
                   Id = 50,
                   Name = "500L",
                   BrandId = 4,
               },
               new Model()
               {
                   Id = 51,
                   Name = "500X",
                   BrandId = 4,
               },
               new Model()
               {
                   Id = 52,
                   Name = "500c",
                   BrandId = 4,
               },
               new Model()
               {
                   Id = 53,
                   Name = "500e",
                   BrandId = 4,
               },
               new Model()
               {
                   Id = 54,
                   Name = "E150 Cargo",
                   BrandId = 5,
               },
               new Model()
               {
                   Id = 55,
                   Name = "E150 Passenger",
                   BrandId = 5,
               },
               new Model()
               {
                   Id = 56,
                   Name = "E150 Super Duty Cargo",
                   BrandId = 5,
               },
               new Model()
               {
                   Id = 57,
                   Name = "E250 Cargo",
                   BrandId = 5,
               },
               new Model()
               {
                   Id = 58,
                   Name = "E350 Super Duty Cargo",
                   BrandId = 5,
               },
               new Model()
               {
                   Id = 59,
                   Name = "Econoline E150 Cargo",
                   BrandId = 5,
               },
               new Model()
               {
                   Id = 60,
                   Name = "Econoline E250 Cargo",
                   BrandId = 5,
               },
               new Model()
               {
                   Id = 61,
                   Name = "Edge",
                   BrandId = 5,
               },
               new Model()
               {
                   Id = 62,
                   Name = "Escape",
                   BrandId = 5,
               },
               new Model()
               {
                   Id = 63,
                   Name = "Escort",
                   BrandId = 5,
               },
               new Model()
               {
                   Id = 64,
                   Name = "Excursion",
                   BrandId = 5,
               },
               new Model()
               {
                   Id = 65,
                   Name = "ATLAS",
                   BrandId = 6,
               },
               new Model()
               {
                   Id = 66,
                   Name = "GC6",
                   BrandId = 6,
               },
               new Model()
               {
                   Id = 67,
                   Name = "GX7",
                   BrandId = 6,
               },
               new Model()
               {
                   Id = 68,
                   Name = "MK",
                   BrandId = 6,
               },
               new Model()
               {
                   Id = 69,
                   Name = "EMGRAND",
                   BrandId = 6,
               },
               new Model()
               {
                   Id = 70,
                   Name = "EMGRAND 7",
                   BrandId = 6,
               },
               new Model()
               {
                   Id = 71,
                   Name = "EMGRAND EC7",
                   BrandId = 6,
               },
               new Model()
               {
                   Id = 72,
                   Name = "EMGRAND GS",
                   BrandId = 6,
               },
               new Model()
               {
                   Id = 73,
                   Name = "TUGELLA",
                   BrandId = 6,
               },
               new Model()
               {
                   Id = 74,
                   Name = "PREFACE",
                   BrandId = 6,
               },
               new Model()
               {
                   Id = 75,
                   Name = "C30",
                   BrandId = 7,
               },
               new Model()
               {
                   Id = 76,
                   Name = "C70",
                   BrandId = 7,
               },
               new Model()
               {
                   Id = 77,
                   Name = "S40",
                   BrandId = 7,
               },
               new Model()
               {
                   Id = 78,
                   Name = "S60",
                   BrandId = 7,
               },
               new Model()
               {
                   Id = 79,
                   Name = "S70",
                   BrandId = 7,
               },
               new Model()
               {
                   Id = 80,
                   Name = "S80",
                   BrandId = 7,
               },
               new Model()
               {
                   Id = 81,
                   Name = "S90",
                   BrandId = 7,
               },
               new Model()
               {
                   Id = 82,
                   Name = "V40",
                   BrandId = 7,
               },
               new Model()
               {
                   Id = 83,
                   Name = "V50",
                   BrandId = 7,
               },
               new Model()
               {
                   Id = 84,
                   Name = "V60",
                   BrandId = 7,
               },
               new Model()
               {
                   Id = 85,
                   Name = "Fabia",
                   BrandId = 8,
               },
               new Model()
               {
                   Id = 86,
                   Name = "SUPERB",
                   BrandId = 8,
               },
               new Model()
               {
                   Id = 87,
                   Name = "OCTAVIA",
                   BrandId = 8,
               },
               new Model()
               {
                   Id = 88,
                   Name = "KAROQ",
                   BrandId = 8,
               },
               new Model()
               {
                   Id = 89,
                   Name = "KODIAQ",
                   BrandId = 8,
               },
               new Model()
               {
                   Id = 90,
                   Name = "SCALA",
                   BrandId = 8,
               },
               new Model()
               {
                   Id = 91,
                   Name = "KAMIQ",
                   BrandId = 8,
               },
               new Model()
               {
                   Id = 92,
                   Name = "Rapid TSI",
                   BrandId = 8,
               },
               new Model()
               {
                   Id = 93,
                   Name = "New Octavia",
                   BrandId = 8,
               },
               new Model()
               {
                   Id = 94,
                   Name = "Vision In Concept",
                   BrandId = 8,
               },
               new Model()
               {
                   Id = 95,
                   Name = "Diamante",
                   BrandId = 9,
               },
               new Model()
               {
                   Id = 96,
                   Name = "Eclipse",
                   BrandId = 9,
               },
               new Model()
               {
                   Id = 97,
                   Name = "Expo",
                   BrandId = 9,
               },
               new Model()
               {
                   Id = 98,
                   Name = "Galant",
                   BrandId = 9,
               },
               new Model()
               {
                   Id = 99,
                   Name = "Endeavor",
                   BrandId = 9,
               },
               new Model()
               {
                   Id = 100,
                   Name = "Lancer",
                   BrandId = 9,
               },
               new Model()
               {
                   Id = 101,
                   Name = "Mirage",
                   BrandId = 9,
               },
               new Model()
               {
                   Id = 102,
                   Name = "Outlander",
                   BrandId = 9,
               },
               new Model()
               {
                   Id = 103,
                   Name = "i-MiEV",
                   BrandId = 9,
               },
               new Model()
               {
                   Id = 104,
                   Name = "Raider Double Cab",
                   BrandId = 9,
               },
               new Model()
               {
                   Id = 105,
                   Name = "Accord",
                   BrandId = 10,
               },
               new Model()
               {
                   Id = 106,
                   Name = "Accord Hybrid",
                   BrandId = 10,
               },
               new Model()
               {
                   Id = 107,
                   Name = "CR-V",
                   BrandId = 10,
               },
               new Model()
               {
                   Id = 108,
                   Name = "Civic",
                   BrandId = 10,
               },
               new Model()
               {
                   Id = 109,
                   Name = "Element",
                   BrandId = 10,
               },
               new Model()
               {
                   Id = 110,
                   Name = "Fit",
                   BrandId = 10,
               },
               new Model()
               {
                   Id = 111,
                   Name = "HR-V",
                   BrandId = 10,
               },
               new Model()
               {
                   Id = 112,
                   Name = "Insight",
                   BrandId = 10,
               },
               new Model()
               {
                   Id = 113,
                   Name = "Odyssey",
                   BrandId = 10,
               },
               new Model()
               {
                   Id = 114,
                   Name = "Passport",
                   BrandId = 10,
               },
               new Model()
               {
                   Id = 115,
                   Name = "Tavria",
                   BrandId = 11,
               },
               new Model()
               {
                   Id = 116,
                   Name = "Slavuta",
                   BrandId = 11,
               },
               new Model()
               {
                   Id = 117,
                   Name = "Lanos",
                   BrandId = 11,
               },
               new Model()
               {
                   Id = 118,
                   Name = "Forza",
                   BrandId = 11,
               },
               new Model()
               {
                   Id = 119,
                   Name = "Vida",
                   BrandId = 11,
               },
               new Model()
               {
                   Id = 120,
                   Name = "Sens",
                   BrandId = 11,
               },
               new Model()
               {
                   Id = 121,
                   Name = "Pick-up",
                   BrandId = 11,
               },
               new Model()
               {
                   Id = 122,
                   Name = "Dana",
                   BrandId = 11,
               },
               new Model()
               {
                   Id = 123,
                   Name = "Vida Cargo",
                   BrandId = 11,
               },
               new Model()
               {
                   Id = 124,
                   Name = "Lanos Cargo",
                   BrandId = 11,
               },
               new Model()
               {
                   Id = 125,
                   Name = "350Z",
                   BrandId = 12,
               },
               new Model()
               {
                   Id = 126,
                   Name = "370Z",
                   BrandId = 12,
               },
               new Model()
               {
                   Id = 127,
                   Name = "Altima",
                   BrandId = 12,
               },
               new Model()
               {
                   Id = 128,
                   Name = "Armada",
                   BrandId = 12,
               },
               new Model()
               {
                   Id = 129,
                   Name = "Frontier King Cab",
                   BrandId = 12,
               },
               new Model()
               {
                   Id = 130,
                   Name = "GT-R",
                   BrandId = 12,
               },
               new Model()
               {
                   Id = 131,
                   Name = "JUKE",
                   BrandId = 12,
               },
               new Model()
               {
                   Id = 132,
                   Name = "LEAF",
                   BrandId = 12,
               },
               new Model()
               {
                   Id = 133,
                   Name = "Maxima",
                   BrandId = 12,
               },
               new Model()
               {
                   Id = 134,
                   Name = "Murano",
                   BrandId = 12,
               },
               new Model()
               {
                   Id = 135,
                   Name = "Accent",
                   BrandId = 13,
               },
               new Model()
               {
                   Id = 136,
                   Name = "Azera",
                   BrandId = 13,
               },
               new Model()
               {
                   Id = 137,
                   Name = "Elantra",
                   BrandId = 13,
               },
               new Model()
               {
                   Id = 138,
                   Name = "Entourage",
                   BrandId = 13,
               },
               new Model()
               {
                   Id = 139,
                   Name = "Equus",
                   BrandId = 13,
               },
               new Model()
               {
                   Id = 140,
                   Name = "Genesis",
                   BrandId = 13,
               },
               new Model()
               {
                   Id = 141,
                   Name = "Genesis Coupe",
                   BrandId = 13,
               },
               new Model()
               {
                   Id = 142,
                   Name = "Ioniq Electric",
                   BrandId = 13,
               },
               new Model()
               {
                   Id = 143,
                   Name = "Kona",
                   BrandId = 13,
               },
               new Model()
               {
                   Id = 144,
                   Name = "NEXO",
                   BrandId = 13,
               },
               new Model()
               {
                   Id = 145,
                   Name = "Arteon",
                   BrandId = 14,
               },
               new Model()
               {
                   Id = 146,
                   Name = "Beetle",
                   BrandId = 14,
               },
               new Model()
               {
                   Id = 147,
                   Name = "CC",
                   BrandId = 14,
               },
               new Model()
               {
                   Id = 148,
                   Name = "Eos",
                   BrandId = 14,
               },
               new Model()
               {
                   Id = 149,
                   Name = "GTI",
                   BrandId = 14,
               },
               new Model()
               {
                   Id = 150,
                   Name = "Golf",
                   BrandId = 14,
               },
               new Model()
               {
                   Id = 151,
                   Name = "Jetta",
                   BrandId = 14,
               },
               new Model()
               {
                   Id = 152,
                   Name = "New Beetle",
                   BrandId = 14,
               },
               new Model()
               {
                   Id = 153,
                   Name = "Passat",
                   BrandId = 14,
               },
               new Model()
               {
                   Id = 154,
                   Name = "Rabbit",
                   BrandId = 14,
               },
               new Model()
               {
                   Id = 155,
                   Name = "ES",
                   BrandId = 15,
               },
               new Model()
               {
                   Id = 156,
                   Name = "CT",
                   BrandId = 15,
               },
               new Model()
               {
                   Id = 157,
                   Name = "GS",
                   BrandId = 15,
               },
               new Model()
               {
                   Id = 158,
                   Name = "GX",
                   BrandId = 15,
               },
               new Model()
               {
                   Id = 159,
                   Name = "IS",
                   BrandId = 15,
               },
               new Model()
               {
                   Id = 160,
                   Name = "LS",
                   BrandId = 15,
               },
               new Model()
               {
                   Id = 161,
                   Name = "LC",
                   BrandId = 15,
               },
               new Model()
               {
                   Id = 162,
                   Name = "RC",
                   BrandId = 15,
               },
               new Model()
               {
                   Id = 163,
                   Name = "LX",
                   BrandId = 15,
               },
               new Model()
               {
                   Id = 164,
                   Name = "SC",
                   BrandId = 15,
               },
               new Model()
               {
                   Id = 165,
                   Name = "CX-3",
                   BrandId = 16,
               },
               new Model()
               {
                   Id = 166,
                   Name = "CX-30",
                   BrandId = 16,
               },
               new Model()
               {
                   Id = 167,
                   Name = "CX-5",
                   BrandId = 16,
               },
               new Model()
               {
                   Id = 168,
                   Name = "CX-7",
                   BrandId = 16,
               },
               new Model()
               {
                   Id = 169,
                   Name = "CX-9",
                   BrandId = 16,
               },
               new Model()
               {
                   Id = 170,
                   Name = "MAZDA2",
                   BrandId = 16,
               },
               new Model()
               {
                   Id = 171,
                   Name = "MAZDA3",
                   BrandId = 16,
               },
               new Model()
               {
                   Id = 172,
                   Name = "MAZDA5",
                   BrandId = 16,
               },
               new Model()
               {
                   Id = 173,
                   Name = "MAZDA6",
                   BrandId = 16,
               },
               new Model()
               {
                   Id = 174,
                   Name = "MX-5 Miata",
                   BrandId = 16,
               },
               new Model()
               {
                   Id = 175,
                   Name = "911",
                   BrandId = 17,
               },
               new Model()
               {
                   Id = 176,
                   Name = "Boxster",
                   BrandId = 17,
               },
               new Model()
               {
                   Id = 177,
                   Name = "Cayenne",
                   BrandId = 17,
               },
               new Model()
               {
                   Id = 178,
                   Name = "Cayman",
                   BrandId = 17,
               },
               new Model()
               {
                   Id = 179,
                   Name = "Macan",
                   BrandId = 17,
               },
               new Model()
               {
                   Id = 180,
                   Name = "Panamera",
                   BrandId = 17,
               },
               new Model()
               {
                   Id = 181,
                   Name = "Taycan",
                   BrandId = 17,
               },
               new Model()
               {
                   Id = 182,
                   Name = "718 Boxster",
                   BrandId = 17,
               },
               new Model()
               {
                   Id = 183,
                   Name = "718 Cayman",
                   BrandId = 17,
               },
               new Model()
               {
                   Id = 184,
                   Name = "Cayenne Coupe",
                   BrandId = 17,
               },
               new Model()
               {
                   Id = 185,
                   Name = "A-Class",
                   BrandId = 18,
               },
               new Model()
               {
                   Id = 186,
                   Name = "B-Class",
                   BrandId = 18,
               },
               new Model()
               {
                   Id = 187,
                   Name = "C-Class",
                   BrandId = 18,
               },
               new Model()
               {
                   Id = 188,
                   Name = "CL-Class",
                   BrandId = 18,
               },
               new Model()
               {
                   Id = 189,
                   Name = "CLS",
                   BrandId = 18,
               },
               new Model()
               {
                   Id = 190,
                   Name = "E-Class",
                   BrandId = 18,
               },
               new Model()
               {
                   Id = 191,
                   Name = "G-Class",
                   BrandId = 18,
               },
               new Model()
               {
                   Id = 192,
                   Name = "GLA",
                   BrandId = 18,
               },
               new Model()
               {
                   Id = 193,
                   Name = "M-Class",
                   BrandId = 18,
               },
               new Model()
               {
                   Id = 194,
                   Name = "S-Class",
                   BrandId = 18,
               },
               new Model()
               {
                   Id = 195,
                   Name = "4Runner",
                   BrandId = 19,
               },
               new Model()
               {
                   Id = 196,
                   Name = "Avalon",
                   BrandId = 19,
               },
               new Model()
               {
                   Id = 197,
                   Name = "86",
                   BrandId = 19,
               },
               new Model()
               {
                   Id = 198,
                   Name = "Camry",
                   BrandId = 19,
               },
               new Model()
               {
                   Id = 199,
                   Name = "Camry Hybrid",
                   BrandId = 19,
               },
               new Model()
               {
                   Id = 200,
                   Name = "Corolla",
                   BrandId = 19,
               },
               new Model()
               {
                   Id = 201,
                   Name = "Highlander",
                   BrandId = 19,
               },
               new Model()
               {
                   Id = 202,
                   Name = "Land Cruiser",
                   BrandId = 19,
               },
               new Model()
               {
                   Id = 203,
                   Name = "Mirai",
                   BrandId = 19,
               },
               new Model()
               {
                   Id = 204,
                   Name = "Prius",
                   BrandId = 19,
               });
        }
    }
}