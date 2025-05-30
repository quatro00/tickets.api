namespace tickets.api.Models.DTO.Ticket
{
    public class GetTicketsAbiertosResponse
    {
        public Guid Id { get; set; }
        public int Folio { get; set; }
        public string Organizacion { get; set; }
        public string Solicitante { get; set; }
        public List<string> Area { get; set; }
        public string Estatus { get; set; }
        public string Categoria { get; set; }
        public string Prioridad { get; set; }
        public string Descripcion { get; set; }
        public string ContactoNombre { get; set; }
        public string ContactoTelefono { get; set; }
        public bool AfectaOperacion {  get; set; }
        public DateTime Desde { get; set; }
        public string Asignado { get; set; }

    }
}
