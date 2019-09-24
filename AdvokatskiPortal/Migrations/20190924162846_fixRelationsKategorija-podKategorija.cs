using Microsoft.EntityFrameworkCore.Migrations;

namespace AdvokatskiPortal.Migrations
{
    public partial class fixRelationsKategorijapodKategorija : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kategorija_PodKategorija_PodKategorijaId",
                table: "Kategorija");

            migrationBuilder.DropIndex(
                name: "IX_Kategorija_PodKategorijaId",
                table: "Kategorija");

            migrationBuilder.DropColumn(
                name: "PodKategorijaId",
                table: "Kategorija");

            migrationBuilder.AddColumn<int>(
                name: "KategorijaId",
                table: "PodKategorija",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PodKategorija_KategorijaId",
                table: "PodKategorija",
                column: "KategorijaId");

            migrationBuilder.AddForeignKey(
                name: "FK_PodKategorija_Kategorija_KategorijaId",
                table: "PodKategorija",
                column: "KategorijaId",
                principalTable: "Kategorija",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PodKategorija_Kategorija_KategorijaId",
                table: "PodKategorija");

            migrationBuilder.DropIndex(
                name: "IX_PodKategorija_KategorijaId",
                table: "PodKategorija");

            migrationBuilder.DropColumn(
                name: "KategorijaId",
                table: "PodKategorija");

            migrationBuilder.AddColumn<int>(
                name: "PodKategorijaId",
                table: "Kategorija",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Kategorija_PodKategorijaId",
                table: "Kategorija",
                column: "PodKategorijaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Kategorija_PodKategorija_PodKategorijaId",
                table: "Kategorija",
                column: "PodKategorijaId",
                principalTable: "PodKategorija",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
