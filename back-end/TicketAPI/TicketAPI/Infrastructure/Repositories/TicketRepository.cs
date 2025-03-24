using TicketAPI.Domain.Data;
using TicketAPI.Domain.Repositories;
using TicketAPI.Infrastructure.Data;

namespace TicketAPI.Infrastructure.Repositories
{
    public class TicketRepository : Repository<Ticket>, ITicketRepository
    {
        public TicketRepository(MyDbContext context) : base(context) { }
    }
}
