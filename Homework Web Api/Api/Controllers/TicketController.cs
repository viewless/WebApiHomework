using Api.Entities;
using Api.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/ticket")]
    public class TicketController : ControllerBase
    {
        private readonly TicketContext _ticketContext;
        private readonly IMapper _mapper;

        public TicketController(TicketContext ticketContext, IMapper mapper)
        {
            this._ticketContext = ticketContext;
            this._mapper = mapper;
        }

        [HttpGet]
        public ActionResult<List<TicketDetailsDto>> Get()
        {
            var tickets = _ticketContext.Tickets.ToList();
            var ticketDto = _mapper.Map<List<TicketDetailsDto>>(tickets);
            return Ok(ticketDto);
        }

        [HttpPost]
        public ActionResult Post([FromBody] TicketDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ticket = _mapper.Map<Ticket>(model);
            _ticketContext.Tickets.Add(ticket);
            _ticketContext.SaveChanges();

            var key = ticket.EventName.Replace(" ", "-").ToLower();

            return Created("api/ticket/" + key, null);
        }

        [HttpPut("{name}")]
        public ActionResult Put(string name, [FromBody] TicketDto model)
        {
            var ticket = _ticketContext.Tickets
                .FirstOrDefault(x => x.EventName.Replace(" ", "-").ToLower() == name.ToLower());

            if (ticket == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ticket.EventName = model.EventName;
            ticket.Owner = model.Owner;

            _ticketContext.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{name}")]
        public ActionResult Delete(string name)
        {
            var ticket = _ticketContext.Tickets
                .FirstOrDefault(x => x.EventName.Replace(" ", "-").ToLower() == name.ToLower());

            if (ticket == null)
            {
                return NotFound();
            }

           
            _ticketContext.Remove(ticket);
            _ticketContext.SaveChanges();

            return NoContent();
        }

    }
}
