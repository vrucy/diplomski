using Microsoft.EntityFrameworkCore.Migrations;

namespace MajstorskiPortal.Migrations
{
    public partial class relationSlucajNotification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SlucajId",
                table: "Notifications",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_SlucajId",
                table: "Notifications",
                column: "SlucajId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Slucajs_SlucajId",
                table: "Notifications",
                column: "SlucajId",
                principalTable: "Slucajs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Slucajs_SlucajId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_SlucajId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "SlucajId",
                table: "Notifications");
        }
    }
}
