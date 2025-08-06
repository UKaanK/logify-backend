using logifly.application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace logifly.application.Interfaces
{
    public interface ITicketService
    {
        Task<TicketResponseDto> CreateTicketAsync(TicketCreateDto dto);
        Task<TicketResponseDto?> GetTicketByIdAsync(Guid id);
        Task<IEnumerable<TicketResponseDto>> GetAllTicketAsync();
        Task<bool> UpdateTicketStatusAsync(Guid id, string newStatus);
    }
}
