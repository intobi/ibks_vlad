namespace TicketAPI.Controllers.Models
{
    public class UpdateTicket
    {
        public string ApplicationName { get; set; }
        public int PriorityId { get; set; }
        public int TicketTypeId { get; set; }
        public string Description { get; set; }
    }
}
