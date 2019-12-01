using Microsoft.EntityFrameworkCore.Migrations;

namespace MajstorskiPortal.Migrations
{
    public partial class cenovnikAddIsKonacan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isKonacan",
                table: "Cenovniks",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isKonacan",
                table: "Cenovniks");
        }
    }
}
