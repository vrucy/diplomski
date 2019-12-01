using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MajstorskiPortal.Migrations
{
    public partial class moveZavrsavanjaInSlucajMajsotr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ZavrsetakRada",
                table: "Slucajs");

            migrationBuilder.AddColumn<DateTime>(
                name: "ZavrsetakRada",
                table: "SlucajMajstors",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ZavrsetakRada",
                table: "SlucajMajstors");

            migrationBuilder.AddColumn<DateTime>(
                name: "ZavrsetakRada",
                table: "Slucajs",
                nullable: true);
        }
    }
}
