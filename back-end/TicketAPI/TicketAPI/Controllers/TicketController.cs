using Microsoft.AspNetCore.Mvc;
using TicketAPI.Application.DTO;
using TicketAPI.Application.Services;

namespace TicketAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tickets = await _ticketService.GetAllTicketsAsync();
            return Ok(tickets);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var ticket = await _ticketService.GetTicketByIdAsync(id);
            if (ticket == null)
                return NotFound();
            return Ok(ticket);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTicketDto createTicketDto)
        {
            if (createTicketDto == null)
                return BadRequest("Invalid ticket data");

            var createdTicket = await _ticketService.CreateTicketAsync(createTicketDto);
            return CreatedAtAction(nameof(GetById), new { id = createdTicket.Id }, createdTicket);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateTicketDto updateTicketDto)
        {
            if (updateTicketDto == null || id == 0)
                return BadRequest("Invalid ticket data");

            var updated = await _ticketService.UpdateTicketAsync(id, updateTicketDto);
            if (!updated)
                return NotFound("Ticket not found");

            return NoContent();
        }

        [HttpGet("test-error")]
        public IActionResult TestError()
        {
            throw new Exception("This is a test error.");
        }
    }
}
