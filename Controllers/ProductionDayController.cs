using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using gb_prod_api.Auth;
using gb_prod_api.DTOs.Production;
using gb_prod_api.DTOs.ProductionDay;
using gb_prod_api.Mappers;
using gb_prod_api.Models;
using gb_prod_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace gb_prod_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [HasPermission(Permission.ViewProduction)]
    public class ProductionDayController(ProductionDayService productionDayService) : ControllerBase
    {
        private readonly ProductionDayService _productionDayService = productionDayService;

        [HttpGet]
        public async Task<ActionResult<List<ProductionDayResponse>>> GetProductionDays([FromQuery] GetProductionDaysRequest request)
        {
            var productionDays = await _productionDayService.GetProductionDaysAsync(request.StartDate, request.EndDate);
            return ProductionDayMapper.ToResponse(productionDays);
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<ProductionDayResponse>> GetProductionDayById(long id)
        {
            var productionDay = await _productionDayService.GetProductionDayByIdAsync(id);
            if (productionDay == null)
            {
                return NotFound();
            }
            return ProductionDayMapper.ToResponse(productionDay);
        }
    }
}