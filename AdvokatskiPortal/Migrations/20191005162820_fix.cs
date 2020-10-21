using Microsoft.EntityFrameworkCore.Migrations;

namespace ContractorskiPortal.Migrations
{
    public partial class fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contractors_Kategorijas_KategorijaId",
                table: "Contractors");

            migrationBuilder.DropIndex(
                name: "IX_Contractors_KategorijaId",
                table: "Contractors");

            migrationBuilder.DropColumn(
                name: "KategorijaId",
                table: "Contractors");

            migrationBuilder.AddColumn<int>(
                name: "ContractorId",
                table: "Kategorijas",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Kategorijas_ContractorId",
                table: "Kategorijas",
                column: "ContractorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Kategorijas_Contractors_ContractorId",
                table: "Kategorijas",
                column: "ContractorId",
                principalTable: "Contractors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kategorijas_Contractors_ContractorId",
                table: "Kategorijas");

            migrationBuilder.DropIndex(
                name: "IX_Kategorijas_ContractorId",
                table: "Kategorijas");

            migrationBuilder.DropColumn(
                name: "ContractorId",
                table: "Kategorijas");

            migrationBuilder.AddColumn<int>(
                name: "KategorijaId",
                table: "Contractors",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Contractors_KategorijaId",
                table: "Contractors",
                column: "KategorijaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contractors_Kategorijas_KategorijaId",
                table: "Contractors",
                column: "KategorijaId",
                principalTable: "Kategorijas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
