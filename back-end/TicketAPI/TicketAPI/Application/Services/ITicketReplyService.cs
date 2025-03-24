using TicketAPI.Application.DTO;

namespace TicketAPI.Application.Services
{
    public interface ITicketReplyService
    {
        Task<IEnumerable<TicketReplyDto>> GetRepliesByTicketIdAsync(long ticketId);
        Task<TicketReplyDto> CreateReplyAsync(CreateTicketReplyDto createTicketReplyDto);
        Task<bool> UpdateReplyAsync(int id, UpdateTicketReplyDto updateTicketReplyDto);
    }
}
