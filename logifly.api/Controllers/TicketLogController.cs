using logifly.application.DTOs;
using logifly.application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace logifly.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketLogController : ControllerBase
    {
        private readonly ITicketLogService _ticketLogService;
        public TicketLogController(ITicketLogService ticketLogService)
        {
            _ticketLogService = ticketLogService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(Summary = "Sistemdeki Ticketler Getirilir.")]
        [SwaggerResponse(200, "Başarıyla Ticketler Getirildi")]
        [SwaggerResponse(400, "Geçersiz istek && Ticketler Bulunamadı")]
        public async Task<IActionResult> GetAllAsync()
        {
            var logs = await _ticketLogService.GetAllAsync();
            return Ok(logs);
        }

        [HttpGet("{ticketId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(Summary = "Yeni bir destek talebi oluşturur.")]
        [SwaggerResponse(200, "Başarıyla oluşturuldu")]
        [SwaggerResponse(400, "Geçersiz istek && Ticket Bulunamadı")]
        public async Task<IActionResult> GetByTicketIdAsync(Guid ticketId)
        {
            var logs = await _ticketLogService.GetByTicketIdAsync(ticketId);
            return Ok(logs);
        }
        [HttpPost]
        [ProducesResponseType(typeof(TicketLogCreateDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(Summary = "Yeni bir destek Log talebi oluşturur.")]
        [SwaggerResponse(201, "Başarıyla oluşturuldu", typeof(TicketLogCreateDto))]
        [SwaggerResponse(400, "Geçersiz istek")]
        public async Task<IActionResult> AddAsync([FromBody] TicketLogCreateDto request)
        {
           await _ticketLogService.AddAsync(request);
            return NoContent();
        }
    }
}
