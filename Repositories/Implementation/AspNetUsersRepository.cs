using Microsoft.EntityFrameworkCore;
using tickets.api.Data;
using tickets.api.Models.Domain;
using tickets.api.Repositories.Interface;

namespace tickets.api.Repositories.Implementation
{
    public class AspNetUsersRepository : GenericRepository<AspNetUser>, IAspNetUsersRepository
    {
        private readonly DbContext _context;
        private readonly DbSet<AspNetUser> _dbSet;

        public AspNetUsersRepository(DbAb1c8aTicketsContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<AspNetUser>();
        }

    }
}
