using AutoMapper;
using TicketAPI.Application.DTO;
using TicketAPI.Domain.Data;
using TicketAPI.Domain.Repositories;

namespace TicketAPI.Application.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IMapper _mapper;

        public TicketService(ITicketRepository ticketRepository, IMapper mapper)
        {
            _ticketRepository = ticketRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TicketDto>> GetAllTicketsAsync()
        {
            var tickets = await _ticketRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<TicketDto>>(tickets);
        }

        public async Task<TicketDto> GetTicketByIdAsync(long id)
        {
            var ticket = await _ticketRepository.GetByIdAsync(id);
            return ticket == null ? null : _mapper.Map<TicketDto>(ticket);
        }

        public async Task<TicketDto> CreateTicketAsync(CreateTicketDto createTicketDto)
        {
            var ticket = _mapper.Map<Ticket>(createTicketDto);
            await _ticketRepository.AddAsync(ticket);
            return _mapper.Map<TicketDto>(ticket);
        }

        public async Task<bool> UpdateTicketAsync(long id, UpdateTicketDto updateTicketDto)
        {
            var ticket = await _ticketRepository.GetByIdAsync(id);
            if (ticket == null) return false;

            _mapper.Map(updateTicketDto, ticket);
            await _ticketRepository.UpdateAsync(ticket);

            return true;
        }
    }
}
