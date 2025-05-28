using Microsoft.EntityFrameworkCore;
using tickets.api.Data;
using tickets.api.Models.Domain;
using tickets.api.Repositories.Interface;

namespace tickets.api.Repositories.Implementation
{
    public class EquipoTrabajoIntegrantesRepository : GenericRepository<EquipoTrabajoIntegrante>, IEquipoTrabajoIntegrantesRepository
    {
        private readonly DbContext _context;
        private readonly DbSet<EquipoTrabajoIntegrante> _dbSet;

        public EquipoTrabajoIntegrantesRepository(DbAb1c8aTicketsContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<EquipoTrabajoIntegrante>();
        }

    }
}
