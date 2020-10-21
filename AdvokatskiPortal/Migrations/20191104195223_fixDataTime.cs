using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ContractorskiPortal.Migrations
{
    public partial class fixDataTFirstName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "zavrsetakRada",
                table: "Cenovniks",
                nullable: true,
                oldClrType: typeof(DateTime));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "zavrsetakRada",
                table: "Cenovniks",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}
