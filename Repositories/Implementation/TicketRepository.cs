using AutoMapper;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.EntityFrameworkCore;
using tickets.api.Data;
using tickets.api.Models.Domain;
using tickets.api.Models.DTO.Ticket;
using tickets.api.Repositories.Interface;
using tickets.api.Services;

namespace tickets.api.Repositories.Implementation
{
    public class TicketRepository : GenericRepository<Ticket>, ITicketRepository
    {
        private readonly IMapper mapper;
        private readonly DbContext _context;
        private readonly DbSet<Ticket> _dbSet;
        private readonly IEmailService _emailService;

        public TicketRepository(IMapper mapper,DbAb1c8aTicketsContext context, IEmailService emailService) : base(context)
        {
            this.mapper = mapper;
            _context = context;
            _dbSet = _context.Set<Ticket>();
            _emailService = emailService;
        }

        public async Task<bool> CrearTicket(CrearTicketDto model, string userId, string rutaBase)
        {
            //buscamos los estatus de los tickets
            var estatusTicket = await _context.Set<CatEstatusTicket>()
                .Where(x => x.Clave == 1)
                .FirstOrDefaultAsync();

            List<string> urls = new List<string>();
            List<TicketArchivo> ticketArchivos = new List<TicketArchivo>();
            List<TicketHistorial> ticketHistorial = new List<TicketHistorial>();
            //guardamos las imagenes
            foreach (var item in model.Archivos) 
            {
                var id = Guid.NewGuid().ToString();
                var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "archivos");
                var ext = Path.GetExtension(item.FileName);
                id = id + ext;
                string pathCompleto = Path.Combine(uploadPath, id);
                using var stream = new FileStream(pathCompleto, FileMode.Create);
                await item.CopyToAsync(stream);
                string rutaPublica = rutaBase + $"{id}";
                ticketArchivos.Add(new TicketArchivo()
                {
                    Fecha = DateTime.Now,
                    Url = rutaPublica,
                    Nombre = item.FileName,
                });
                urls.Add(rutaPublica);
            }
            var dto = mapper.Map<Ticket>(model);
            dto.UsuarioCreacionId = userId;
            dto.EstatusTicketId = estatusTicket.Id;

            //creamos el historial
            ticketHistorial.Add(new TicketHistorial()
            {
                Fecha = DateTime.Now,
                UsuarioId = userId,
                Comentario = estatusTicket.Descripcion.ToString(),
            });

            dto.TicketArchivos = ticketArchivos;
            dto.TicketHistorials = ticketHistorial;

            

            await this._context.AddAsync(dto);
            await this._context.SaveChangesAsync();

            var ticket = await this._context.Set<Ticket>()
    .Include(t => t.Area)
    .Include(t => t.UsuarioCreacion)
    .Include(t => t.EstatusTicket)
    .Include(t => t.Prioridad)
    .FirstOrDefaultAsync(t => t.Id == dto.Id);

            await this._emailService.SendTicketNotificationAsync(ticket);

            return true;
        }
    }
}
