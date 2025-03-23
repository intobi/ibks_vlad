using Microsoft.EntityFrameworkCore;
using TicketAPI.Domain;
using TicketAPI.Domain.Data;
using TicketAPI.Domain.Repositories;

namespace TicketAPI.Infrastructure.Repositories
{
    public class TicketReplyRepository : Repository<TicketReply>, ITicketReplyRepository
    {
        private readonly DbSet<TicketReply> _dbSet;

        public TicketReplyRepository(MyDbContext context) : base(context)
        {
            _dbSet = _context.Set<TicketReply>();
        }

        public async Task<TicketReply> GetReplyByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<List<TicketReply>> GetReplyListByTicketIdAsync(long ticketId)
        {
            return await _dbSet.Where(r => r.Tid == ticketId).ToListAsync();
        }
    }
}
