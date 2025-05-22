namespace tickets.api.Models.DTO.Area
{
    public class CrearAreaDto
    {
        public Guid? AreaPadreId { get; set; }

        public string Clave { get; set; } = null!;

        public string Nombre { get; set; } = null!;

        public string Telefono { get; set; } = null!;

        public Guid OrganizacionId { get; set; }
    }
}
