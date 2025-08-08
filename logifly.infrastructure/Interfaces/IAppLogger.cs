using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace logifly.infrastructure.Interfaces
{
    public interface IAppLogger<T>
    {
        void LogInformation(string message, Guid? ticketId = null, string? createdBy = null);
        void LogError(string message, Guid? ticketId = null, string? createdBy = null);
        void LogWarning(string message, Guid? ticketId = null, string? createdBy = null);

    }
}
