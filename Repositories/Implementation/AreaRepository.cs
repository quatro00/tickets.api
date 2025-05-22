using Microsoft.EntityFrameworkCore;
using tickets.api.Data;
using tickets.api.Models.Domain;
using tickets.api.Repositories.Interface;

namespace tickets.api.Repositories.Implementation
{
    public class AreaRepository : GenericRepository<Area>, IAreaRepository
    {
        private readonly DbContext _context;
        private readonly DbSet<Area> _dbSet;

        public AreaRepository(DbAb1c8aTicketsContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<Area>();
        }

    }
}
