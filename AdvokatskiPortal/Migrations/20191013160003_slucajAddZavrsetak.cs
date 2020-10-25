using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CraftmanPortal.Migrations
{
    public partial class CaseAddZavrsetak : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ZavrsetakRada",
                table: "CaseCraftmans");

            migrationBuilder.AddColumn<DateTime>(
                name: "zavrsetakRada",
                table: "Cases",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "zavrsetakRada",
                table: "Cases");

            migrationBuilder.AddColumn<DateTime>(
                name: "ZavrsetakRada",
                table: "CaseCraftmans",
                nullable: true);
        }
    }
}
