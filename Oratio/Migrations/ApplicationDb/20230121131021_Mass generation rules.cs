using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oratio.Migrations.ApplicationDb
{
    public partial class Massgenerationrules : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimesToRepeat",
                table: "MassGenerationRules");

            migrationBuilder.DropColumn(
                name: "TimespanToRepeat",
                table: "MassGenerationRules");

            migrationBuilder.DropColumn(
                name: "WeekNumber",
                table: "MassGenerationRules");

            migrationBuilder.AddColumn<DateTime>(
                name: "Time",
                table: "MassGenerationRules",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Time",
                table: "MassGenerationRules");

            migrationBuilder.AddColumn<int>(
                name: "TimesToRepeat",
                table: "MassGenerationRules",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TimespanToRepeat",
                table: "MassGenerationRules",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WeekNumber",
                table: "MassGenerationRules",
                type: "int",
                nullable: true);
        }
    }
}
