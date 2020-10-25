using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CraftmanPortal.Migrations
{
    public partial class changeModelsToEng : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cases_Kategorijas_KategorijaId",
                table: "Cases");

            migrationBuilder.DropTable(
                name: "Cenovniks");

            migrationBuilder.DropTable(
                name: "CraftmanKategorijes");

            migrationBuilder.DropTable(
                name: "Kategorijas");

            migrationBuilder.DropColumn(
                name: "Mesto",
                table: "Cases");

            migrationBuilder.DropColumn(
                name: "Naziv",
                table: "Cases");

            migrationBuilder.DropColumn(
                name: "CraftmanIdStr",
                table: "CaseCraftmans");

            migrationBuilder.DropColumn(
                name: "isReject",
                table: "CaseCraftmans");

            migrationBuilder.RenameColumn(
                name: "Ulica",
                table: "Users",
                newName: "Street");

            migrationBuilder.RenameColumn(
                name: "PrezFirstName",
                table: "Users",
                newName: "Place");

            migrationBuilder.RenameColumn(
                name: "Mesto",
                table: "Users",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "slikaProp",
                table: "Slikas",
                newName: "PictureBytes");

            migrationBuilder.RenameColumn(
                name: "Naziv",
                table: "Slikas",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "TFirstNameStamp",
                table: "Notifications",
                newName: "TimeStamp");

            migrationBuilder.RenameColumn(
                name: "Ulica",
                table: "Craftmans",
                newName: "Street");

            migrationBuilder.RenameColumn(
                name: "PrezFirstName",
                table: "Craftmans",
                newName: "Place");

            migrationBuilder.RenameColumn(
                name: "Mesto",
                table: "Craftmans",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "UlicaIBroj",
                table: "Cases",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Opis",
                table: "Cases",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "KrajnjiRokZaOdgovor",
                table: "Cases",
                newName: "DeadLineForAnswer");

            migrationBuilder.RenameColumn(
                name: "KategorijaId",
                table: "Cases",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Cases_KategorijaId",
                table: "Cases",
                newName: "IX_Cases_CategoryId");

            migrationBuilder.RenameColumn(
                name: "datumKreiranja",
                table: "CaseCraftmans",
                newName: "CreationDate");

            migrationBuilder.RenameColumn(
                name: "Odgovor",
                table: "CaseCraftmans",
                newName: "CraftmanIdIndentity");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ParentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TypeOfPayment = table.Column<string>(nullable: true),
                    QuantitySize = table.Column<string>(nullable: true),
                    Comment = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: true),
                    FinishDate = table.Column<DateTime>(nullable: true),
                    ReciveCase = table.Column<DateTime>(nullable: true),
                    ChangeCaseDate = table.Column<DateTime>(nullable: true),
                    CaseId = table.Column<int>(nullable: false),
                    StatusId = table.Column<int>(nullable: false),
                    CraftmanId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contracts_Cases_CaseId",
                        column: x => x.CaseId,
                        principalTable: "Cases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contracts_Craftmans_CraftmanId",
                        column: x => x.CraftmanId,
                        principalTable: "Craftmans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contracts_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContractCategores",
                columns: table => new
                {
                    CraftmanId = table.Column<int>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractCategores", x => new { x.CraftmanId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_ContractCategores_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContractCategores_Craftmans_CraftmanId",
                        column: x => x.CraftmanId,
                        principalTable: "Craftmans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentId",
                table: "Categories",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractCategores_CategoryId",
                table: "ContractCategores",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_CaseId",
                table: "Contracts",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_CraftmanId",
                table: "Contracts",
                column: "CraftmanId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_StatusId",
                table: "Contracts",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_Categories_CategoryId",
                table: "Cases",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cases_Categories_CategoryId",
                table: "Cases");

            migrationBuilder.DropTable(
                name: "ContractCategores");

            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.RenameColumn(
                name: "Street",
                table: "Users",
                newName: "Ulica");

            migrationBuilder.RenameColumn(
                name: "Place",
                table: "Users",
                newName: "PrezFirstName");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Users",
                newName: "Mesto");

            migrationBuilder.RenameColumn(
                name: "PictureBytes",
                table: "Slikas",
                newName: "slikaProp");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Slikas",
                newName: "Naziv");

            migrationBuilder.RenameColumn(
                name: "TimeStamp",
                table: "Notifications",
                newName: "TFirstNameStamp");

            migrationBuilder.RenameColumn(
                name: "Street",
                table: "Craftmans",
                newName: "Ulica");

            migrationBuilder.RenameColumn(
                name: "Place",
                table: "Craftmans",
                newName: "PrezFirstName");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Craftmans",
                newName: "Mesto");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Cases",
                newName: "UlicaIBroj");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Cases",
                newName: "Opis");

            migrationBuilder.RenameColumn(
                name: "DeadLineForAnswer",
                table: "Cases",
                newName: "KrajnjiRokZaOdgovor");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Cases",
                newName: "KategorijaId");

            migrationBuilder.RenameIndex(
                name: "IX_Cases_CategoryId",
                table: "Cases",
                newName: "IX_Cases_KategorijaId");

            migrationBuilder.RenameColumn(
                name: "CreationDate",
                table: "CaseCraftmans",
                newName: "datumKreiranja");

            migrationBuilder.RenameColumn(
                name: "CraftmanIdIndentity",
                table: "CaseCraftmans",
                newName: "Odgovor");

            migrationBuilder.AddColumn<string>(
                name: "Mesto",
                table: "Cases",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Naziv",
                table: "Cases",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CraftmanIdStr",
                table: "CaseCraftmans",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isReject",
                table: "CaseCraftmans",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Cenovniks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CaseId = table.Column<int>(nullable: false),
                    CraftmanId = table.Column<int>(nullable: false),
                    IdenityId = table.Column<string>(nullable: true),
                    IzmenaCasea = table.Column<DateTime>(nullable: true),
                    PocetakRada = table.Column<DateTime>(nullable: true),
                    PrimanjeCasea = table.Column<DateTime>(nullable: true),
                    StatusId = table.Column<int>(nullable: false),
                    isKonacan = table.Column<bool>(nullable: false),
                    kolicina = table.Column<string>(nullable: true),
                    komentar = table.Column<string>(nullable: true),
                    vrstaPlacanja = table.Column<string>(nullable: true),
                    zavrsetakRada = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cenovniks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cenovniks_Cases_CaseId",
                        column: x => x.CaseId,
                        principalTable: "Cases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cenovniks_Craftmans_CraftmanId",
                        column: x => x.CraftmanId,
                        principalTable: "Craftmans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cenovniks_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Kategorijas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(nullable: true),
                    Opis = table.Column<string>(nullable: true),
                    ParentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategorijas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kategorijas_Kategorijas_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Kategorijas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CraftmanKategorijes",
                columns: table => new
                {
                    CraftmanId = table.Column<int>(nullable: false),
                    KategorijaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CraftmanKategorijes", x => new { x.CraftmanId, x.KategorijaId });
                    table.ForeignKey(
                        name: "FK_CraftmanKategorijes_Craftmans_CraftmanId",
                        column: x => x.CraftmanId,
                        principalTable: "Craftmans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CraftmanKategorijes_Kategorijas_KategorijaId",
                        column: x => x.KategorijaId,
                        principalTable: "Kategorijas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cenovniks_CaseId",
                table: "Cenovniks",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Cenovniks_CraftmanId",
                table: "Cenovniks",
                column: "CraftmanId");

            migrationBuilder.CreateIndex(
                name: "IX_Cenovniks_StatusId",
                table: "Cenovniks",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_CraftmanKategorijes_KategorijaId",
                table: "CraftmanKategorijes",
                column: "KategorijaId");

            migrationBuilder.CreateIndex(
                name: "IX_Kategorijas_ParentId",
                table: "Kategorijas",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_Kategorijas_KategorijaId",
                table: "Cases",
                column: "KategorijaId",
                principalTable: "Kategorijas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
