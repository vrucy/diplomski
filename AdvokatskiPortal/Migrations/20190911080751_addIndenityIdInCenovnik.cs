using Microsoft.EntityFrameworkCore.Migrations;

namespace AdvokatskiPortal.Migrations
{
    public partial class addIndenityIdInCenovnik : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdenityId",
                table: "Cenovniks",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cenovniks_IdenityId",
                table: "Cenovniks",
                column: "IdenityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cenovniks_AspNetUsers_IdenityId",
                table: "Cenovniks",
                column: "IdenityId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cenovniks_AspNetUsers_IdenityId",
                table: "Cenovniks");

            migrationBuilder.DropIndex(
                name: "IX_Cenovniks_IdenityId",
                table: "Cenovniks");

            migrationBuilder.DropColumn(
                name: "IdenityId",
                table: "Cenovniks");
        }
    }
}
