using Microsoft.EntityFrameworkCore.Migrations;

namespace AdvokatskiPortal.Migrations
{
    public partial class addFieldInSlucajtitle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isActive",
                table: "SlucajAdvokats");

            migrationBuilder.AddColumn<string>(
                name: "Naziv",
                table: "Slucajs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Naziv",
                table: "Slucajs");

            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "SlucajAdvokats",
                nullable: false,
                defaultValue: false);
        }
    }
}
