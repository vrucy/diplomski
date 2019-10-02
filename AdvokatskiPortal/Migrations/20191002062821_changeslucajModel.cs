using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AdvokatskiPortal.Migrations
{
    public partial class changeslucajModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Lokacija",
                table: "Slucajs",
                newName: "GSirina");

            migrationBuilder.AddColumn<string>(
                name: "GDuzina",
                table: "Slucajs",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "KrajnjiRokZaOdgovor",
                table: "Slucajs",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GDuzina",
                table: "Slucajs");

            migrationBuilder.DropColumn(
                name: "KrajnjiRokZaOdgovor",
                table: "Slucajs");

            migrationBuilder.RenameColumn(
                name: "GSirina",
                table: "Slucajs",
                newName: "Lokacija");
        }
    }
}
