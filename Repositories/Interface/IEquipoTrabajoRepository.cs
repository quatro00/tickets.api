using tickets.api.Models.Domain;
using tickets.api.Models.DTO.Area;
using tickets.api.Models.DTO.CatCategoria;
using tickets.api.Models.DTO.EquipoTrabajo;

namespace tickets.api.Repositories.Interface
{
    public interface IEquipoTrabajoRepository : IGenericRepository<EquipoTrabajo>
    {
        Task<List<GetResponsablesDto>> GetAgentesResponsables(Guid equipoTrabajoId);
        Task<List<GetResponsablesDto>> GetAgentes(Guid ticketId);
        Task<List<GetResponsablesDto>> GetAgentesByResponsable(string userId);
        Task<bool> AsignarAgentes(AsignarAgentesRequest model, string usuarioId);
        Task<List<GetCategoriasAsignadasResponse>> GetCategoriasAsignadas(Guid equipoTrabajoId);
        Task<bool> AsignarCategorias(AsignarCategoriasRequest model, string usuarioId);
    }
}
