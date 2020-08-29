using Microsoft.EntityFrameworkCore.Migrations;

namespace TeduCoreApp.Data.EF.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Lat",
                table: "ContactDetails");

            migrationBuilder.DropColumn(
                name: "Lng",
                table: "ContactDetails");

            migrationBuilder.DropColumn(
                name: "Other",
                table: "ContactDetails");

            migrationBuilder.DropColumn(
                name: "Website",
                table: "ContactDetails");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "ContactDetails",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "ContactDetails",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 250,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "ContactDetails",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "ContactDetails",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 250);

            migrationBuilder.AddColumn<double>(
                name: "Lat",
                table: "ContactDetails",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Lng",
                table: "ContactDetails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Other",
                table: "ContactDetails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Website",
                table: "ContactDetails",
                maxLength: 250,
                nullable: true);
        }
    }
}
