﻿using tickets.api.Models.Domain;
using tickets.api.Models.DTO.Area;

namespace tickets.api.Repositories.Interface
{
    public interface IAreaRepository : IGenericRepository<Area>
    {
        Task<List<GetResponsablesDto>> GetResponsablesAsync(Guid areaId);
        Task<bool> AsignaResponsables(AsignaResponsablesRequest model);
        Task<List<SubAreaDto>> GetArbolAreas(Guid organizacionId);
        Task<List<SubAreaDto>> GetArbolAreasResponsable(Guid organizacionId);
    }
}
