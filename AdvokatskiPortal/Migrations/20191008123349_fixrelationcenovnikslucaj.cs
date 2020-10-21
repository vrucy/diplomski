using Microsoft.EntityFrameworkCore.Migrations;

namespace ContractorskiPortal.Migrations
{
    public partial class fixrelationcenovnikCase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Cenovniks_CaseId",
                table: "Cenovniks");

            migrationBuilder.DropColumn(
                name: "CenovnikId",
                table: "Cases");

            migrationBuilder.CreateIndex(
                name: "IX_Cenovniks_CaseId",
                table: "Cenovniks",
                column: "CaseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Cenovniks_CaseId",
                table: "Cenovniks");

            migrationBuilder.AddColumn<int>(
                name: "CenovnikId",
                table: "Cases",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Cenovniks_CaseId",
                table: "Cenovniks",
                column: "CaseId",
                unique: true);
        }
    }
}
