using TicketAPI.Domain.Repositories;
using TicketAPI.Infrastructure.Repositories;

namespace TicketAPI.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<ITicketRepository, TicketRepository>();
            services.AddScoped<ITicketReplyRepository, TicketReplyRepository>();
        }
    }
}
