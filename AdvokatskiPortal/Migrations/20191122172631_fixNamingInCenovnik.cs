using Microsoft.EntityFrameworkCore.Migrations;

namespace ContractorskiPortal.Migrations
{
    public partial class fixNamingInCenovnik : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Kreiranje",
                table: "Cenovniks",
                newName: "PrimanjeCasea");

            migrationBuilder.RenameColumn(
                name: "Izmena",
                table: "Cenovniks",
                newName: "IzmenaCasea");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PrimanjeCasea",
                table: "Cenovniks",
                newName: "Kreiranje");

            migrationBuilder.RenameColumn(
                name: "IzmenaCasea",
                table: "Cenovniks",
                newName: "Izmena");
        }
    }
}
