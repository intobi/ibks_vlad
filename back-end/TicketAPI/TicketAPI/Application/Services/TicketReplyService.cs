using AutoMapper;
using TicketAPI.Application.DTO;
using TicketAPI.Domain.Data;
using TicketAPI.Domain.Repositories;

namespace TicketAPI.Application.Services
{
    public class TicketReplyService : ITicketReplyService
    {
        private readonly IMapper _mapper;
        private readonly ITicketReplyRepository _ticketReplyRepository;

        public TicketReplyService(IMapper mapper, ITicketReplyRepository ticketReplyRepository)
        {
            _mapper = mapper;
            _ticketReplyRepository = ticketReplyRepository;
        }

        public async Task<IEnumerable<TicketReplyDto>> GetRepliesByTicketIdAsync(long ticketId)
        {
            var replies = await _ticketReplyRepository.GetReplyListByTicketIdAsync(ticketId);
            return _mapper.Map<IEnumerable<TicketReplyDto>>(replies);
        }

        public async Task<TicketReplyDto> CreateReplyAsync(CreateTicketReplyDto createTicketReplyDto)
        {
            var ticketReply = _mapper.Map<TicketReply>(createTicketReplyDto);
            ticketReply.ReplyDate = DateTime.UtcNow;
            await _ticketReplyRepository.AddAsync(ticketReply);
            return _mapper.Map<TicketReplyDto>(ticketReply);
        }

        public async Task<bool> UpdateReplyAsync(int id, UpdateTicketReplyDto updateTicketReplyDto)
        {
            var existingReply = await _ticketReplyRepository.GetReplyByIdAsync(id);
            if (existingReply == null) return false;

            _mapper.Map(updateTicketReplyDto, existingReply);
            existingReply.ReplyDate = DateTime.UtcNow;
            await _ticketReplyRepository.UpdateAsync(existingReply);
            return true;
        }
    }
}
