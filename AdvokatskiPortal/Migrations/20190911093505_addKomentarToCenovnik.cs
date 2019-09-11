using Microsoft.EntityFrameworkCore.Migrations;

namespace AdvokatskiPortal.Migrations
{
    public partial class addKomentarToCenovnik : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "komentar",
                table: "Cenovniks",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "komentar",
                table: "Cenovniks");
        }
    }
}
