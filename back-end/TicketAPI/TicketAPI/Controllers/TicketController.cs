using Microsoft.AspNetCore.Mvc;
using TicketAPI.Controllers.Models;
using TicketAPI.Domain.Data;
using TicketAPI.Domain.Repositories;

namespace TicketAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketRepository _ticketRepository;

        public TicketController(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tickets = await _ticketRepository.GetAllAsync();
            return Ok(tickets);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var ticket = await _ticketRepository.GetByIdAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            return Ok(ticket);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Ticket ticket)
        {
            if (ticket == null)
                return BadRequest("Invalid ticket data");

            await _ticketRepository.AddAsync(ticket);
            return CreatedAtAction(nameof(GetById), new { id = ticket.Id }, ticket);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateTicket updateTicket)
        {
            if (updateTicket == null || id == 0)
                return BadRequest("Invalid ticket data");

            var ticket = await _ticketRepository.GetByIdAsync(id);
            if (ticket == null)
                return NotFound("Ticket not found");

            ticket.ApplicationName = updateTicket.ApplicationName;
            ticket.PriorityId = updateTicket.PriorityId;
            ticket.TicketTypeId = updateTicket.TicketTypeId;
            ticket.Description = updateTicket.Description;

            await _ticketRepository.UpdateAsync(ticket);

            return NoContent();
        }


    }

}
