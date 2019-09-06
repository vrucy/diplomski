using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AdvokatskiPortal.Migrations
{
    public partial class addTableSlucajStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SlucajStatusId",
                table: "SlucajAdvokats",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "SlucajStatus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SlucajStatus", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SlucajAdvokats_SlucajStatusId",
                table: "SlucajAdvokats",
                column: "SlucajStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_SlucajAdvokats_SlucajStatus_SlucajStatusId",
                table: "SlucajAdvokats",
                column: "SlucajStatusId",
                principalTable: "SlucajStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SlucajAdvokats_SlucajStatus_SlucajStatusId",
                table: "SlucajAdvokats");

            migrationBuilder.DropTable(
                name: "SlucajStatus");

            migrationBuilder.DropIndex(
                name: "IX_SlucajAdvokats_SlucajStatusId",
                table: "SlucajAdvokats");

            migrationBuilder.DropColumn(
                name: "SlucajStatusId",
                table: "SlucajAdvokats");
        }
    }
}
