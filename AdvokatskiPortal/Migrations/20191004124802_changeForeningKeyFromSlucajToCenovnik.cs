using Microsoft.EntityFrameworkCore.Migrations;

namespace CraftmanPortal.Migrations
{
    public partial class changeForeningKeyFromCaseToCenovnik : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cases_Cenovniks_CenovnikId",
                table: "Cases");

            migrationBuilder.DropIndex(
                name: "IX_Cases_CenovnikId",
                table: "Cases");

            migrationBuilder.AlterColumn<int>(
                name: "CenovnikId",
                table: "Cases",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cenovniks_CaseId",
                table: "Cenovniks",
                column: "CaseId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cenovniks_Cases_CaseId",
                table: "Cenovniks",
                column: "CaseId",
                principalTable: "Cases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cenovniks_Cases_CaseId",
                table: "Cenovniks");

            migrationBuilder.DropIndex(
                name: "IX_Cenovniks_CaseId",
                table: "Cenovniks");

            migrationBuilder.AlterColumn<int>(
                name: "CenovnikId",
                table: "Cases",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Cases_CenovnikId",
                table: "Cases",
                column: "CenovnikId",
                unique: true,
                filter: "[CenovnikId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_Cenovniks_CenovnikId",
                table: "Cases",
                column: "CenovnikId",
                principalTable: "Cenovniks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
