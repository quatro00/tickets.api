namespace tickets.api.Models.DTO.Ticket
{
    public class CrearTicketDto
    {
        public Guid CategoriaId { get; set; }
        public Guid PrioridadId { get; set; }
        public string Descripcion { get; set; }
        public Guid AreaId { get; set; }
        public string AreaEspecifica { get; set; }
        public string NombreContacto { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public bool AfectaOperacion { get; set; }
        public DateTime DesdeCuandoOcurre { get; set; }
        public List<IFormFile> Archivos { get; set; }
    }
}
