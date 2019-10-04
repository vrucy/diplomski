using Microsoft.EntityFrameworkCore.Migrations;

namespace AdvokatskiPortal.Migrations
{
    public partial class changeRelationSlucajCenovnik : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cenovniks_Slucajs_SlucajId",
                table: "Cenovniks");

            migrationBuilder.DropIndex(
                name: "IX_Cenovniks_SlucajId",
                table: "Cenovniks");

            migrationBuilder.AddColumn<int>(
                name: "CenovnikId",
                table: "Slucajs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Slucajs_CenovnikId",
                table: "Slucajs",
                column: "CenovnikId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Slucajs_Cenovniks_CenovnikId",
                table: "Slucajs",
                column: "CenovnikId",
                principalTable: "Cenovniks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Slucajs_Cenovniks_CenovnikId",
                table: "Slucajs");

            migrationBuilder.DropIndex(
                name: "IX_Slucajs_CenovnikId",
                table: "Slucajs");

            migrationBuilder.DropColumn(
                name: "CenovnikId",
                table: "Slucajs");

            migrationBuilder.CreateIndex(
                name: "IX_Cenovniks_SlucajId",
                table: "Cenovniks",
                column: "SlucajId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cenovniks_Slucajs_SlucajId",
                table: "Cenovniks",
                column: "SlucajId",
                principalTable: "Slucajs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
