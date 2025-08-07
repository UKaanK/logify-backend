using logifly.application.Interfaces;
using logifly.domain.Entities;
using logifly.domain.Enums;
using logifly.persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace logifly.application.Services
{
    public class LogService : ILogService
    {
        private readonly LogiflyDbContext _logiflyDbContext;
        public LogService(LogiflyDbContext logiflyDbContext)
        {
            _logiflyDbContext = logiflyDbContext;   
        }
        public async Task LogErrorAsync(string content, Guid ticketId, string createdBy)
        {
            var log = new TicketLog
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                Content = content,
                CreatedBy = createdBy,
                LogType = TicketLogType.ERROR,
                TicketId = ticketId,
            };
             _logiflyDbContext.TicketLogs.Add(log);
            await _logiflyDbContext.SaveChangesAsync();
        }

        public async Task LogInfoAsync(string content, Guid ticketId, string createdBy)
        {
            var log = new TicketLog
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                Content = content,
                CreatedBy = createdBy,
                LogType = TicketLogType.INFO,
                TicketId = ticketId,
            };
            _logiflyDbContext.TicketLogs.Add(log);
            await _logiflyDbContext.SaveChangesAsync();
        }
    }
}
