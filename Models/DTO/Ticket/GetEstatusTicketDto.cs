namespace tickets.api.Models.DTO.Ticket
{
    public class GetEstatusTicketDto
    {
        public Guid Id { get; set; }
        public int Clave { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }
}
