namespace tickets.api.Models.DTO.Ticket
{
    public class AgregarMensajeDto
    {
        public Guid TicketId { get; set; }
        public string Mensaje { get; set; }
        public List<IFormFile> Archivos { get; set; } = new List<IFormFile>();
    }
}
