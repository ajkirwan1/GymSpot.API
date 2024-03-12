using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GymSpot.API.Migrations
{
    /// <inheritdoc />
    public partial class Firstmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Difficulties",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Difficulties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExerciseItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BodyArea = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DifficultyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExerciseItems_Difficulties_DifficultyId",
                        column: x => x.DifficultyId,
                        principalTable: "Difficulties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("0effa98d-4d39-4896-bc75-456e21d7459f"), "Medium" },
                    { new Guid("6661afe2-11a6-43c1-a1ec-44273a01ebd8"), "Hard" },
                    { new Guid("ce99eb20-a50e-401a-a2ea-ce0065239cff"), "Easy" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { new Guid("434fdcd7-2959-4325-aac8-7004e5088c7e"), "LON", "London" },
                    { new Guid("ae600fca-91f0-42f2-ac9f-3c0901fec94d"), "HAM", "Hampshire" },
                    { new Guid("e792ba02-0630-4512-8b47-1e4770ded925"), "DOR", "Dorset" },
                    { new Guid("e9d163dd-3085-4525-8033-febe455eb071"), "WIL", "Wiltshire" }
                });

            migrationBuilder.InsertData(
                table: "ExerciseItems",
                columns: new[] { "Id", "BodyArea", "Description", "DifficultyId", "Name" },
                values: new object[,]
                {
                    { new Guid("88cf4342-5f1c-4e29-9a0f-10c924913e60"), "Upper", "A super upper body exercise", new Guid("6661afe2-11a6-43c1-a1ec-44273a01ebd8"), "Bench-press" },
                    { new Guid("d4e49c0a-d227-421e-8e58-32ba9fc70e68"), "Upper", "A super upper body exercise", new Guid("6661afe2-11a6-43c1-a1ec-44273a01ebd8"), "Bench-press" },
                    { new Guid("e2b54ae6-60f5-4718-994d-f380cd9daaf3"), "Upper", "A great all-rounder", new Guid("6661afe2-11a6-43c1-a1ec-44273a01ebd8"), "Pull-up" },
                    { new Guid("e62e7403-307d-4c85-8d76-292c7d3923f5"), "Lower", "A super upper body exercise", new Guid("6661afe2-11a6-43c1-a1ec-44273a01ebd8"), "Squat" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "Password", "PhoneNumber", "RegionId", "Role" },
                values: new object[] { new Guid("8505d655-067a-48a5-acf3-65deec25bb41"), "AnEmail", "Adam Smith", null, 0, new Guid("434fdcd7-2959-4325-aac8-7004e5088c7e"), null });

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseItems_DifficultyId",
                table: "ExerciseItems",
                column: "DifficultyId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RegionId",
                table: "Users",
                column: "RegionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExerciseItems");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Difficulties");

            migrationBuilder.DropTable(
                name: "Regions");
        }
    }
}
