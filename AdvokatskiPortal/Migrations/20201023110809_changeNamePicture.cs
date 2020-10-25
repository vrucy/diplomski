using Microsoft.EntityFrameworkCore.Migrations;

namespace CraftmanPortal.Migrations
{
    public partial class changeNamePicture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Slikas_Cases_CaseId",
                table: "Slikas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Slikas",
                table: "Slikas");

            migrationBuilder.RenameTable(
                name: "Slikas",
                newName: "Pictures");

            migrationBuilder.RenameColumn(
                name: "QuantitySize",
                table: "Contracts",
                newName: "Price");

            migrationBuilder.RenameIndex(
                name: "IX_Slikas_CaseId",
                table: "Pictures",
                newName: "IX_Pictures_CaseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pictures",
                table: "Pictures",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pictures_Cases_CaseId",
                table: "Pictures",
                column: "CaseId",
                principalTable: "Cases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pictures_Cases_CaseId",
                table: "Pictures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pictures",
                table: "Pictures");

            migrationBuilder.RenameTable(
                name: "Pictures",
                newName: "Slikas");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Contracts",
                newName: "QuantitySize");

            migrationBuilder.RenameIndex(
                name: "IX_Pictures_CaseId",
                table: "Slikas",
                newName: "IX_Slikas_CaseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Slikas",
                table: "Slikas",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Slikas_Cases_CaseId",
                table: "Slikas",
                column: "CaseId",
                principalTable: "Cases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
