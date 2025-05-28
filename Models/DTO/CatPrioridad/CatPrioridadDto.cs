namespace tickets.api.Models.DTO.CatPrioridad
{
    public class CatPrioridadDto
    {
        public Guid Id { get; set; }
        public int Valor { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }
    }
}
