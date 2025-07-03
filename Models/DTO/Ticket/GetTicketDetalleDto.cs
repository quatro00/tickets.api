namespace tickets.api.Models.DTO.Ticket
{
    public class GetTicketDetalleDto
    {
        public Guid Id { get; set; }
        public int Folio { get; set; }
        public string Categoria { get; set; }
        public string Prioridad { get; set; }
        public string Area { get; set; }
        public string Descripcion { get; set; }
        public string AfectaOperacion { get; set; }
        public DateTime DesdeCuandoSePresenta { get; set; }
        public string Estatus { get; set; }
        public string ContactoNombre { get; set; }
        public string ContactoCorreo { get; set; }
        public string ContactoTelefono { get; set; }
        public string AsignadoNombre { get; set; }
        public string AsignadoCorreo { get; set; }
        public string AsignadoTelefono { get; set; }
        public List<GetTicketDetalleArchivo> Archivos { get; set; } = new List<GetTicketDetalleArchivo>();
        public List<GetTicketDetalleMensaje> Mensajes { get; set; } = new List<GetTicketDetalleMensaje>();
    }
    public class GetTicketDetalleMensaje
    {
        public Guid Id { get; set;}
        public string UserName { get; set; }
        public string Nombre { get; set; }
        public string Perfil { get; set; }
        public string Mensaje { get; set; }
        public DateTime Fecha { get; set; }
        public List<GetTicketDetalleMensajeArchivo> Archivos { get; set; } = new List<GetTicketDetalleMensajeArchivo>();
    }
    public class GetTicketDetalleArchivo
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Url { get; set; }
        public DateTime Fecha { get; set; }
    }
    public class GetTicketDetalleMensajeArchivo
    {
        public Guid Id { get; set; }
        public Guid TicketHistorialId { get; set; }
        public string Nombre { get; set; }
        public string RutaPublica { get; set; }
    }
}
