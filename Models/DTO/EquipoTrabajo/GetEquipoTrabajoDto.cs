namespace tickets.api.Models.DTO.EquipoTrabajo
{
    public class GetEquipoTrabajoDto
    {
        public Guid Id { get; set; }
        public Guid OrganizacionId { get; set; }
        public string Organizacion { get; set; }
        public string Nombre { get; set; }
        public Guid SupervisorId { get; set; }
        public string Supervisor { get; set; }
        public bool Activo { get; set; }
    }
}
