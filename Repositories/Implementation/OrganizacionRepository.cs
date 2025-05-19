using Microsoft.EntityFrameworkCore;
using tickets.api.Data;
using tickets.api.Models.Domain;
using tickets.api.Repositories.Interface;

namespace tickets.api.Repositories.Implementation
{
    public class OrganizacionRepository : GenericRepository<Organizacion>, IOrganizacionRepository
    {
        private readonly DbContext _context;
        private readonly DbSet<Organizacion> _dbSet;

        public OrganizacionRepository(DbAb1c8aTicketsContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<Organizacion>();
        }

    }
}
