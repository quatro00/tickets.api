using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Microsoft.EntityFrameworkCore;
using tickets.api.Data;
using tickets.api.Models.Domain;
using tickets.api.Models.DTO.Area;
using tickets.api.Models.DTO.EquipoTrabajo;
using tickets.api.Repositories.Interface;

namespace tickets.api.Repositories.Implementation
{
    public class EquipoTrabajoRepository : GenericRepository<EquipoTrabajo>, IEquipoTrabajoRepository
    {
        private readonly DbContext _context;
        private readonly DbSet<EquipoTrabajo> _dbSet;

        public EquipoTrabajoRepository(DbAb1c8aTicketsContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<EquipoTrabajo>();
        }

        public async Task<List<GetResponsablesDto>> GetAgentesResponsables(Guid equipoTrabajoId)
        {
            var equipoTrabajo = await _context.Set<EquipoTrabajo>()
            .Where(a => a.Id == equipoTrabajoId)
            .Select(a => new { a.OrganizacionId })
            .FirstOrDefaultAsync();

            if (equipoTrabajo == null)
                return new List<GetResponsablesDto>();

            //buscamos los usuarios asignados a la organizacion
            var usuarios = await _context.Set<AspNetUser>()
            .Where(u => u.OrganizacionId == equipoTrabajo.OrganizacionId && u.Roles.Any(x => x.Name == "Agente"))
            .Select(user => new GetResponsablesDto
            {
                Id = user.Id,
                Nombre = user.Nombre,
                Apellidos = user.Apellidos,
                Username = user.UserName,
                Activo = _context.Set<EquipoTrabajoIntegrante>()
                    .Any(r => r.UsuarioId == user.Id && r.EquipoTrabajoId == equipoTrabajoId)
            })
            .ToListAsync();

            return usuarios;
        }
    }
}
