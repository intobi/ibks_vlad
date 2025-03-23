using TicketAPI.Domain.Data;

namespace TicketAPI.Domain.Repositories
{
    public interface ITicketReplyRepository : IRepository<TicketReply>
    {
        Task<List<TicketReply>> GetReplyListByTicketIdAsync(long ticketId);
        Task<TicketReply> GetReplyByIdAsync(int id);
    }
}
