namespace tickets.api.Models.DTO.Area
{
    public class AsignaResponsablesRequest
    {
        public Guid AreaId { get; set; }
        public List<string> Responsables { get; set; }
    }
}
