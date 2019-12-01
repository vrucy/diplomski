using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MajstorskiPortal.Migrations
{
    public partial class AddToCenovnikDateCreateAndChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Izmena",
                table: "Cenovniks",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Kreiranje",
                table: "Cenovniks",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Izmena",
                table: "Cenovniks");

            migrationBuilder.DropColumn(
                name: "Kreiranje",
                table: "Cenovniks");
        }
    }
}
