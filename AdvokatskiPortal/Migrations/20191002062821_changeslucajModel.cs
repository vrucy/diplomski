using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ContractorskiPortal.Migrations
{
    public partial class changeCaseModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Lokacija",
                table: "Cases",
                newName: "GSirina");

            migrationBuilder.AddColumn<string>(
                name: "GDuzina",
                table: "Cases",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "KrajnjiRokZaOdgovor",
                table: "Cases",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GDuzina",
                table: "Cases");

            migrationBuilder.DropColumn(
                name: "KrajnjiRokZaOdgovor",
                table: "Cases");

            migrationBuilder.RenameColumn(
                name: "GSirina",
                table: "Cases",
                newName: "Lokacija");
        }
    }
}
