using Microsoft.EntityFrameworkCore.Migrations;

namespace AdvokatskiPortal.Migrations
{
    public partial class addmajstorToCenovnik : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MajstorId",
                table: "Cenovniks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Cenovniks_MajstorId",
                table: "Cenovniks",
                column: "MajstorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cenovniks_Majstors_MajstorId",
                table: "Cenovniks",
                column: "MajstorId",
                principalTable: "Majstors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cenovniks_Majstors_MajstorId",
                table: "Cenovniks");

            migrationBuilder.DropIndex(
                name: "IX_Cenovniks_MajstorId",
                table: "Cenovniks");

            migrationBuilder.DropColumn(
                name: "MajstorId",
                table: "Cenovniks");
        }
    }
}
