using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace gb_prod_api.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContainerShipments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ShippedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ContainerNumber = table.Column<string>(type: "text", nullable: false),
                    Notes = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContainerShipments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PawColors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PawColors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductionDays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Date = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionDays", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tunnels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tunnels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PawGrades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PawColorId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PawGrades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PawGrades_PawColors_PawColorId",
                        column: x => x.PawColorId,
                        principalTable: "PawColors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContainerShipmentItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ContainerShipmentId = table.Column<long>(type: "bigint", nullable: false),
                    PawGradeId = table.Column<int>(type: "integer", nullable: false),
                    QuantityKg = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContainerShipmentItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContainerShipmentItems_ContainerShipments_ContainerShipment~",
                        column: x => x.ContainerShipmentId,
                        principalTable: "ContainerShipments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContainerShipmentItems_PawGrades_PawGradeId",
                        column: x => x.PawGradeId,
                        principalTable: "PawGrades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductionRecords",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductionDayId = table.Column<int>(type: "integer", nullable: false),
                    PawGradeId = table.Column<int>(type: "integer", nullable: false),
                    TunnelId = table.Column<int>(type: "integer", nullable: true),
                    ProducedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    QuantityKg = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    MovedOutAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Notes = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductionRecords_PawGrades_PawGradeId",
                        column: x => x.PawGradeId,
                        principalTable: "PawGrades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductionRecords_ProductionDays_ProductionDayId",
                        column: x => x.ProductionDayId,
                        principalTable: "ProductionDays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductionRecords_Tunnels_TunnelId",
                        column: x => x.TunnelId,
                        principalTable: "Tunnels",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StockAdjustments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PawGradeId = table.Column<int>(type: "integer", nullable: false),
                    QuantityKg = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Reason = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockAdjustments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockAdjustments_PawGrades_PawGradeId",
                        column: x => x.PawGradeId,
                        principalTable: "PawGrades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContainerShipmentItems_ContainerShipmentId",
                table: "ContainerShipmentItems",
                column: "ContainerShipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ContainerShipmentItems_PawGradeId_ContainerShipmentId",
                table: "ContainerShipmentItems",
                columns: new[] { "PawGradeId", "ContainerShipmentId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PawGrades_PawColorId",
                table: "PawGrades",
                column: "PawColorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionDays_Date",
                table: "ProductionDays",
                column: "Date",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductionRecords_PawGradeId_MovedOutAt",
                table: "ProductionRecords",
                columns: new[] { "PawGradeId", "MovedOutAt" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductionRecords_ProductionDayId",
                table: "ProductionRecords",
                column: "ProductionDayId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionRecords_TunnelId",
                table: "ProductionRecords",
                column: "TunnelId");

            migrationBuilder.CreateIndex(
                name: "IX_StockAdjustments_PawGradeId_CreatedAt",
                table: "StockAdjustments",
                columns: new[] { "PawGradeId", "CreatedAt" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContainerShipmentItems");

            migrationBuilder.DropTable(
                name: "ProductionRecords");

            migrationBuilder.DropTable(
                name: "StockAdjustments");

            migrationBuilder.DropTable(
                name: "ContainerShipments");

            migrationBuilder.DropTable(
                name: "ProductionDays");

            migrationBuilder.DropTable(
                name: "Tunnels");

            migrationBuilder.DropTable(
                name: "PawGrades");

            migrationBuilder.DropTable(
                name: "PawColors");
        }
    }
}
