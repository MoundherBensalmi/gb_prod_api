using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gb_prod_api.Migrations
{
    /// <inheritdoc />
    public partial class AddIsClosedToProductionDay : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsClosed",
                table: "ProductionDays",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsClosed",
                table: "ProductionDays");
        }
    }
}
