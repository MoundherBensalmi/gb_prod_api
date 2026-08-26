using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;
using gb_prod_api.Common;
using gb_prod_api.Data;
using gb_prod_api.DTOs.ProductionRecord;
using gb_prod_api.Models;

namespace gb_prod_api.Services
{
    public class ProductionRecordService(AppDbContext dbContext, ProductionDayService productionDayService)
    {
        private readonly AppDbContext _dbContext = dbContext;
        private readonly ProductionDayService _productionDayService = productionDayService;

        public async Task<Result<ProductionRecord>> CreateProductionRecordAsync(CreateProductionRecordRequest request)
        {
            var productionDay = await _productionDayService.GetProductionDayByIdAsync(request.ProductionDayId);
            if (productionDay == null)
            {
                return AppError.Validation(message: "production_day.invalide", field: nameof(request.ProductionDayId));
            }

            var isSameDate = productionDay.Date == DateOnly.FromDateTime(request.ProducedAt.Date);

            if (!isSameDate)
            {
                return AppError.Validation(message: "productionRecord.producedAt.notSameDate", field: nameof(request.ProducedAt));
            }

            var productionRecord = new ProductionRecord
            {
                ProductionDayId = request.ProductionDayId,
                PawGradeId = request.PawGradeId,
                TunnelId = request.TunnelId,
                ProducedAt = request.ProducedAt,
                QuantityKg = request.QuantityKg,
                MovedOutAt = request.MovedOutAt,
                Status = ProductionRecordStatus.Accepted,
                Notes = request.Notes
            };

            _dbContext.ProductionRecords.Add(productionRecord);
            await _dbContext.SaveChangesAsync();

            return Result<ProductionRecord>.Success(productionRecord);
        }
    }
}