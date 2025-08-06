using logifly.application.DTOs;
using logifly.application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace logifly.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(Summary = "Sistemdeki Ticketler Getirilir.")]
        [SwaggerResponse(200, "Başarıyla Ticketler Getirildi")]
        [SwaggerResponse(400, "Geçersiz istek && Ticketler Bulunamadı")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _ticketService.GetAllTicketAsync();
            return Ok(result);
        }

        [HttpGet("{id}",Name ="GetTicketById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(Summary = "Yeni bir destek talebi oluşturur.")]
        [SwaggerResponse(200, "Başarıyla oluşturuldu")]
        [SwaggerResponse(400, "Geçersiz istek && Ticket Bulunamadı")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var result = await _ticketService.GetTicketByIdAsync(id);
            return result is not null ? Ok(result) : NotFound("Ticket Bulunamadı");
        }
        [HttpPost]
        [ProducesResponseType(typeof(TicketResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(Summary = "Yeni bir destek talebi oluşturur.")]
        [SwaggerResponse(201, "Başarıyla oluşturuldu", typeof(TicketResponseDto))]
        [SwaggerResponse(400, "Geçersiz istek")]
        public async Task<IActionResult> CreateAsync([FromBody] TicketCreateDto dto)
        {
            var createdTicket = await _ticketService.CreateTicketAsync(dto);
            return CreatedAtRoute("GetTicketById", new { id = createdTicket.Id }, createdTicket);
        }
        
        
        [HttpPut("status/{id}")]
        [ProducesResponseType(typeof(TicketUpdateStatusDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(Summary = "Destek Talebi Güncellendi.")]
        [SwaggerResponse(200, "Başarıyla Güncellendi", typeof(TicketUpdateStatusDto))]
        [SwaggerResponse(400, "Geçersiz istek")]
        public async Task<IActionResult> UpdateStatusAsync(Guid id, [FromQuery] string newStatus){
            var updated = await _ticketService.UpdateTicketStatusAsync(id, newStatus);
            return updated ? NoContent() : BadRequest("Geçersiz ID veya statü");
        }
    }
}
