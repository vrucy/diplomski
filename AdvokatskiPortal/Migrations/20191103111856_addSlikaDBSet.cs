﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace CraftmanPortal.Migrations
{
    public partial class addSlikaDBSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Slika_Cases_CaseId",
                table: "Slika");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Slika",
                table: "Slika");

            migrationBuilder.RenameTable(
                name: "Slika",
                newName: "Slikas");

            migrationBuilder.RenameIndex(
                name: "IX_Slika_CaseId",
                table: "Slikas",
                newName: "IX_Slikas_CaseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Slikas",
                table: "Slikas",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Slikas_Cases_CaseId",
                table: "Slikas",
                column: "CaseId",
                principalTable: "Cases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Slikas_Cases_CaseId",
                table: "Slikas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Slikas",
                table: "Slikas");

            migrationBuilder.RenameTable(
                name: "Slikas",
                newName: "Slika");

            migrationBuilder.RenameIndex(
                name: "IX_Slikas_CaseId",
                table: "Slika",
                newName: "IX_Slika_CaseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Slika",
                table: "Slika",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Slika_Cases_CaseId",
                table: "Slika",
                column: "CaseId",
                principalTable: "Cases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
