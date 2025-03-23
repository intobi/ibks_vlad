using TicketAPI.Domain;
using TicketAPI.Domain.Data;
using TicketAPI.Domain.Repositories;

namespace TicketAPI.Infrastructure.Repositories
{
    public class TicketRepository : Repository<Ticket>, ITicketRepository
    {
        public TicketRepository(MyDbContext context) : base(context) { }
    }
}
