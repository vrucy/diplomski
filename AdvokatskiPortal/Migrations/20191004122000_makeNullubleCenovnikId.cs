using Microsoft.EntityFrameworkCore.Migrations;

namespace MajstorskiPortal.Migrations
{
    public partial class makeNullubleCenovnikId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Slucajs_Cenovniks_CenovnikId",
                table: "Slucajs");

            migrationBuilder.DropIndex(
                name: "IX_Slucajs_CenovnikId",
                table: "Slucajs");

            migrationBuilder.AlterColumn<int>(
                name: "CenovnikId",
                table: "Slucajs",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Slucajs_CenovnikId",
                table: "Slucajs",
                column: "CenovnikId",
                unique: true,
                filter: "[CenovnikId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Slucajs_Cenovniks_CenovnikId",
                table: "Slucajs",
                column: "CenovnikId",
                principalTable: "Cenovniks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Slucajs_Cenovniks_CenovnikId",
                table: "Slucajs");

            migrationBuilder.DropIndex(
                name: "IX_Slucajs_CenovnikId",
                table: "Slucajs");

            migrationBuilder.AlterColumn<int>(
                name: "CenovnikId",
                table: "Slucajs",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

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
    }
}
