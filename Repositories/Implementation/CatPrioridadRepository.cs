using Microsoft.EntityFrameworkCore;
using tickets.api.Data;
using tickets.api.Models.Domain;
using tickets.api.Repositories.Interface;

namespace tickets.api.Repositories.Implementation
{
    public class CatPrioridadRepository : GenericRepository<CatPrioridad>, ICatPrioridadRepository
    {
        private readonly DbContext _context;
        private readonly DbSet<CatPrioridad> _dbSet;

        public CatPrioridadRepository(DbAb1c8aTicketsContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<CatPrioridad>();
        }

    }
}
