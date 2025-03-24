namespace TicketAPI.Application.DTO
{
    public class TicketReplyDto
    {
        public int ReplyId { get; set; }
        public long Tid { get; set; }
        public string? Reply { get; set; }
        public DateTime ReplyDate { get; set; }
    }

    public class CreateTicketReplyDto
    {
        public long Tid { get; set; }
        public string? Reply { get; set; }
    }

    public class UpdateTicketReplyDto
    {
        public string? Reply { get; set; }
    }
}
