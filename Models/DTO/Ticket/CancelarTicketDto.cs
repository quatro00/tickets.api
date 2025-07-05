namespace tickets.api.Models.DTO.Ticket
{
    public class CancelarTicketDto
    {
        public Guid ticketId { get; set; }
        public string mensaje { get; set; }
    }
}
