using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace logifly.application.DTOs
{
    public class TicketLogCreateDto
    {
        public Guid TicketId { get; set; }
        public string Content { get; set; } = string.Empty;
        public string? LogType { get; set; }
        public string? CreatedBy { get; set; }
    }
}
    