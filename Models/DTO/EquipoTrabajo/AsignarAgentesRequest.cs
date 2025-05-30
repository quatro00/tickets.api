namespace tickets.api.Models.DTO.EquipoTrabajo
{
    public class AsignarAgentesRequest
    {
        public Guid EquipoTrabajoId { get; set; }
        public List<string> Responsables { get; set; }
    }
}
