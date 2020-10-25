using Microsoft.EntityFrameworkCore.Migrations;

namespace CraftmanPortal.Migrations
{
    public partial class addCaseCraftmanIsReadUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isReadOdbijenCraftman",
                table: "CaseCraftmans",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isReadOdbijenUser",
                table: "CaseCraftmans",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isReadOdbijenCraftman",
                table: "CaseCraftmans");

            migrationBuilder.DropColumn(
                name: "isReadOdbijenUser",
                table: "CaseCraftmans");
        }
    }
}
