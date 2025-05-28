using tickets.api.Models.Domain;
using tickets.api.Models.DTO.Area;
using tickets.api.Models.DTO.EquipoTrabajo;

namespace tickets.api.Repositories.Interface
{
    public interface IEquipoTrabajoRepository : IGenericRepository<EquipoTrabajo>
    {
        Task<List<GetResponsablesDto>> GetAgentesResponsables(Guid equipoTrabajoId);
    }
}
