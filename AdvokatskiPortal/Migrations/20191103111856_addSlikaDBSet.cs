using Microsoft.EntityFrameworkCore.Migrations;

namespace AdvokatskiPortal.Migrations
{
    public partial class addSlikaDBSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Slika_Slucajs_SlucajId",
                table: "Slika");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Slika",
                table: "Slika");

            migrationBuilder.RenameTable(
                name: "Slika",
                newName: "Slikas");

            migrationBuilder.RenameIndex(
                name: "IX_Slika_SlucajId",
                table: "Slikas",
                newName: "IX_Slikas_SlucajId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Slikas",
                table: "Slikas",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Slikas_Slucajs_SlucajId",
                table: "Slikas",
                column: "SlucajId",
                principalTable: "Slucajs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Slikas_Slucajs_SlucajId",
                table: "Slikas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Slikas",
                table: "Slikas");

            migrationBuilder.RenameTable(
                name: "Slikas",
                newName: "Slika");

            migrationBuilder.RenameIndex(
                name: "IX_Slikas_SlucajId",
                table: "Slika",
                newName: "IX_Slika_SlucajId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Slika",
                table: "Slika",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Slika_Slucajs_SlucajId",
                table: "Slika",
                column: "SlucajId",
                principalTable: "Slucajs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
