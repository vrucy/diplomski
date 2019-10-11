using Microsoft.EntityFrameworkCore.Migrations;

namespace AdvokatskiPortal.Migrations
{
    public partial class fixSlucajMajstor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MajstorIdStr",
                table: "SlucajMajstors",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MajstorIdStr",
                table: "SlucajMajstors");
        }
    }
}
