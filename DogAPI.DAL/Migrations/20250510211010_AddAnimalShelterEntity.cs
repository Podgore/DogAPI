using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DogAPI.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddAnimalShelterEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TailLenght",
                table: "Dogs",
                newName: "TailLength");

            migrationBuilder.AddColumn<Guid>(
                name: "AnimalShelterId",
                table: "Dogs",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Dogs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "Dogs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "AnimalShelters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberOfAnimals = table.Column<int>(type: "int", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalShelters", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dogs_AnimalShelterId",
                table: "Dogs",
                column: "AnimalShelterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dogs_AnimalShelters_AnimalShelterId",
                table: "Dogs",
                column: "AnimalShelterId",
                principalTable: "AnimalShelters",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dogs_AnimalShelters_AnimalShelterId",
                table: "Dogs");

            migrationBuilder.DropTable(
                name: "AnimalShelters");

            migrationBuilder.DropIndex(
                name: "IX_Dogs_AnimalShelterId",
                table: "Dogs");

            migrationBuilder.DropColumn(
                name: "AnimalShelterId",
                table: "Dogs");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Dogs");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Dogs");

            migrationBuilder.RenameColumn(
                name: "TailLength",
                table: "Dogs",
                newName: "TailLenght");
        }
    }
}
