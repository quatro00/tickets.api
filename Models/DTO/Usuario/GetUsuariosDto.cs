using DocumentFormat.OpenXml.Office.CoverPageProps;

namespace tickets.api.Models.DTO.Usuario
{
    public class GetUsuariosDto
    {
        public Guid Id { get; set; }
        public string Avatar { get; set; }
        public string UserName { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Organizacion { get; set; }
        public Guid OrganizacionId { get; set; }
        public List<string> Roles { get; set; }
        public bool Activo { get; set; }
    }
}
