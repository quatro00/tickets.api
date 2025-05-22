namespace tickets.api.Models.DTO.Area
{
    public class GetAreasDto
    {
        public Guid Id { get; set; }
        public string Organizacion { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public List<string> Responsables { get; set; }
        public string Telefono {  get; set; }
        public bool Activo { get; set; }
    }
}
