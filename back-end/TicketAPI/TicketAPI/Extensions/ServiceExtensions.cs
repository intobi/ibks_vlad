using TicketAPI.Application.Mapping;
using TicketAPI.Application.Services;
using TicketAPI.Domain.Repositories;
using TicketAPI.Infrastructure.Repositories;

namespace TicketAPI.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<ITicketRepository, TicketRepository>();
            services.AddScoped<ITicketService, TicketService>();
            services.AddScoped<ITicketReplyRepository, TicketReplyRepository>();
            services.AddScoped<ITicketReplyService, TicketReplyService>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}
