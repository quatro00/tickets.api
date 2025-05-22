namespace tickets.api.Models.DTO.Area
{
    public class GetResponsablesDto
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public bool Activo { get; set; }
    }
}
