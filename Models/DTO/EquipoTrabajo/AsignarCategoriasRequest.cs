namespace tickets.api.Models.DTO.EquipoTrabajo
{
    public class AsignarCategoriasRequest
    {
        public Guid EquipoTrabajoId { get; set; }
        public List<Guid> Categorias { get; set; }
    }
}
