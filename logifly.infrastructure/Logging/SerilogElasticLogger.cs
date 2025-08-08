using logifly.infrastructure.Interfaces;
using Serilog;
using Serilog.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace logifly.infrastructure.Logging
{
    public class SerilogElasticLogger<T>:IAppLogger<T>
    {
        private readonly ILogger _logger;

        public SerilogElasticLogger()
        {
            _logger = Log.ForContext<T>();
        }

        public void LogInformation(string message, Guid? ticketId = null, string? createdBy = null)
        {
            using (LogContext.PushProperty("TicketId", ticketId))
            using (LogContext.PushProperty("CreatedBy", createdBy))
            {
                _logger.Information(message);
            }
        }

        public void LogError(string message, Guid? ticketId = null, string? createdBy = null)
        {
            using (LogContext.PushProperty("TicketId", ticketId))
            using (LogContext.PushProperty("CreatedBy", createdBy))
            {
                _logger.Error(message);
            }
        }

        public void LogWarning(string message, Guid? ticketId = null, string? createdBy = null)
        {
            using (LogContext.PushProperty("TicketId", ticketId))
            using (LogContext.PushProperty("CreatedBy", createdBy))
            {
                _logger.Warning(message);
            }
        }
    }
}
