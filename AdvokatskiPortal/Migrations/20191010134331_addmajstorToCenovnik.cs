using Microsoft.EntityFrameworkCore.Migrations;

namespace CraftmanPortal.Migrations
{
    public partial class addCraftmanToCenovnik : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CraftmanId",
                table: "Cenovniks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Cenovniks_CraftmanId",
                table: "Cenovniks",
                column: "CraftmanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cenovniks_Craftmans_CraftmanId",
                table: "Cenovniks",
                column: "CraftmanId",
                principalTable: "Craftmans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cenovniks_Craftmans_CraftmanId",
                table: "Cenovniks");

            migrationBuilder.DropIndex(
                name: "IX_Cenovniks_CraftmanId",
                table: "Cenovniks");

            migrationBuilder.DropColumn(
                name: "CraftmanId",
                table: "Cenovniks");
        }
    }
}
