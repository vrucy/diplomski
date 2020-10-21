using Microsoft.EntityFrameworkCore.Migrations;

namespace ContractorskiPortal.Migrations
{
    public partial class fixModelCase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "GSirina",
                table: "Cases",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "GDuzina",
                table: "Cases",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "GSirina",
                table: "Cases",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<string>(
                name: "GDuzina",
                table: "Cases",
                nullable: true,
                oldClrType: typeof(double));
        }
    }
}
