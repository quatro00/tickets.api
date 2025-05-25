namespace tickets.api.Models.DTO.Area
{
    public class SubAreaDto
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public List<SubAreaDto> children { get; set; }
    }
}
