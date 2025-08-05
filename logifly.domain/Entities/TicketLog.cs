using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace logifly.domain.Entities
{
    public class TicketLog
    {
        public Guid Id { get; set; }
        public  Guid TicketId { get; set; }
        public Ticket Ticket { get; set; }
        public string LogType { get; set; } = "INFO";
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } =DateTime.UtcNow;

        public string? CreatedBy { get; set; } //Sistem mi Yoksa Kullanıcı Logu mu

    }
    }
