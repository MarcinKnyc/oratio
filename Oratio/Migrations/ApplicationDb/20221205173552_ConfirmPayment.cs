using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oratio.Migrations.ApplicationDb
{
    public partial class ConfirmPayment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Parishes",
                keyColumn: "Id",
                keyValue: new Guid("77ce69ce-c09b-4837-9f2f-6143275f3e23"));

            migrationBuilder.InsertData(
                table: "Parishes",
                columns: new[] { "Id", "Dedicated", "MinimumOffering", "Name", "OwnerId" },
                values: new object[] { new Guid("f96c18c7-4e2b-4f73-96a4-e6980598b753"), "Żadna", null, "Fejk", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Parishes",
                keyColumn: "Id",
                keyValue: new Guid("f96c18c7-4e2b-4f73-96a4-e6980598b753"));

            migrationBuilder.InsertData(
                table: "Parishes",
                columns: new[] { "Id", "Dedicated", "MinimumOffering", "Name", "OwnerId" },
                values: new object[] { new Guid("77ce69ce-c09b-4837-9f2f-6143275f3e23"), "Żadna", null, "Fejk", null });
        }
    }
}
