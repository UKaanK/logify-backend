using logifly.application.Interfaces;
using logifly.domain.Entities;
using logifly.domain.Enums;
using logifly.infrastructure.Logging;
using logifly.persistence.Contexts;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<LogService> _logger;
        public LogService(LogiflyDbContext logiflyDbContext,ILogger<LogService> logger)
        {
            _logiflyDbContext = logiflyDbContext;
            _logger = logger;
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

            // Serilog'a yaz
            _logger.LogError("TICKET ERROR | TicketId: {TicketId} | CreatedBy: {CreatedBy} | Content: {Content}",
                ticketId, createdBy, content);
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
            // Serilog'a yaz
            _logger.LogInformation("TICKET INFO | TicketId: {TicketId} | CreatedBy: {CreatedBy} | Content: {Content}",
                ticketId, createdBy, content);

        }
    }
}
