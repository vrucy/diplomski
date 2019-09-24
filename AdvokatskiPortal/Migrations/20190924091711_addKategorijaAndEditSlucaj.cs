using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AdvokatskiPortal.Migrations
{
    public partial class addKategorijaAndEditSlucaj : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KategorijaId",
                table: "Slucajs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Lokacija",
                table: "Slucajs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Mesto",
                table: "Slucajs",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SlikaId",
                table: "Slucajs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UlicaIBroj",
                table: "Slucajs",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PodKategorija",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(nullable: true),
                    Opis = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PodKategorija", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Slika",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(nullable: true),
                    slikaProp = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slika", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kategorija",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(nullable: true),
                    Opis = table.Column<string>(nullable: true),
                    PodKategorijaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategorija", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kategorija_PodKategorija_PodKategorijaId",
                        column: x => x.PodKategorijaId,
                        principalTable: "PodKategorija",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Slucajs_KategorijaId",
                table: "Slucajs",
                column: "KategorijaId");

            migrationBuilder.CreateIndex(
                name: "IX_Slucajs_SlikaId",
                table: "Slucajs",
                column: "SlikaId");

            migrationBuilder.CreateIndex(
                name: "IX_Kategorija_PodKategorijaId",
                table: "Kategorija",
                column: "PodKategorijaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Slucajs_Kategorija_KategorijaId",
                table: "Slucajs",
                column: "KategorijaId",
                principalTable: "Kategorija",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Slucajs_Slika_SlikaId",
                table: "Slucajs",
                column: "SlikaId",
                principalTable: "Slika",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Slucajs_Kategorija_KategorijaId",
                table: "Slucajs");

            migrationBuilder.DropForeignKey(
                name: "FK_Slucajs_Slika_SlikaId",
                table: "Slucajs");

            migrationBuilder.DropTable(
                name: "Kategorija");

            migrationBuilder.DropTable(
                name: "Slika");

            migrationBuilder.DropTable(
                name: "PodKategorija");

            migrationBuilder.DropIndex(
                name: "IX_Slucajs_KategorijaId",
                table: "Slucajs");

            migrationBuilder.DropIndex(
                name: "IX_Slucajs_SlikaId",
                table: "Slucajs");

            migrationBuilder.DropColumn(
                name: "KategorijaId",
                table: "Slucajs");

            migrationBuilder.DropColumn(
                name: "Lokacija",
                table: "Slucajs");

            migrationBuilder.DropColumn(
                name: "Mesto",
                table: "Slucajs");

            migrationBuilder.DropColumn(
                name: "SlikaId",
                table: "Slucajs");

            migrationBuilder.DropColumn(
                name: "UlicaIBroj",
                table: "Slucajs");
        }
    }
}
