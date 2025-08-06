using logifly.application.DTOs;
using logifly.application.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _ticketService.GetAllTicketAsync();
            return Ok(result);
        }

        [HttpGet("{id}",Name ="GetTicketById")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var result = await _ticketService.GetTicketByIdAsync(id);
            return result is not null ? Ok(result) : NotFound("Ticket Bulunamadı");
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] TicketCreateDto dto)
        {
            var createdTicket = await _ticketService.CreateTicketAsync(dto);
            return CreatedAtRoute("GetTicketById", new { id = createdTicket.Id }, createdTicket);
        }

    }
}
