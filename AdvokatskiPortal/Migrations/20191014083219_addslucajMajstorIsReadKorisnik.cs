using Microsoft.EntityFrameworkCore.Migrations;

namespace ContractorskiPortal.Migrations
{
    public partial class addCaseContractorIsReadUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isReadOdbijenContractor",
                table: "CaseContractors",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isReadOdbijenUser",
                table: "CaseContractors",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isReadOdbijenContractor",
                table: "CaseContractors");

            migrationBuilder.DropColumn(
                name: "isReadOdbijenUser",
                table: "CaseContractors");
        }
    }
}
