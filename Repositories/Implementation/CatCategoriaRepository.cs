using Microsoft.EntityFrameworkCore;
using tickets.api.Data;
using tickets.api.Models.Domain;
using tickets.api.Repositories.Interface;

namespace tickets.api.Repositories.Implementation
{
    public class CatCategoriaRepository : GenericRepository<CatCategorium>, ICatCategoriaRepository
    {
        private readonly DbContext _context;
        private readonly DbSet<CatCategorium> _dbSet;

        public CatCategoriaRepository(DbAb1c8aTicketsContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<CatCategorium>();
        }

    }
}
