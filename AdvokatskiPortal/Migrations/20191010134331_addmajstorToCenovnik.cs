using Microsoft.EntityFrameworkCore.Migrations;

namespace ContractorskiPortal.Migrations
{
    public partial class addContractorToCenovnik : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContractorId",
                table: "Cenovniks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Cenovniks_ContractorId",
                table: "Cenovniks",
                column: "ContractorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cenovniks_Contractors_ContractorId",
                table: "Cenovniks",
                column: "ContractorId",
                principalTable: "Contractors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cenovniks_Contractors_ContractorId",
                table: "Cenovniks");

            migrationBuilder.DropIndex(
                name: "IX_Cenovniks_ContractorId",
                table: "Cenovniks");

            migrationBuilder.DropColumn(
                name: "ContractorId",
                table: "Cenovniks");
        }
    }
}
