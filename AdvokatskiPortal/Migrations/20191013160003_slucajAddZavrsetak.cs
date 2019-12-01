using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MajstorskiPortal.Migrations
{
    public partial class slucajAddZavrsetak : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ZavrsetakRada",
                table: "SlucajMajstors");

            migrationBuilder.AddColumn<DateTime>(
                name: "zavrsetakRada",
                table: "Slucajs",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "zavrsetakRada",
                table: "Slucajs");

            migrationBuilder.AddColumn<DateTime>(
                name: "ZavrsetakRada",
                table: "SlucajMajstors",
                nullable: true);
        }
    }
}
