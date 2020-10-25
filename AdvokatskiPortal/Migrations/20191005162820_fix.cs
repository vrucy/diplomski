using Microsoft.EntityFrameworkCore.Migrations;

namespace CraftmanPortal.Migrations
{
    public partial class fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Craftmans_Kategorijas_KategorijaId",
                table: "Craftmans");

            migrationBuilder.DropIndex(
                name: "IX_Craftmans_KategorijaId",
                table: "Craftmans");

            migrationBuilder.DropColumn(
                name: "KategorijaId",
                table: "Craftmans");

            migrationBuilder.AddColumn<int>(
                name: "CraftmanId",
                table: "Kategorijas",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Kategorijas_CraftmanId",
                table: "Kategorijas",
                column: "CraftmanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Kategorijas_Craftmans_CraftmanId",
                table: "Kategorijas",
                column: "CraftmanId",
                principalTable: "Craftmans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kategorijas_Craftmans_CraftmanId",
                table: "Kategorijas");

            migrationBuilder.DropIndex(
                name: "IX_Kategorijas_CraftmanId",
                table: "Kategorijas");

            migrationBuilder.DropColumn(
                name: "CraftmanId",
                table: "Kategorijas");

            migrationBuilder.AddColumn<int>(
                name: "KategorijaId",
                table: "Craftmans",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Craftmans_KategorijaId",
                table: "Craftmans",
                column: "KategorijaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Craftmans_Kategorijas_KategorijaId",
                table: "Craftmans",
                column: "KategorijaId",
                principalTable: "Kategorijas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
