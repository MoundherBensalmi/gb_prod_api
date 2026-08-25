using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;
using gb_prod_api.Data;
using gb_prod_api.DTOs.ProductionRecord;
using gb_prod_api.Models;
using gb_prod_api.Services.Results;

namespace gb_prod_api.Services
{
    public class ProductionRecordService(AppDbContext dbContext, ProductionDayService productionDayService)
    {
        private readonly AppDbContext _dbContext = dbContext;
        private readonly ProductionDayService _productionDayService = productionDayService;

        public async Task<ServiceResult<ProductionRecord>> CreateProductionRecordAsync(CreateProductionRecordRequest requset)
        {
            var productionDay = await _productionDayService.GetProductionDayByIdAsync(requset.ProductionDayId);
            if (productionDay == null)
            {
                return ServiceResult<ProductionRecord>.Fail(
                    new ServiceError{
                        Code = "invalide_production_day",
                        Message = "invalide_production_day",
                        Field = nameof(requset.ProductionDayId)
                    }
                );
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

            return ServiceResult<ProductionRecord>.Ok(productionRecord);
        }
    }
}