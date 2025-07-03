namespace tickets.api.Models.DTO.Ticket
{
    public class AgregarArchivosDto
    {
        public Guid TicketId { get; set; }
        public List<IFormFile> Archivos { get; set; } = new List<IFormFile>();
    }
}
