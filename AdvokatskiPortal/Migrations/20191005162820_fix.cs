using Microsoft.EntityFrameworkCore.Migrations;

namespace MajstorskiPortal.Migrations
{
    public partial class fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Majstors_Kategorijas_KategorijaId",
                table: "Majstors");

            migrationBuilder.DropIndex(
                name: "IX_Majstors_KategorijaId",
                table: "Majstors");

            migrationBuilder.DropColumn(
                name: "KategorijaId",
                table: "Majstors");

            migrationBuilder.AddColumn<int>(
                name: "MajstorId",
                table: "Kategorijas",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Kategorijas_MajstorId",
                table: "Kategorijas",
                column: "MajstorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Kategorijas_Majstors_MajstorId",
                table: "Kategorijas",
                column: "MajstorId",
                principalTable: "Majstors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kategorijas_Majstors_MajstorId",
                table: "Kategorijas");

            migrationBuilder.DropIndex(
                name: "IX_Kategorijas_MajstorId",
                table: "Kategorijas");

            migrationBuilder.DropColumn(
                name: "MajstorId",
                table: "Kategorijas");

            migrationBuilder.AddColumn<int>(
                name: "KategorijaId",
                table: "Majstors",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Majstors_KategorijaId",
                table: "Majstors",
                column: "KategorijaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Majstors_Kategorijas_KategorijaId",
                table: "Majstors",
                column: "KategorijaId",
                principalTable: "Kategorijas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
