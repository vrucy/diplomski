using Microsoft.EntityFrameworkCore.Migrations;

namespace AdvokatskiPortal.Migrations
{
    public partial class addUserName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Korisniks",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdenityId",
                table: "Korisniks",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Korisniks",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Korisniks",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IdenityId",
                table: "Advokats",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Advokats",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Advokats",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Korisniks_IdenityId",
                table: "Korisniks",
                column: "IdenityId");

            migrationBuilder.CreateIndex(
                name: "IX_Advokats_IdenityId",
                table: "Advokats",
                column: "IdenityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Advokats_AspNetUsers_IdenityId",
                table: "Advokats",
                column: "IdenityId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Korisniks_AspNetUsers_IdenityId",
                table: "Korisniks",
                column: "IdenityId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advokats_AspNetUsers_IdenityId",
                table: "Advokats");

            migrationBuilder.DropForeignKey(
                name: "FK_Korisniks_AspNetUsers_IdenityId",
                table: "Korisniks");

            migrationBuilder.DropIndex(
                name: "IX_Korisniks_IdenityId",
                table: "Korisniks");

            migrationBuilder.DropIndex(
                name: "IX_Advokats_IdenityId",
                table: "Advokats");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Korisniks");

            migrationBuilder.DropColumn(
                name: "IdenityId",
                table: "Korisniks");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Korisniks");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Korisniks");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IdenityId",
                table: "Advokats");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Advokats");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Advokats");
        }
    }
}
