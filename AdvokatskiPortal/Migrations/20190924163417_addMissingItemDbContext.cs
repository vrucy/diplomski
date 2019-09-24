using Microsoft.EntityFrameworkCore.Migrations;

namespace AdvokatskiPortal.Migrations
{
    public partial class addMissingItemDbContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PodKategorija_Kategorija_KategorijaId",
                table: "PodKategorija");

            migrationBuilder.DropForeignKey(
                name: "FK_Slucajs_Kategorija_KategorijaId",
                table: "Slucajs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PodKategorija",
                table: "PodKategorija");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Kategorija",
                table: "Kategorija");

            migrationBuilder.RenameTable(
                name: "PodKategorija",
                newName: "PodKategorijas");

            migrationBuilder.RenameTable(
                name: "Kategorija",
                newName: "Kategorijas");

            migrationBuilder.RenameIndex(
                name: "IX_PodKategorija_KategorijaId",
                table: "PodKategorijas",
                newName: "IX_PodKategorijas_KategorijaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PodKategorijas",
                table: "PodKategorijas",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Kategorijas",
                table: "Kategorijas",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PodKategorijas_Kategorijas_KategorijaId",
                table: "PodKategorijas",
                column: "KategorijaId",
                principalTable: "Kategorijas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Slucajs_Kategorijas_KategorijaId",
                table: "Slucajs",
                column: "KategorijaId",
                principalTable: "Kategorijas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PodKategorijas_Kategorijas_KategorijaId",
                table: "PodKategorijas");

            migrationBuilder.DropForeignKey(
                name: "FK_Slucajs_Kategorijas_KategorijaId",
                table: "Slucajs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PodKategorijas",
                table: "PodKategorijas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Kategorijas",
                table: "Kategorijas");

            migrationBuilder.RenameTable(
                name: "PodKategorijas",
                newName: "PodKategorija");

            migrationBuilder.RenameTable(
                name: "Kategorijas",
                newName: "Kategorija");

            migrationBuilder.RenameIndex(
                name: "IX_PodKategorijas_KategorijaId",
                table: "PodKategorija",
                newName: "IX_PodKategorija_KategorijaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PodKategorija",
                table: "PodKategorija",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Kategorija",
                table: "Kategorija",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PodKategorija_Kategorija_KategorijaId",
                table: "PodKategorija",
                column: "KategorijaId",
                principalTable: "Kategorija",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Slucajs_Kategorija_KategorijaId",
                table: "Slucajs",
                column: "KategorijaId",
                principalTable: "Kategorija",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
