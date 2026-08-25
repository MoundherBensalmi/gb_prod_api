using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            Console.WriteLine("Message: Reached");
            var result  = await _productionRecordService.CreateProductionRecordAsync(request);
            if (result.Success == false)
            {
                Console.WriteLine("Message:" + result.Error!.Message);
                return BadRequest(result.Error);
            }
            
            return ProductionRecordMapper.ToResponse(result.Data!);
        }
    }
}