using LojaBenner.Dtos;
using LojaBenner.Enums;
using LojaBenner.Services;
using LojaBenner.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LojaBenner.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;

        public PedidoController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService ?? throw new ArgumentNullException(nameof(pedidoService));
        }

        [HttpPost]
        public async Task<IActionResult> Create(PedidoCreateDto pedidoCreateDto)
        {
            var result = await _pedidoService.Create(pedidoCreateDto);
            return Ok(result);
        }

        [HttpPatch]
        [Route("${id}/status")]
        public async Task<IActionResult> ChangeStatus(int id, StatusPedido status)
        {
            await _pedidoService.ChangeStatus(id, status);
            return Ok(true);
        }
    }
}
