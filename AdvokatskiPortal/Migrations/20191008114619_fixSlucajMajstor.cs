using Microsoft.EntityFrameworkCore.Migrations;

namespace ContractorskiPortal.Migrations
{
    public partial class fixCaseContractor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContractorIdStr",
                table: "CaseContractors",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContractorIdStr",
                table: "CaseContractors");
        }
    }
}
