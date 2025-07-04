using tickets.api.Models.Domain;
using tickets.api.Models.DTO.Area;
using tickets.api.Models.DTO.CatCategoria;
using tickets.api.Models.DTO.Ticket;

namespace tickets.api.Repositories.Interface
{
    public interface ITicketRepository : IGenericRepository<Ticket>
    {
        Task<bool> CrearTicket(CrearTicketDto model, string userId, string rutaBase);
        Task<bool> AgregarMensaje(AgregarMensajeDto model, string userId, string rutaBase);
        Task<bool> AgregarArchivos(AgregarArchivosDto model, string userId, string rutaBase);
        Task<List<GetTicketsAbiertosResponse>> GetTicketsAbiertos(GetTicketsAbiertosDto model);
        Task<List<GetTicketsAbiertosResponse>> GetTicketsAbiertosResponsable(GetTicketsAbiertosDto model, string usuarioCreacionId);
    }
}
