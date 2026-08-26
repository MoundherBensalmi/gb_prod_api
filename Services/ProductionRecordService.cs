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

        public async Task<Result<ProductionRecord>> CreateProductionRecordAsync(CreateProductionRecordRequest requset)
        {
            var productionDay = await _productionDayService.GetProductionDayByIdAsync(requset.ProductionDayId);
            if (productionDay == null)
            {
                return AppError.Validation(message: "production_day.invalide", field: nameof(requset.ProductionDayId));
            }

            var isSameDate = productionDay.Date.Equals(requset.ProducedAt.Date);
            if (!isSameDate)
            {
                return AppError.Validation(message: "productionRecord.producedAt.invalide", field: nameof(requset.ProducedAt));
            }

            var productionRecord = new ProductionRecord
            {
                ProductionDayId = requset.ProductionDayId,
                PawGradeId = requset.PawGradeId,
                TunnelId = requset.TunnelId,
                ProducedAt = requset.ProducedAt,
                QuantityKg = requset.QuantityKg,
                MovedOutAt = requset.MovedOutAt,
                Status = ProductionRecordStatus.Accepted,
                Notes = requset.Notes
            };

            _dbContext.ProductionRecords.Add(productionRecord);
            await _dbContext.SaveChangesAsync();

            return Result<ProductionRecord>.Success(productionRecord);
        }
    }
}