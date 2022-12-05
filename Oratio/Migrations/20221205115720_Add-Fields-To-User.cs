using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oratio.Migrations
{
    public partial class AddFieldsToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsAdministrator",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFaithful",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsModerator",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsAdministrator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsFaithful",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsModerator",
                table: "AspNetUsers");
        }
    }
}
