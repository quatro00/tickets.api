namespace tickets.api.Models.DTO.EquipoTrabajo
{
    public class CrearEquipoTrabajoDto
    {
        public Guid OrganizacionId {  get; set; }
        public string Nombre { get; set; }
        public string SupervisorId { get; set; }
    }
}
