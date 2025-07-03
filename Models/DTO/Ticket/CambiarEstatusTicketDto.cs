namespace tickets.api.Models.DTO.Ticket
{
    public class CambiarEstatusTicketDto
    {
        public Guid TicketId { get; set; }
        public Guid EstatusTicketId { get; set; }
        public string Mensaje { get; set; }
    }
}
