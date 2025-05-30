namespace tickets.api.Models.DTO.CatCategoria
{
    public class GetCategoriasAsignadasResponse
    {
        public Guid EquipoTrabajoId { get; set; }
        public Guid CategoriaId { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }
    }
}
