using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oratio.Migrations.ApplicationDb
{
    public partial class fixForeignKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Churches_ChurchId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Churches_Parishes_ParishId",
                table: "Churches");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_ChurchId",
                table: "Addresses");

            migrationBuilder.DeleteData(
                table: "Parishes",
                keyColumn: "Id",
                keyValue: new Guid("8d4b5617-57fb-4275-b20e-ce9747f6073f"));

            migrationBuilder.AlterColumn<Guid>(
                name: "ParishId",
                table: "Churches",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "ChurchId",
                table: "Addresses",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_ChurchId",
                table: "Addresses",
                column: "ChurchId",
                unique: true,
                filter: "[ChurchId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Churches_ChurchId",
                table: "Addresses",
                column: "ChurchId",
                principalTable: "Churches",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Churches_Parishes_ParishId",
                table: "Churches",
                column: "ParishId",
                principalTable: "Parishes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Churches_ChurchId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Churches_Parishes_ParishId",
                table: "Churches");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_ChurchId",
                table: "Addresses");

            migrationBuilder.AlterColumn<Guid>(
                name: "ParishId",
                table: "Churches",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ChurchId",
                table: "Addresses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Parishes",
                columns: new[] { "Id", "Dedicated", "MinimumOffering", "Name", "OwnerId" },
                values: new object[] { new Guid("8d4b5617-57fb-4275-b20e-ce9747f6073f"), "Żadna", null, "Fejk", null });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_ChurchId",
                table: "Addresses",
                column: "ChurchId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Churches_ChurchId",
                table: "Addresses",
                column: "ChurchId",
                principalTable: "Churches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Churches_Parishes_ParishId",
                table: "Churches",
                column: "ParishId",
                principalTable: "Parishes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
