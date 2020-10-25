using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CraftmanPortal.Migrations
{
    public partial class moveZavrsavanjaInCaseMajsotr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ZavrsetakRada",
                table: "Cases");

            migrationBuilder.AddColumn<DateTime>(
                name: "ZavrsetakRada",
                table: "CaseCraftmans",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ZavrsetakRada",
                table: "CaseCraftmans");

            migrationBuilder.AddColumn<DateTime>(
                name: "ZavrsetakRada",
                table: "Cases",
                nullable: true);
        }
    }
}
