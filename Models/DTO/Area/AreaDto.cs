namespace tickets.api.Models.DTO.Area
{
    public class AreaDto
    {

        public Guid? AreaPadreId { get; set; }

        public string Clave { get; set; } = null!;

        public string Nombre { get; set; } = null!;

        public string Telefono { get; set; } = null!;

        public Guid OrganizacionId { get; set; }

        public string ResponsableId { get; set; } = null!;

        public bool Activo { get; set; }
    }
}
