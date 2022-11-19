using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oratio.Migrations.ApplicationDb
{
    public partial class AddedChurchesParishesAddresses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Parishes",
                keyColumn: "Id",
                keyValue: new Guid("da776626-be3c-48fd-91c2-ae4042970a46"));

            migrationBuilder.InsertData(
                table: "Parishes",
                columns: new[] { "Id", "Dedicated", "Name" },
                values: new object[] { new Guid("0d49723c-13a4-4b43-ad29-e4a055169742"), "Żadna", "Fejk" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Parishes",
                keyColumn: "Id",
                keyValue: new Guid("0d49723c-13a4-4b43-ad29-e4a055169742"));

            migrationBuilder.InsertData(
                table: "Parishes",
                columns: new[] { "Id", "Dedicated", "Name" },
                values: new object[] { new Guid("da776626-be3c-48fd-91c2-ae4042970a46"), "Żadna", "Fejk" });
        }
    }
}
