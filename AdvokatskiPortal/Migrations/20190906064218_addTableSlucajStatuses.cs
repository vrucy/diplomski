using Microsoft.EntityFrameworkCore.Migrations;

namespace AdvokatskiPortal.Migrations
{
    public partial class addTableSlucajStatuses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SlucajAdvokats_SlucajStatus_SlucajStatusId",
                table: "SlucajAdvokats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SlucajStatus",
                table: "SlucajStatus");

            migrationBuilder.RenameTable(
                name: "SlucajStatus",
                newName: "SlucajStatuses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SlucajStatuses",
                table: "SlucajStatuses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SlucajAdvokats_SlucajStatuses_SlucajStatusId",
                table: "SlucajAdvokats",
                column: "SlucajStatusId",
                principalTable: "SlucajStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SlucajAdvokats_SlucajStatuses_SlucajStatusId",
                table: "SlucajAdvokats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SlucajStatuses",
                table: "SlucajStatuses");

            migrationBuilder.RenameTable(
                name: "SlucajStatuses",
                newName: "SlucajStatus");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SlucajStatus",
                table: "SlucajStatus",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SlucajAdvokats_SlucajStatus_SlucajStatusId",
                table: "SlucajAdvokats",
                column: "SlucajStatusId",
                principalTable: "SlucajStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
