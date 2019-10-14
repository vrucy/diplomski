using Microsoft.EntityFrameworkCore.Migrations;

namespace AdvokatskiPortal.Migrations
{
    public partial class addslucajMajstorIsReadKorisnik : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isReadOdbijenAdvokat",
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
                name: "isReadOdbijenAdvokat",
                table: "SlucajMajstors");

            migrationBuilder.DropColumn(
                name: "isReadOdbijenKorisnik",
                table: "SlucajMajstors");
        }
    }
}
