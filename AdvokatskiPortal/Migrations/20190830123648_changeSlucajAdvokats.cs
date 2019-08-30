using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AdvokatskiPortal.Migrations
{
    public partial class changeSlucajAdvokats : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SlucajAdvokats",
                table: "SlucajAdvokats");

            migrationBuilder.DropIndex(
                name: "IX_SlucajAdvokats_SlucajId",
                table: "SlucajAdvokats");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "SlucajAdvokats");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SlucajAdvokats",
                table: "SlucajAdvokats",
                columns: new[] { "SlucajId", "AdvokatId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SlucajAdvokats",
                table: "SlucajAdvokats");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "SlucajAdvokats",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SlucajAdvokats",
                table: "SlucajAdvokats",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_SlucajAdvokats_SlucajId",
                table: "SlucajAdvokats",
                column: "SlucajId");
        }
    }
}
