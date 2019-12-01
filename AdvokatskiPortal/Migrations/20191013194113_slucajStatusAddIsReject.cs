using Microsoft.EntityFrameworkCore.Migrations;

namespace MajstorskiPortal.Migrations
{
    public partial class slucajStatusAddIsReject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isReject",
                table: "SlucajMajstors",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isReject",
                table: "SlucajMajstors");
        }
    }
}
