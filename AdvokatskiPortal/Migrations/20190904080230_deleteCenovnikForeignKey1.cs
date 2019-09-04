using Microsoft.EntityFrameworkCore.Migrations;

namespace AdvokatskiPortal.Migrations
{
    public partial class deleteCenovnikForeignKey1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Cenovniks_StatusId",
                table: "Cenovniks");

            migrationBuilder.CreateIndex(
                name: "IX_Cenovniks_StatusId",
                table: "Cenovniks",
                column: "StatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Cenovniks_StatusId",
                table: "Cenovniks");

            migrationBuilder.CreateIndex(
                name: "IX_Cenovniks_StatusId",
                table: "Cenovniks",
                column: "StatusId",
                unique: true);
        }
    }
}
