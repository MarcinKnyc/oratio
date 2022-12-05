using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oratio.Migrations.ApplicationDb
{
    public partial class AddOwners : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Parishes",
                keyColumn: "Id",
                keyValue: new Guid("0d49723c-13a4-4b43-ad29-e4a055169742"));

            migrationBuilder.AddColumn<float>(
                name: "MinimumOffering",
                table: "Parishes",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "Parishes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "Churches",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "Addresses",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Mass",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChurchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mass", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mass_Churches_ChurchId",
                        column: x => x.ChurchId,
                        principalTable: "Churches",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MassGenerationRules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParishId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TimesToRepeat = table.Column<int>(type: "int", nullable: true),
                    TimespanToRepeat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RuleTerminationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DayOfWeek = table.Column<int>(type: "int", nullable: true),
                    WeekNumber = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    RuleStartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MassGenerationRules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MassGenerationRules_Parishes_ParishId",
                        column: x => x.ParishId,
                        principalTable: "Parishes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Intentions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AskedIntention = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Offering = table.Column<float>(type: "real", nullable: true),
                    isPaid = table.Column<bool>(type: "bit", nullable: false),
                    MassId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Intentions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Intentions_Mass_MassId",
                        column: x => x.MassId,
                        principalTable: "Mass",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Parishes",
                columns: new[] { "Id", "Dedicated", "MinimumOffering", "Name", "OwnerId" },
                values: new object[] { new Guid("264ac181-b7dd-4cd6-8e30-51a06c496059"), "Żadna", null, "Fejk", null });

            migrationBuilder.CreateIndex(
                name: "IX_Intentions_MassId",
                table: "Intentions",
                column: "MassId");

            migrationBuilder.CreateIndex(
                name: "IX_Mass_ChurchId",
                table: "Mass",
                column: "ChurchId");

            migrationBuilder.CreateIndex(
                name: "IX_MassGenerationRules_ParishId",
                table: "MassGenerationRules",
                column: "ParishId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Intentions");

            migrationBuilder.DropTable(
                name: "MassGenerationRules");

            migrationBuilder.DropTable(
                name: "Mass");

            migrationBuilder.DeleteData(
                table: "Parishes",
                keyColumn: "Id",
                keyValue: new Guid("264ac181-b7dd-4cd6-8e30-51a06c496059"));

            migrationBuilder.DropColumn(
                name: "MinimumOffering",
                table: "Parishes");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Parishes");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Churches");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Addresses");

            migrationBuilder.InsertData(
                table: "Parishes",
                columns: new[] { "Id", "Dedicated", "Name" },
                values: new object[] { new Guid("0d49723c-13a4-4b43-ad29-e4a055169742"), "Żadna", "Fejk" });
        }
    }
}
