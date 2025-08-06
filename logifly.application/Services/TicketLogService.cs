using logifly.application.DTOs;
using logifly.application.Interfaces;
using logifly.domain.Entities;
using logifly.domain.Enums;
using logifly.persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace logifly.application.Services
{
    public class TicketLogService:ITicketLogService
    {
        private readonly LogiflyDbContext _context;
        public TicketLogService(LogiflyDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(TicketLogCreateDto request)
        {
            if (!Enum.TryParse<TicketLogType>(request.LogType, true, out var logType))
                throw new ArgumentException($"Geçersiz log türü: {request.LogType}");

            var log = new TicketLog
            {
                TicketId = request.TicketId,
                LogType = logType,
                Content = request.Content,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = request.CreatedBy
            };
            _context.TicketLogs.Add(log);
            await _context.SaveChangesAsync();
        }

        public async Task<List<TicketLogResponseDto>> GetAllAsync()
        {
            var logs = await _context.TicketLogs.OrderByDescending(t => t.CreatedAt).ToListAsync();

            return logs.Select(l => new TicketLogResponseDto
            {
                Id = l.Id,
                Content=l.Content,
                LogType=l.LogType.ToString(),
                CreatedAt=l.CreatedAt,
                CreatedBy=l.CreatedBy
            }).ToList();
        }

        public async Task<List<TicketLogResponseDto>> GetByTicketIdAsync(Guid ticketId)
        {
            var logs = await _context.TicketLogs.Where(l => l.TicketId == ticketId).OrderByDescending(l => l.CreatedAt).ToListAsync();

            return logs.Select(l => new TicketLogResponseDto
            {
                Id = l.Id,
                Content = l.Content,
                LogType = l.LogType.ToString(),
                CreatedAt = l.CreatedAt,
                CreatedBy = l.CreatedBy
            }).ToList();
        }
    }
}
