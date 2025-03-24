using Microsoft.AspNetCore.Mvc;
using TicketAPI.Application.DTO;
using TicketAPI.Application.Services;

namespace TicketAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketReplyController : ControllerBase
    {
        private readonly ITicketReplyService _ticketReplyService;

        public TicketReplyController(ITicketReplyService ticketReplyService)
        {
            _ticketReplyService = ticketReplyService;
        }

        [HttpGet("{ticketId}")]
        public async Task<IActionResult> GetRepliesByTicketId(long ticketId)
        {
            var replies = await _ticketReplyService.GetRepliesByTicketIdAsync(ticketId);
            if (replies == null || !replies.Any())
            {
                return NotFound();
            }
            return Ok(replies);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTicketReplyDto createTicketReplyDto)
        {
            if (createTicketReplyDto == null)
                return BadRequest("Invalid reply data");

            var reply = await _ticketReplyService.CreateReplyAsync(createTicketReplyDto);
            return StatusCode(201, reply);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateTicketReplyDto updateTicketReplyDto)
        {
            if (updateTicketReplyDto == null)
                return BadRequest("Invalid reply data");

            var updated = await _ticketReplyService.UpdateReplyAsync(id, updateTicketReplyDto);
            if (!updated)
                return NotFound("Reply not found");

            return NoContent();
        }
    }

}
