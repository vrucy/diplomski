using Microsoft.EntityFrameworkCore.Migrations;

namespace AdvokatskiPortal.Migrations
{
    public partial class fixRealationSlucajSlika : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Slucajs_Slika_SlikaId",
                table: "Slucajs");

            migrationBuilder.DropIndex(
                name: "IX_Slucajs_SlikaId",
                table: "Slucajs");

            migrationBuilder.DropColumn(
                name: "SlikaId",
                table: "Slucajs");

            migrationBuilder.AddColumn<int>(
                name: "PocetakRada",
                table: "Slucajs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ZavrsetakRada",
                table: "Slucajs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SlucajId",
                table: "Slika",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Slika_SlucajId",
                table: "Slika",
                column: "SlucajId");

            migrationBuilder.AddForeignKey(
                name: "FK_Slika_Slucajs_SlucajId",
                table: "Slika",
                column: "SlucajId",
                principalTable: "Slucajs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Slika_Slucajs_SlucajId",
                table: "Slika");

            migrationBuilder.DropIndex(
                name: "IX_Slika_SlucajId",
                table: "Slika");

            migrationBuilder.DropColumn(
                name: "PocetakRada",
                table: "Slucajs");

            migrationBuilder.DropColumn(
                name: "ZavrsetakRada",
                table: "Slucajs");

            migrationBuilder.DropColumn(
                name: "SlucajId",
                table: "Slika");

            migrationBuilder.AddColumn<int>(
                name: "SlikaId",
                table: "Slucajs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Slucajs_SlikaId",
                table: "Slucajs",
                column: "SlikaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Slucajs_Slika_SlikaId",
                table: "Slucajs",
                column: "SlikaId",
                principalTable: "Slika",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
