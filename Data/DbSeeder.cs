using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gb_prod_api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace gb_prod_api.Data
{
    public static class DbSeeder
    {
        // Fixed seed so repeated runs produce identical data.
        private const int RandomSeed = 20260821;

        public static async Task SeedAsync(AppDbContext dbContext)
        {
            await ClearAsync(dbContext);

            var tunnels = await SeedTunnelsAsync(dbContext);
            var pawGrades = await SeedPawColorsAndGradesAsync(dbContext);
            await SeedProductionAsync(dbContext, tunnels, pawGrades);
            await SeedContainerShipmentsAsync(dbContext, pawGrades);
            await SeedStockAdjustmentsAsync(dbContext, pawGrades);
            await SeedUsersAsync(dbContext);
        }

        /// <summary>
        /// Wipes every table and resets identity sequences so seeded ids start at 1.
        /// CASCADE is safe here because the full graph is being rebuilt.
        /// </summary>
        private static async Task ClearAsync(AppDbContext dbContext)
        {
            await dbContext.Database.ExecuteSqlRawAsync(
                """
                TRUNCATE TABLE
                    "UserPermissions",
                    "Users",
                    "StockAdjustments",
                    "ContainerShipmentItems",
                    "ContainerShipments",
                    "ProductionRecords",
                    "ProductionDays",
                    "PawGrades",
                    "PawColors",
                    "Tunnels"
                RESTART IDENTITY CASCADE;
                """);
        }

        private static async Task SeedUsersAsync(AppDbContext dbContext)
        {
            var passwordHasher = new PasswordHasher<User>();

            var admin = new User
            {
                Username = "admin",
                Role = UserRole.Admin,
            };
            admin.PasswordHash = passwordHasher.HashPassword(admin, "admin123");

            var user = new User
            {
                Username = "user",
                Role = UserRole.User,
                UserPermissions =
                [
                    new() { Permission = Permission.ViewProduction },
                    new() { Permission = Permission.ViewTunnels },
                ],
            };
            user.PasswordHash = passwordHasher.HashPassword(user, "user123");

            dbContext.Users.AddRange(admin, user);
            await dbContext.SaveChangesAsync();
        }

        private static async Task<List<Tunnel>> SeedTunnelsAsync(AppDbContext dbContext)
        {
            var tunnels = new List<Tunnel>
            {
                new() { Name = "Tunnel 1", ArabicName = "نفق 1", Capacity = 2500 },
                new() { Name = "Tunnel 2", ArabicName = "نفق 2", Capacity = 2000 },
                new() { Name = "Tunnel 3", ArabicName = "نفق 3", Capacity = 2000 },
                new() { Name = "Tunnel 4", ArabicName = "نفق 4", Capacity = 2500 },
                new() { Name = "Tunnel 5", ArabicName = "نفق 5", Capacity = 2500 },
            };

            dbContext.Tunnels.AddRange(tunnels);
            await dbContext.SaveChangesAsync();

            return tunnels;
        }

        private static async Task<List<PawGrade>> SeedPawColorsAndGradesAsync(AppDbContext dbContext)
        {
            var colors = new List<PawColor>
            {
                new() { Name = "White", ArabicName = "أبيض" },
                new() { Name = "Yellow", ArabicName = "أصفر" },
                new() { Name = "Black", ArabicName = "أسود" },
            };

            dbContext.PawColors.AddRange(colors);
            await dbContext.SaveChangesAsync();

            var gradeTiers = new[]
            {
                (Name: "Grade A", ArabicName: "درجة أولى", Description: "Premium export quality"),
                (Name: "Grade B", ArabicName: "درجة ثانية", Description: "Standard export quality"),
                (Name: "Grade C", ArabicName: "درجة ثالثة", Description: "Local market quality"),
            };

            var grades = colors
                .SelectMany(color => gradeTiers.Select(tier => new PawGrade
                {
                    PawColorId = color.Id,
                    Name = $"{color.Name} {tier.Name}",
                    ArabicName = $"{color.ArabicName} {tier.ArabicName}",
                    Description = $"{tier.Description} — {color.Name.ToLowerInvariant()} paws",
                }))
                .ToList();

            dbContext.PawGrades.AddRange(grades);
            await dbContext.SaveChangesAsync();

            return grades;
        }

        private static async Task SeedProductionAsync(
            AppDbContext dbContext,
            List<Tunnel> tunnels,
            List<PawGrade> pawGrades)
        {
            var random = new Random(RandomSeed);
            var today = DateOnly.FromDateTime(DateTime.UtcNow);

            var productionDays = Enumerable.Range(0, 7)
                .Select(offset => new ProductionDay { Date = today.AddDays(-offset) })
                .ToList();

            dbContext.ProductionDays.AddRange(productionDays);
            await dbContext.SaveChangesAsync();

            var records = new List<ProductionRecord>();

            foreach (var productionDay in productionDays)
            {
                var isClosedOut = productionDay.Date < today.AddDays(-2);

                for (var slot = 0; slot < 6; slot++)
                {
                    var grade = pawGrades[random.Next(pawGrades.Count)];
                    var tunnel = tunnels[random.Next(tunnels.Count)];
                    var producedAt = productionDay.Date.ToDateTime(
                        new TimeOnly(7 + slot * 2, 0),
                        DateTimeKind.Utc);

                    records.Add(new ProductionRecord
                    {
                        ProductionDayId = productionDay.Id,
                        PawGradeId = grade.Id,
                        TunnelId = tunnel.Id,
                        ProducedAt = producedAt,
                        QuantityKg = Math.Round((decimal)(random.NextDouble() * 400 + 150), 2),
                        // Older batches have already left the tunnel; recent ones are still inside,
                        // so "current occupancy" queries return something meaningful.
                        MovedOutAt = isClosedOut ? producedAt.AddHours(36) : null,
                        Status = random.Next(10) == 0
                            ? ProductionRecordStatus.Rejected
                            : ProductionRecordStatus.Accepted,
                        Notes = slot == 0 ? "Morning shift opening batch" : null,
                    });
                }
            }

            dbContext.ProductionRecords.AddRange(records);
            await dbContext.SaveChangesAsync();
        }

        private static async Task SeedContainerShipmentsAsync(
            AppDbContext dbContext,
            List<PawGrade> pawGrades)
        {
            var random = new Random(RandomSeed);
            var now = DateTime.UtcNow;

            var shipments = new List<ContainerShipment>
            {
                new()
                {
                    ContainerNumber = "MSKU-4821907",
                    ShippedAt = now.AddDays(-12),
                    Notes = "Reefer container, -18°C",
                },
                new()
                {
                    ContainerNumber = "TGHU-7739155",
                    ShippedAt = now.AddDays(-4),
                    Notes = null,
                },
            };

            dbContext.ContainerShipments.AddRange(shipments);
            await dbContext.SaveChangesAsync();

            // The (PawGradeId, ContainerShipmentId) unique index means a grade
            // can appear at most once per shipment.
            var items = shipments
                .SelectMany(shipment => pawGrades
                    .OrderBy(_ => random.Next())
                    .Take(4)
                    .Select(grade => new ContainerShipmentItem
                    {
                        ContainerShipmentId = shipment.Id,
                        PawGradeId = grade.Id,
                        QuantityKg = Math.Round((decimal)(random.NextDouble() * 1500 + 500), 2),
                    }))
                .ToList();

            dbContext.ContainerShipmentItems.AddRange(items);
            await dbContext.SaveChangesAsync();
        }

        private static async Task SeedStockAdjustmentsAsync(
            AppDbContext dbContext,
            List<PawGrade> pawGrades)
        {
            var random = new Random(RandomSeed);
            var now = DateTime.UtcNow;

            var reasons = new[]
            {
                "Inventory recount",
                "Damaged during storage",
                "Quality control sample",
                "Correction of mis-keyed entry",
            };

            var adjustments = reasons
                .Select((reason, index) => new StockAdjustment
                {
                    PawGradeId = pawGrades[random.Next(pawGrades.Count)].Id,
                    QuantityKg = Math.Round((decimal)(random.NextDouble() * 80 - 40), 2),
                    Reason = reason,
                    CreatedAt = now.AddDays(-(index * 3 + 1)),
                })
                .ToList();

            dbContext.StockAdjustments.AddRange(adjustments);
            await dbContext.SaveChangesAsync();
        }
    }
}
