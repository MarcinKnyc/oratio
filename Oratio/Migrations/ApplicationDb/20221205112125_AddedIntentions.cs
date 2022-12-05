using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oratio.Migrations.ApplicationDb
{
    public partial class AddedIntentions : Migration
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

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Parishes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Churches",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Addresses",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OratioUser",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsModerator = table.Column<bool>(type: "bit", nullable: false),
                    IsAdministrator = table.Column<bool>(type: "bit", nullable: false),
                    IsFaithful = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OratioUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mass",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChurchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mass", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mass_Churches_ChurchId",
                        column: x => x.ChurchId,
                        principalTable: "Churches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Mass_OratioUser_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "OratioUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Intention",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AskedIntention = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Offering = table.Column<float>(type: "real", nullable: true),
                    isPaid = table.Column<bool>(type: "bit", nullable: false),
                    isApproved = table.Column<bool>(type: "bit", nullable: false),
                    MassId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Intention", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Intention_Mass_MassId",
                        column: x => x.MassId,
                        principalTable: "Mass",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Intention_OratioUser_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "OratioUser",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Parishes",
                columns: new[] { "Id", "Dedicated", "MinimumOffering", "Name", "OwnerId" },
                values: new object[] { new Guid("12322f45-af01-4783-8b22-ebf75d03ddfc"), "Żadna", null, "Fejk", null });

            migrationBuilder.CreateIndex(
                name: "IX_Parishes_OwnerId",
                table: "Parishes",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Churches_OwnerId",
                table: "Churches",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_OwnerId",
                table: "Addresses",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Intention_MassId",
                table: "Intention",
                column: "MassId");

            migrationBuilder.CreateIndex(
                name: "IX_Intention_OwnerId",
                table: "Intention",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Mass_ChurchId",
                table: "Mass",
                column: "ChurchId");

            migrationBuilder.CreateIndex(
                name: "IX_Mass_OwnerId",
                table: "Mass",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_OratioUser_OwnerId",
                table: "Addresses",
                column: "OwnerId",
                principalTable: "OratioUser",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Churches_OratioUser_OwnerId",
                table: "Churches",
                column: "OwnerId",
                principalTable: "OratioUser",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Parishes_OratioUser_OwnerId",
                table: "Parishes",
                column: "OwnerId",
                principalTable: "OratioUser",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_OratioUser_OwnerId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Churches_OratioUser_OwnerId",
                table: "Churches");

            migrationBuilder.DropForeignKey(
                name: "FK_Parishes_OratioUser_OwnerId",
                table: "Parishes");

            migrationBuilder.DropTable(
                name: "Intention");

            migrationBuilder.DropTable(
                name: "Mass");

            migrationBuilder.DropTable(
                name: "OratioUser");

            migrationBuilder.DropIndex(
                name: "IX_Parishes_OwnerId",
                table: "Parishes");

            migrationBuilder.DropIndex(
                name: "IX_Churches_OwnerId",
                table: "Churches");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_OwnerId",
                table: "Addresses");

            migrationBuilder.DeleteData(
                table: "Parishes",
                keyColumn: "Id",
                keyValue: new Guid("12322f45-af01-4783-8b22-ebf75d03ddfc"));

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
