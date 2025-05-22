namespace tickets.api.Models.DTO.Usuario
{
    public class CrearUsuarioDto
    {
        public string? Usuario { get; set; }
        public string? Nombre { get; set; }
        public string? Apellidos { get; set; }
        public string? Correo { get; set; }
        public string? Password { get; set; }
        public Guid? OrganizacionId { get; set; }
        public string? Rol { get; set; }
        public IFormFile Avatar { get; set; }
    }
}
