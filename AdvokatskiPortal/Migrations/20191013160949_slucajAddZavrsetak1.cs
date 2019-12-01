using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MajstorskiPortal.Migrations
{
    public partial class slucajAddZavrsetak1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PocetakRada",
                table: "Slucajs");

            migrationBuilder.DropColumn(
                name: "zavrsetakRada",
                table: "Slucajs");

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
                table: "Slucajs",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "zavrsetakRada",
                table: "Slucajs",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
