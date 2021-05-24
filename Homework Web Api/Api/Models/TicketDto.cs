using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public class TicketDto
    {
        [Required]
        [MinLength(3)]
        public string EventName { get; set; } = string.Empty;

        [Required]
        [MinLength(2)]
        public string Owner { get; set; } = string.Empty;
    }
}
