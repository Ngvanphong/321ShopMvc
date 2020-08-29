using Microsoft.EntityFrameworkCore.Migrations;

namespace TeduCoreApp.Data.EF.Migrations
{
    public partial class init3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Bank1",
                table: "ContactDetails",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Bank2",
                table: "ContactDetails",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Bank3",
                table: "ContactDetails",
                maxLength: 250,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bank1",
                table: "ContactDetails");

            migrationBuilder.DropColumn(
                name: "Bank2",
                table: "ContactDetails");

            migrationBuilder.DropColumn(
                name: "Bank3",
                table: "ContactDetails");
        }
    }
}
