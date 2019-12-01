using Microsoft.EntityFrameworkCore.Migrations;

namespace MajstorskiPortal.Migrations
{
    public partial class fixNamingInCenovnik : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Kreiranje",
                table: "Cenovniks",
                newName: "PrimanjeSlucaja");

            migrationBuilder.RenameColumn(
                name: "Izmena",
                table: "Cenovniks",
                newName: "IzmenaSlucaja");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PrimanjeSlucaja",
                table: "Cenovniks",
                newName: "Kreiranje");

            migrationBuilder.RenameColumn(
                name: "IzmenaSlucaja",
                table: "Cenovniks",
                newName: "Izmena");
        }
    }
}
