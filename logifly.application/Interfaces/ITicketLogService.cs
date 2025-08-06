using logifly.application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace logifly.application.Interfaces
{
    public interface ITicketLogService
    {
        Task<List<TicketLogResponseDto>> GetAllAsync();
        Task<List<TicketLogResponseDto>> GetByTicketIdAsync(Guid ticketId);
        Task AddAsync(TicketLogCreateDto request);
    }
}
