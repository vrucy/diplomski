using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MajstorskiPortal.Migrations
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
                table: "SlucajMajstors");

            migrationBuilder.DropColumn(
                name: "isReadOdbijenMajstor",
                table: "SlucajMajstors");

            migrationBuilder.DropColumn(
                name: "isReadOdbijenKorisnik",
                table: "SlucajMajstors");

            migrationBuilder.DropColumn(
                name: "prihvacno",
                table: "SlucajMajstors");

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
                table: "SlucajMajstors",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isReadOdbijenMajstor",
                table: "SlucajMajstors",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isReadOdbijenKorisnik",
                table: "SlucajMajstors",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "prihvacno",
                table: "SlucajMajstors",
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

            migrationBuilder.CreateIndex(
                name: "IX_Cenovniks_IdenityId",
                table: "Cenovniks",
                column: "IdenityId");

            migrationBuilder.CreateIndex(
                name: "IX_Ugovors_SlucajId",
                table: "Ugovors",
                column: "SlucajId",
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
