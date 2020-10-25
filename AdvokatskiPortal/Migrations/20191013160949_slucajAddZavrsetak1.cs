using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CraftmanPortal.Migrations
{
    public partial class CaseAddZavrsetak1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PocetakRada",
                table: "Cases");

            migrationBuilder.DropColumn(
                name: "zavrsetakRada",
                table: "Cases");

            migrationBuilder.AddColumn<DateTime>(
                name: "PocetakRada",
                table: "Cenovniks",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "zavrsetakRada",
                table: "Cenovniks",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PocetakRada",
                table: "Cenovniks");

            migrationBuilder.DropColumn(
                name: "zavrsetakRada",
                table: "Cenovniks");

            migrationBuilder.AddColumn<DateTime>(
                name: "PocetakRada",
                table: "Cases",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "zavrsetakRada",
                table: "Cases",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
