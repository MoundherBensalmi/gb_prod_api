using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gb_prod_api.Common;
using gb_prod_api.DTOs.ProductionRecord;
using gb_prod_api.Mappers;
using gb_prod_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace gb_prod_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductionController(ProductionRecordService productionRecordService) : ControllerBase
    {
        private readonly ProductionRecordService _productionRecordService = productionRecordService;

        [HttpPost]
        public async Task<ActionResult<ProductionRecordResponse>> CreateProductionRecord([FromBody] CreateProductionRecordRequest request)
        {
            var result  = await _productionRecordService.CreateProductionRecordAsync(request);
            if (!result.IsSuccess)
            {
                return result.ToErrorActionResult(this);
            }

            return Created(
                String.Empty,
                ProductionRecordMapper.ToResponse(result.Data!)
            );
        }
    }
}