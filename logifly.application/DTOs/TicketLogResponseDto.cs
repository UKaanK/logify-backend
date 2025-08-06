using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace logifly.application.DTOs
{
    public class TicketLogResponseDto
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public string LogType { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
    }
}
