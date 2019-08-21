using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AdvokatskiPortal.Migrations
{
    public partial class restDBTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Slucajs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Opis = table.Column<string>(nullable: true),
                    KorisnikId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slucajs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Slucajs_Korisniks_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Korisniks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Specjalnost",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specjalnost", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SlucajAdvokats",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    datumKreiranja = table.Column<DateTime>(nullable: false),
                    prihvacno = table.Column<bool>(nullable: false),
                    Odgovor = table.Column<string>(nullable: true),
                    AdvokatId = table.Column<int>(nullable: false),
                    SlucajId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SlucajAdvokats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SlucajAdvokats_Advokats_AdvokatId",
                        column: x => x.AdvokatId,
                        principalTable: "Advokats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SlucajAdvokats_Slucajs_SlucajId",
                        column: x => x.SlucajId,
                        principalTable: "Slucajs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ugovors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SlucajId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ugovors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ugovors_Slucajs_SlucajId",
                        column: x => x.SlucajId,
                        principalTable: "Slucajs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SpecjalnostiAdvokat",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SpecjalnostId = table.Column<int>(nullable: false),
                    AdvokatId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecjalnostiAdvokat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpecjalnostiAdvokat_Advokats_AdvokatId",
                        column: x => x.AdvokatId,
                        principalTable: "Advokats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SpecjalnostiAdvokat_Specjalnost_SpecjalnostId",
                        column: x => x.SpecjalnostId,
                        principalTable: "Specjalnost",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cenovniks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    vrstaPlacanja = table.Column<string>(nullable: true),
                    kolicina = table.Column<string>(nullable: true),
                    SlucajId = table.Column<int>(nullable: false),
                    StatusId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cenovniks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cenovniks_Slucajs_SlucajId",
                        column: x => x.SlucajId,
                        principalTable: "Slucajs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cenovniks_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cenovniks_SlucajId",
                table: "Cenovniks",
                column: "SlucajId");

            migrationBuilder.CreateIndex(
                name: "IX_Cenovniks_StatusId",
                table: "Cenovniks",
                column: "StatusId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SlucajAdvokats_AdvokatId",
                table: "SlucajAdvokats",
                column: "AdvokatId");

            migrationBuilder.CreateIndex(
                name: "IX_SlucajAdvokats_SlucajId",
                table: "SlucajAdvokats",
                column: "SlucajId");

            migrationBuilder.CreateIndex(
                name: "IX_Slucajs_KorisnikId",
                table: "Slucajs",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecjalnostiAdvokat_AdvokatId",
                table: "SpecjalnostiAdvokat",
                column: "AdvokatId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecjalnostiAdvokat_SpecjalnostId",
                table: "SpecjalnostiAdvokat",
                column: "SpecjalnostId");

            migrationBuilder.CreateIndex(
                name: "IX_Ugovors_SlucajId",
                table: "Ugovors",
                column: "SlucajId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cenovniks");

            migrationBuilder.DropTable(
                name: "SlucajAdvokats");

            migrationBuilder.DropTable(
                name: "SpecjalnostiAdvokat");

            migrationBuilder.DropTable(
                name: "Ugovors");

            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.DropTable(
                name: "Specjalnost");

            migrationBuilder.DropTable(
                name: "Slucajs");
        }
    }
}
