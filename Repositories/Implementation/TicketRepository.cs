using AutoMapper;
using DocumentFormat.OpenXml.Presentation;
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

        public async Task<bool> AgregarArchivos(AgregarArchivosDto model, string userId, string rutaBase)
        {
            List<string> urls = new List<string>();
            List<TicketArchivo> ticketArchivos = new List<TicketArchivo>();
            string mensaje = "Se agregan los archivos: ";
            foreach (var item in model.Archivos)
            {
                mensaje = mensaje + item.FileName + ", ";
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
                    Nombre = item.FileName,
                    TicketId = model.TicketId,
                    Fecha = DateTime.Now,
                    Url = rutaPublica
                });
            }
            mensaje = mensaje.Trim().Substring(0, mensaje.Trim().Length - 1);
            TicketHistorial ticketHistorial = new TicketHistorial() 
            {
                TicketId = model.TicketId,
                Fecha = DateTime.Now,
                UsuarioId = userId,
                Comentario = mensaje
            };
            await _context.Set<TicketHistorial>().AddAsync(ticketHistorial);
            await _context.Set<TicketArchivo>().AddRangeAsync(ticketArchivos);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> AgregarMensaje(AgregarMensajeDto model, string userId, string rutaBase)
        {
            List<string> urls = new List<string>();
            List<TicketHistorialArchivo> ticketHistorialArchivo = new List<TicketHistorialArchivo>();

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
                ticketHistorialArchivo.Add(new TicketHistorialArchivo()
                {
                    Nombre = item.FileName,
                    TicketHistorialId = model.TicketId,
                    RutaPublica = rutaPublica,
                    RutaLocal = pathCompleto,
                    Activo = true,
                    FechaCreacion = DateTime.Now,
                    UsuarioCreacion = userId
                });
            }

            TicketHistorial ticketHistorial = new TicketHistorial()
            {
                TicketId = model.TicketId,
                Fecha = DateTime.Now,
                UsuarioId = userId,
                Comentario = model.Mensaje,
                TicketHistorialArchivos = ticketHistorialArchivo,
            };

            await _context.Set<TicketHistorial>().AddAsync(ticketHistorial);
            await _context.SaveChangesAsync();

            return true;
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

        public async Task<List<GetTicketsAbiertosResponse>> GetTicketsAbiertosResponsable(GetTicketsAbiertosDto model, string usuarioCreacionId)
        {
            var result = await this._dbSet
                .Include(t => t.UsuarioCreacion)
                .Include(t => t.Area)
                    .ThenInclude(a => a.AreaPadre)
                .Include(t => t.Area)
                    .ThenInclude(a => a.Organizacion)
                .Include(t => t.EstatusTicket)
                .Include(t => t.Categoria)
                .Include(t => t.UsuarioCreacion)
                .Include(t => t.UsuarioAsignado)
                .Include(t => t.Prioridad)
                .Where(x => x.EstatusTicket.Clave == 1 && (x.UsuarioCreacionId.ToUpper() == usuarioCreacionId.ToUpper()))
                .ToListAsync();

            List<GetTicketsAbiertosResponse> res = result
                .Select(x =>
                new GetTicketsAbiertosResponse()
                {
                    Id = x.Id,
                    Folio = x.Folio,
                    Organizacion = x.Area.Organizacion.Nombre,
                    Solicitante = x.UsuarioCreacion.Nombre + " " + x.UsuarioCreacion.Apellidos,
                    Area = ObtenerJerarquiaArea(x.Area),
                    Estatus = x.EstatusTicket.Descripcion.ToString(),
                    Categoria = x.Categoria.Nombre,
                    Prioridad = x.Prioridad.Nombre,
                    Descripcion = x.Descripcion.ToString(),
                    ContactoNombre = x.NombreContacto ?? "",
                    ContactoTelefono = x.TelefonoContacto ?? "",
                    AfectaOperacion = x.AfectaOperacion,
                    Desde = x.DesdeCuando,
                    Asignado = x.UsuarioAsignado != null ? x.UsuarioAsignado.Nombre + " " + x.UsuarioAsignado.Apellidos : ""
                }).ToList();
            /*              
            foreach (var ticket in result)
            {
                var jerarquia = ObtenerJerarquiaArea(ticket.Area);
                Console.WriteLine($"Jerarquía del ticket {ticket.Id}: {string.Join(" > ", jerarquia.Select(a => a.Nombre))}");
            }
            */
            return res;
        }
        public async Task<List<GetTicketsAbiertosResponse>> GetTicketsAbiertos(GetTicketsAbiertosDto model)
        {
            var result = await this._dbSet
                .Include(t => t.UsuarioCreacion)
                .Include(t => t.Area)
                    .ThenInclude(a => a.AreaPadre)
                .Include(t => t.Area)
                    .ThenInclude(a => a.Organizacion)
                .Include(t => t.EstatusTicket)
                .Include(t => t.Categoria)
                .Include(t => t.UsuarioCreacion)
                .Include(t => t.UsuarioAsignado)
                .Include(t => t.Prioridad)
                .Where(x=>x.EstatusTicket.Clave == 1)
                .ToListAsync();

            List<GetTicketsAbiertosResponse> res = result
                .Select(x => 
                new GetTicketsAbiertosResponse() { 
                Id = x.Id,
                Folio = x.Folio,
                Organizacion = x.Area.Organizacion.Nombre,
                Solicitante = x.UsuarioCreacion.Nombre + " " + x.UsuarioCreacion.Apellidos,
                Area = ObtenerJerarquiaArea(x.Area),
                Estatus = x.EstatusTicket.Descripcion.ToString(),
                Categoria = x.Categoria.Nombre,
                Prioridad = x.Prioridad.Nombre,
                Descripcion = x.Descripcion.ToString(),
                ContactoNombre = x.NombreContacto ?? "",
                ContactoTelefono = x.TelefonoContacto ?? "",
                AfectaOperacion = x.AfectaOperacion,
                Desde = x.DesdeCuando,
                Asignado = x.UsuarioAsignado != null ? x.UsuarioAsignado.Nombre + " " + x.UsuarioAsignado.Apellidos : ""
                }).ToList();
            /*              
            foreach (var ticket in result)
            {
                var jerarquia = ObtenerJerarquiaArea(ticket.Area);
                Console.WriteLine($"Jerarquía del ticket {ticket.Id}: {string.Join(" > ", jerarquia.Select(a => a.Nombre))}");
            }
            */
            return res;
        }

        public List<string> ObtenerJerarquiaArea(Area area)
        {
            var jerarquia = new List<string>();
            while (area != null)
            {
                jerarquia.Insert(0, area.Nombre); // Insertar al inicio para que quede de base hacia hoja
                if (area.AreaPadre == null) { break; }
                area = area.AreaPadre;
            }
            return jerarquia;
        }
    }
}
