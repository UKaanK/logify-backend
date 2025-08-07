using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace logifly.application.DTOs
{
    public class TicketUpdateStatusDto
    {
        public Guid Id { get; set; }
        public string newStatus { get; set; }
    }
}
