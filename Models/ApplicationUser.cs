using Microsoft.AspNetCore.Identity;

namespace tickets.api.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Avatar { get; set; }
        public Guid? OrganizacionId { get; set; }
        public bool Activo {  get; set; }
    }
}
