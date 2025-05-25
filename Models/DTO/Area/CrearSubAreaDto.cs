namespace tickets.api.Models.DTO.Area
{
    public class CrearSubAreaDto
    {
        public string Clave {  get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public Guid areaPadreId { get; set; }
    }
}
