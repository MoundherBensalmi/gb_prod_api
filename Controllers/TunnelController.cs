using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gb_prod_api.Common;
using gb_prod_api.DTOs.Tunel;
using gb_prod_api.Mappers;
using gb_prod_api.Models;
using gb_prod_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace gb_prod_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TunnelController(TunnelService tunnelService) : ControllerBase
    {
        private readonly TunnelService _tunnelService = tunnelService;

        [HttpGet]
        public async Task<ActionResult<List<TunnelResponse>>> GetTunnels()
        {
            var tunnels = await _tunnelService.GetTunnelsAsync();
            return TunelMapper.ToResponse(tunnels);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TunnelResponse>> GetTunnelById(int id)
        {
            var tunnel = await _tunnelService.GetTunnelByIdAsync(id);
            if (tunnel == null)
            {
                return NotFound();
            }
            return TunelMapper.ToResponse(tunnel);
        }

        [HttpPost]
        public async Task<ActionResult<TunnelResponse>> CreateTunnel([FromBody] CreateTunnelRequest request)
        {
            var tunnel = await _tunnelService.CreateTunnelAsync(request.Name, request.ArabicName, request.Capacity);
            return CreatedAtAction(
                nameof(GetTunnelById),
                new { id = tunnel.Id },
                TunelMapper.ToResponse(tunnel)
            );
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<TunnelResponse>> UpdateTunnel(int id, [FromBody] UpdateTunnelRequest request)
        {
            var tunnel = await _tunnelService.UpdateTunnelAsync(id, request.Name, request.ArabicName, request.Capacity);
            if (tunnel == null)
            {
                return NotFound();
            }
            return TunelMapper.ToResponse(tunnel);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteTunnel(int id)
        {
            var result = await _tunnelService.DeleteTunnelAsync(id);
            if(!result.IsSuccess)
            {
                return result.ToErrorActionResult(this);
            }

            return NoContent();
        }
    }
}
