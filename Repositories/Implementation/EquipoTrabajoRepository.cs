using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Microsoft.EntityFrameworkCore;
using tickets.api.Data;
using tickets.api.Models.Domain;
using tickets.api.Models.DTO.Area;
using tickets.api.Models.DTO.CatCategoria;
using tickets.api.Models.DTO.EquipoTrabajo;
using tickets.api.Repositories.Interface;

namespace tickets.api.Repositories.Implementation
{
    public class EquipoTrabajoRepository : GenericRepository<EquipoTrabajo>, IEquipoTrabajoRepository
    {
        private readonly DbContext _context;
        private readonly DbSet<EquipoTrabajo> _dbSet;

        public EquipoTrabajoRepository(DbAb1c8aTicketsContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<EquipoTrabajo>();
        }

        public async Task<bool> AsignarAgentes(AsignarAgentesRequest model, string usuarioId)
        {
            await _context.Set<EquipoTrabajoIntegrante>().Where(x => x.EquipoTrabajoId == model.EquipoTrabajoId).ExecuteDeleteAsync();

            List<EquipoTrabajoIntegrante> responsables = new List<EquipoTrabajoIntegrante>();
            foreach (var item in model.Responsables)
            {
                await this._context.Set<EquipoTrabajoIntegrante>().AddAsync(new EquipoTrabajoIntegrante()
                {
                    EquipoTrabajoId = model.EquipoTrabajoId,
                    UsuarioId = item,
                    Activo = true,
                    FechaCreacion = DateTime.Now,
                    UsuarioCreacion = usuarioId
                });
            }
            this._context.SaveChanges();
            return true;
        }

        public async Task<bool> AsignarCategorias(AsignarCategoriasRequest model, string usuarioId)
        {
            await _context.Set<RelEquipoTrabajoCategorium>().Where(x => x.EquipoTrabajoId == model.EquipoTrabajoId).ExecuteDeleteAsync();

            List<RelEquipoTrabajoCategorium> responsables = new List<RelEquipoTrabajoCategorium>();
            foreach (var item in model.Categorias)
            {
                await this._context.Set<RelEquipoTrabajoCategorium>().AddAsync(new RelEquipoTrabajoCategorium()
                {
                    EquipoTrabajoId = model.EquipoTrabajoId,
                    CategoriaId = item,
                    Activo = true
                });
            }
            this._context.SaveChanges();
            return true;
        }

        public async Task<List<GetResponsablesDto>> GetAgentes(Guid ticketId)
        {
            var ticket = await _context.Set<Ticket>().Include(x=>x.Area).Where(x => x.Id == ticketId).FirstAsync();
            //buscamos los usuarios asignados a la organizacion
            var usuarios = await _context.Set<AspNetUser>()
            .Where(u => u.OrganizacionId == ticket.Area.OrganizacionId && u.Roles.Any(x => x.Name == "Agente"))
            .Select(user => new GetResponsablesDto
            {
                Id = user.Id,
                Nombre = user.Nombre,
                Apellidos = user.Apellidos,
                Username = user.UserName,
                Activo = true
            })
            .ToListAsync();

            return usuarios;
        }


        public async Task<List<GetResponsablesDto>> GetAgentesResponsables(Guid equipoTrabajoId)
        {
            var equipoTrabajo = await _context.Set<EquipoTrabajo>()
            .Where(a => a.Id == equipoTrabajoId)
            .Select(a => new { a.OrganizacionId })
            .FirstOrDefaultAsync();

            if (equipoTrabajo == null)
                return new List<GetResponsablesDto>();

            //buscamos los usuarios asignados a la organizacion
            var usuarios = await _context.Set<AspNetUser>()
            .Where(u => u.OrganizacionId == equipoTrabajo.OrganizacionId && u.Roles.Any(x => x.Name == "Agente"))
            .Select(user => new GetResponsablesDto
            {
                Id = user.Id,
                Nombre = user.Nombre,
                Apellidos = user.Apellidos,
                Username = user.UserName,
                Activo = _context.Set<EquipoTrabajoIntegrante>()
                    .Any(r => r.UsuarioId == user.Id && r.EquipoTrabajoId == equipoTrabajoId)
            })
            .ToListAsync();

            return usuarios;
        }

        public async Task<List<GetCategoriasAsignadasResponse>> GetCategoriasAsignadas(Guid equipoTrabajoId)
        {
            //obtenemos todas las categorias
            var categorias = await _context.Set<CatCategorium>()
            .ToListAsync();

            if (categorias == null)
                return new List<GetCategoriasAsignadasResponse>();

            List<GetCategoriasAsignadasResponse> lista = new List<GetCategoriasAsignadasResponse>();
            foreach (var categoria in categorias)
            {
                bool activo = false;

                var relacion = await _context.Set<RelEquipoTrabajoCategorium>()
                    .Where(x => x.EquipoTrabajoId == equipoTrabajoId && x.CategoriaId == categoria.Id && x.Activo == true).FirstOrDefaultAsync();

                if (relacion != null) { activo = true; }

                GetCategoriasAsignadasResponse itm = new GetCategoriasAsignadasResponse()
                {
                    EquipoTrabajoId = equipoTrabajoId,
                    CategoriaId = categoria.Id,
                    Nombre = categoria.Nombre,
                    Activo = activo,
                };

                lista.Add(itm);
            }

            return lista;
        }
    }
}
