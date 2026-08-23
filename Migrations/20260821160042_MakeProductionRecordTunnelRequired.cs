using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gb_prod_api.Migrations
{
    /// <inheritdoc />
    public partial class MakeProductionRecordTunnelRequired : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductionRecords_Tunnels_TunnelId",
                table: "ProductionRecords");

            migrationBuilder.AlterColumn<int>(
                name: "TunnelId",
                table: "ProductionRecords",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductionRecords_Tunnels_TunnelId",
                table: "ProductionRecords",
                column: "TunnelId",
                principalTable: "Tunnels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductionRecords_Tunnels_TunnelId",
                table: "ProductionRecords");

            migrationBuilder.AlterColumn<int>(
                name: "TunnelId",
                table: "ProductionRecords",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductionRecords_Tunnels_TunnelId",
                table: "ProductionRecords",
                column: "TunnelId",
                principalTable: "Tunnels",
                principalColumn: "Id");
        }
    }
}
