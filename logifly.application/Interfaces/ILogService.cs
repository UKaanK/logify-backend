using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace logifly.application.Interfaces
{
    public interface ILogService
    {
        Task LogInfoAsync(string content, Guid ticketId, string createdBy);
        Task LogErrorAsync(string content, Guid ticketId, string createdBy);

    }
}
