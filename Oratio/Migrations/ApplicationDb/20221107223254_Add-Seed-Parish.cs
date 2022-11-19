using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oratio.Migrations.ApplicationDb
{
    public partial class AddSeedParish : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Parishes",
                columns: new[] { "Id", "Dedicated", "Name" },
                values: new object[] { new Guid("da776626-be3c-48fd-91c2-ae4042970a46"), "Żadna", "Fejk" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Parishes",
                keyColumn: "Id",
                keyValue: new Guid("da776626-be3c-48fd-91c2-ae4042970a46"));
        }
    }
}
