using System.ComponentModel.DataAnnotations;

namespace TicketAPI.Application.DTO
{
    public class TicketDto
    {
        public long Id { get; set; }
        public string? Title { get; set; }
        public int ApplicationId { get; set; }
        public string? ApplicationName { get; set; }
        public string? Description { get; set; }
        public int PriorityId { get; set; }
        public int StatusId { get; set; }
        public int? UserId { get; set; }
        public int InstalledEnvironmentId { get; set; }
        public int TicketTypeId { get; set; }
        public DateTime Date { get; set; }
        public DateTime LastModified { get; set; }
    }

    public class CreateTicketDto
    {
        public string? Title { get; set; }
        public int ApplicationId { get; set; }
        public string? ApplicationName { get; set; }
        public string? Description { get; set; }
        public int PriorityId { get; set; }
        public int StatusId { get; set; }
        public int? UserId { get; set; }
        public int InstalledEnvironmentId { get; set; }
        public int TicketTypeId { get; set; }
    }

    public class UpdateTicketDto
    {
        public string ApplicationName { get; set; }
        [Required]
        [StringLength(10, ErrorMessage = "Description can't be longer than 10 characters.")]
        public string? Description { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "PriorityId must be greater than 0.")]
        public int PriorityId { get; set; }
        public int TicketTypeId { get; set; }
    }
}
