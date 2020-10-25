using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CraftmanPortal.Migrations
{
    public partial class fixEntireDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cenovniks_AspNetUsers_IdenityId",
                table: "Cenovniks");

            migrationBuilder.DropTable(
                name: "Ugovors");

            migrationBuilder.DropIndex(
                name: "IX_Cenovniks_IdenityId",
                table: "Cenovniks");

            migrationBuilder.DropColumn(
                name: "isRead",
                table: "CaseCraftmans");

            migrationBuilder.DropColumn(
                name: "isReadOdbijenCraftman",
                table: "CaseCraftmans");

            migrationBuilder.DropColumn(
                name: "isReadOdbijenUser",
                table: "CaseCraftmans");

            migrationBuilder.DropColumn(
                name: "prihvacno",
                table: "CaseCraftmans");

            migrationBuilder.AlterColumn<string>(
                name: "IdenityId",
                table: "Cenovniks",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isRead",
                table: "CaseCraftmans",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isReadOdbijenCraftman",
                table: "CaseCraftmans",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isReadOdbijenUser",
                table: "CaseCraftmans",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "prihvacno",
                table: "CaseCraftmans",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "IdenityId",
                table: "Cenovniks",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Ugovors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CaseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ugovors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ugovors_Cases_CaseId",
                        column: x => x.CaseId,
                        principalTable: "Cases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cenovniks_IdenityId",
                table: "Cenovniks",
                column: "IdenityId");

            migrationBuilder.CreateIndex(
                name: "IX_Ugovors_CaseId",
                table: "Ugovors",
                column: "CaseId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cenovniks_AspNetUsers_IdenityId",
                table: "Cenovniks",
                column: "IdenityId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
