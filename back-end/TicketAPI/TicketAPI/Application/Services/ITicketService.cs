using TicketAPI.Application.DTO;

namespace TicketAPI.Application.Services
{
    public interface ITicketService
    {
        Task<IEnumerable<TicketDto>> GetAllTicketsAsync();
        Task<TicketDto> GetTicketByIdAsync(long id);
        Task<TicketDto> CreateTicketAsync(CreateTicketDto createTicketDto);
        Task<bool> UpdateTicketAsync(long id, UpdateTicketDto updateTicketDto);
    }
}
