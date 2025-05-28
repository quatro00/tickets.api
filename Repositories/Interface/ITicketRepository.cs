using tickets.api.Models.Domain;
using tickets.api.Models.DTO.Area;
using tickets.api.Models.DTO.Ticket;

namespace tickets.api.Repositories.Interface
{
    public interface ITicketRepository : IGenericRepository<Ticket>
    {
        Task<bool> CrearTicket(CrearTicketDto model, string userId, string rutaBase);
    }
}
