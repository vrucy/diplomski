using Microsoft.EntityFrameworkCore.Migrations;

namespace CraftmanPortal.Migrations
{
    public partial class relationCaseNotification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CaseId",
                table: "Notifications",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_CaseId",
                table: "Notifications",
                column: "CaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Cases_CaseId",
                table: "Notifications",
                column: "CaseId",
                principalTable: "Cases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Cases_CaseId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_CaseId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "CaseId",
                table: "Notifications");
        }
    }
}
