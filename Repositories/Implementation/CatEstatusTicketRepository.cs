using Microsoft.EntityFrameworkCore;
using tickets.api.Data;
using tickets.api.Models.Domain;
using tickets.api.Repositories.Interface;

namespace tickets.api.Repositories.Implementation
{
    public class CatEstatusTicketRepository : GenericRepository<CatEstatusTicket>, ICatEstatusTicketRepository
    {
        private readonly DbContext _context;
        private readonly DbSet<CatEstatusTicketRepository> _dbSet;

        public CatEstatusTicketRepository(DbAb1c8aTicketsContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<CatEstatusTicketRepository>();
        }

    }
}
