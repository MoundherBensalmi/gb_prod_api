using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gb_prod_api.Migrations
{
    /// <inheritdoc />
    public partial class AddTunelCapacity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Capacity",
                table: "Tunnels",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Capacity",
                table: "Tunnels");
        }
    }
}
