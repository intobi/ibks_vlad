using Microsoft.AspNetCore.Mvc;
using TicketAPI.Domain.Data;
using TicketAPI.Domain.Repositories;

namespace TicketAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketReplyController : ControllerBase
    {
        private readonly ITicketReplyRepository _ticketReplyRepository;

        public TicketReplyController(ITicketReplyRepository ticketReplyRepository)
        {
            _ticketReplyRepository = ticketReplyRepository;
        }

        [HttpGet("{ticketId}")]
        public async Task<IActionResult> GetRepliesByTicketId(long ticketId)
        {
            var replies = await _ticketReplyRepository.GetReplyListByTicketIdAsync(ticketId);
            if (replies == null || replies.Count == 0)
            {
                return NotFound();
            }
            return Ok(replies);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TicketReply reply)
        {
            if (reply == null)
                return BadRequest("Invalid reply data");

            reply.ReplyDate = DateTime.UtcNow;

            await _ticketReplyRepository.AddAsync(reply);

            return StatusCode(201, reply);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TicketReply reply)
        {
            if (reply == null || id != reply.ReplyId)
                return BadRequest("Invalid reply data");

            var existingReply = await _ticketReplyRepository.GetReplyByIdAsync(id);
            if (existingReply == null)
                return NotFound("Reply not found");

            existingReply.Reply = reply.Reply;
            existingReply.ReplyDate = DateTime.UtcNow;

            await _ticketReplyRepository.UpdateAsync(existingReply);
            return NoContent();
        }

    }

}
