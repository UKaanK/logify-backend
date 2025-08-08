using logifly.application.DTOs;
using logifly.application.Interfaces;
using logifly.domain.Entities;
using logifly.persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace logifly.application.Services
{
    public class TicketService : ITicketService
    {
        private readonly LogiflyDbContext _context;
        private readonly ILogService _logService;
        public TicketService(LogiflyDbContext context,ILogService logService)
        {
            _context = context;
            _logService = logService;
        }
        public async Task<TicketResponseDto> CreateTicketAsync(TicketCreateDto dto)
        {
            var ticket = new Ticket
            {
                Title = dto.Title,
                Message = dto.Message,
                CreatedBy = dto.CreatedBy,
                CreatedAt = DateTime.UtcNow,
                Status = domain.Enums.TicketStatus.Pending
            };
            await _context.Tickets.AddAsync(ticket);
            await _context.SaveChangesAsync();

            await _logService.LogInfoAsync($"Ticket Oluşturuldu: {ticket.Title} - {ticket.Message}",ticket.Id,ticket.CreatedBy??"Unkown");

            return new TicketResponseDto
            {
                Id = ticket.Id,
                Title = ticket.Title,
                Message = ticket.Message,
                CreatedAt = ticket.CreatedAt,
                CreatedBy = ticket.CreatedBy,
                Status = ticket.Status.ToString()
            };


        }

        public async Task<IEnumerable<TicketResponseDto>> GetAllTicketAsync()
        {
            var tickets = await _context.Tickets.ToListAsync();
            var result = new List<TicketResponseDto>();
            foreach (var ticket in tickets)
            {
                result.Add(new TicketResponseDto
                {
                    Id = ticket.Id,
                    Title = ticket.Title,
                    Message = ticket.Message,
                    CreatedAt = ticket.CreatedAt,
                    CreatedBy = ticket.CreatedBy,
                    Status = ticket.Status.ToString()
                });
            }
            return result;
        }

        public async Task<TicketResponseDto?> GetTicketByIdAsync(Guid id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null) return null;

            return new TicketResponseDto
            {
                Id = ticket.Id,
                Title = ticket.Title,
                Message = ticket.Message,
                CreatedAt = ticket.CreatedAt,
                CreatedBy = ticket.CreatedBy,
                Status = ticket.Status.ToString()
            };
        }

        public async Task<bool> UpdateTicketStatusAsync(Guid id, string newStatus)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null) return false;
            if (Enum.TryParse<domain.Enums.TicketStatus>(newStatus,true,out var status))
            {
                ticket.Status = status;
                await _context.SaveChangesAsync();

                await _logService.LogInfoAsync($"Ticket Durumu Güncellendi: {ticket.Title} - Yeni Durum: {ticket.Status}", ticket.Id, ticket.CreatedBy ?? "Unkown");
                return true;
            }
            return false;

        }
    }
}
