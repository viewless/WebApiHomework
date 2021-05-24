using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public class TicketDetailsDto
    {
        public string EventName { get; set; } = string.Empty;
        public string Owner { get; set; } = string.Empty;
    }
}
