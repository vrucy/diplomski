using Microsoft.EntityFrameworkCore.Migrations;

namespace MajstorskiPortal.Migrations
{
    public partial class addslucajMajstorIsReadKorisnik : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isReadOdbijenMajstor",
                table: "SlucajMajstors",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isReadOdbijenKorisnik",
                table: "SlucajMajstors",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isReadOdbijenMajstor",
                table: "SlucajMajstors");

            migrationBuilder.DropColumn(
                name: "isReadOdbijenKorisnik",
                table: "SlucajMajstors");
        }
    }
}
