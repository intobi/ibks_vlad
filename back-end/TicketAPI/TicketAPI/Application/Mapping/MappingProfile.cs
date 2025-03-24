using AutoMapper;
using TicketAPI.Application.DTO;
using TicketAPI.Domain.Data;

namespace TicketAPI.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Ticket, TicketDto>().ReverseMap();
            CreateMap<CreateTicketDto, Ticket>();
            CreateMap<UpdateTicketDto, Ticket>()
                .ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<TicketReply, TicketReplyDto>().ReverseMap();
            CreateMap<CreateTicketReplyDto, TicketReply>();
            CreateMap<UpdateTicketReplyDto, TicketReply>()
                .ForMember(dest => dest.ReplyDate, opt => opt.MapFrom(src => DateTime.UtcNow));
        }
    }
}
