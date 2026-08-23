using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gb_prod_api.Migrations
{
    /// <inheritdoc />
    public partial class AddArabicNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ArabicName",
                table: "Tunnels",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ArabicName",
                table: "PawGrades",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ArabicName",
                table: "PawColors",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArabicName",
                table: "Tunnels");

            migrationBuilder.DropColumn(
                name: "ArabicName",
                table: "PawGrades");

            migrationBuilder.DropColumn(
                name: "ArabicName",
                table: "PawColors");
        }
    }
}
