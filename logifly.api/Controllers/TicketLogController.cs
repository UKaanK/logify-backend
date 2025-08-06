using logifly.application.DTOs;
using logifly.application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> GetAllAsync()
        {
            var logs = await _ticketLogService.GetAllAsync();
            return Ok(logs);
        }

        [HttpGet("{ticketId}")]
        public async Task<IActionResult> GetByTicketIdAsync(Guid ticketId)
        {
            var logs = await _ticketLogService.GetByTicketIdAsync(ticketId);
            return Ok(logs);
        }
        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] TicketLogCreateDto request)
        {
           await _ticketLogService.AddAsync(request);
            return NoContent();
        }
    }
}
