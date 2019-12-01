using Microsoft.EntityFrameworkCore.Migrations;

namespace MajstorskiPortal.Migrations
{
    public partial class fixrelationcenovnikslucaj : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Cenovniks_SlucajId",
                table: "Cenovniks");

            migrationBuilder.DropColumn(
                name: "CenovnikId",
                table: "Slucajs");

            migrationBuilder.CreateIndex(
                name: "IX_Cenovniks_SlucajId",
                table: "Cenovniks",
                column: "SlucajId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Cenovniks_SlucajId",
                table: "Cenovniks");

            migrationBuilder.AddColumn<int>(
                name: "CenovnikId",
                table: "Slucajs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Cenovniks_SlucajId",
                table: "Cenovniks",
                column: "SlucajId",
                unique: true);
        }
    }
}
