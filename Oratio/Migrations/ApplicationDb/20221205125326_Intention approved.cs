using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oratio.Migrations.ApplicationDb
{
    public partial class Intentionapproved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Parishes",
                keyColumn: "Id",
                keyValue: new Guid("264ac181-b7dd-4cd6-8e30-51a06c496059"));

            migrationBuilder.AddColumn<bool>(
                name: "isApproved",
                table: "Intentions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "Parishes",
                columns: new[] { "Id", "Dedicated", "MinimumOffering", "Name", "OwnerId" },
                values: new object[] { new Guid("8d4b5617-57fb-4275-b20e-ce9747f6073f"), "Żadna", null, "Fejk", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Parishes",
                keyColumn: "Id",
                keyValue: new Guid("8d4b5617-57fb-4275-b20e-ce9747f6073f"));

            migrationBuilder.DropColumn(
                name: "isApproved",
                table: "Intentions");

            migrationBuilder.InsertData(
                table: "Parishes",
                columns: new[] { "Id", "Dedicated", "MinimumOffering", "Name", "OwnerId" },
                values: new object[] { new Guid("264ac181-b7dd-4cd6-8e30-51a06c496059"), "Żadna", null, "Fejk", null });
        }
    }
}
