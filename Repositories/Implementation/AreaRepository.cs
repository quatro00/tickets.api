using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using tickets.api.Data;
using tickets.api.Models.Domain;
using tickets.api.Models.DTO.Area;
using tickets.api.Repositories.Interface;

namespace tickets.api.Repositories.Implementation
{
    public class AreaRepository : GenericRepository<Area>, IAreaRepository
    {
        private readonly DbContext _context;
        private readonly DbSet<Area> _dbSet;

        public AreaRepository(DbAb1c8aTicketsContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<Area>();
        }

        public async Task<bool> AsignaResponsables(AsignaResponsablesRequest model)
        {
            //borramos todos los responsables del area
            await _context.Set<RelAreaResponsable>().Where(x => x.AreaId == model.AreaId).ExecuteDeleteAsync();

            List<RelAreaResponsable> responsables = new List<RelAreaResponsable>();
            foreach (var item in model.Responsables) 
            {
                await this._context.Set<RelAreaResponsable>().AddAsync(new RelAreaResponsable()
                {
                    AreaId = model.AreaId,
                    UsuarioId = item,
                    Activo = true,
                    FechaCreacion = DateTime.Now,
                });
            }
            this._context.SaveChanges();
            return true;
        }

        public async Task<List<GetResponsablesDto>> GetResponsablesAsync(Guid areaId)
        {

            var area = await _context.Set<Area>()
            .Where(a => a.Id == areaId)
            .Select(a => new { a.OrganizacionId })
            .FirstOrDefaultAsync();

            if (area == null)
                return new List<GetResponsablesDto>();

            //buscamos los usuarios asignados a la organizacion
            var usuarios = await _context.Set<AspNetUser>()
            .Where(u => u.OrganizacionId == area.OrganizacionId && u.Roles.Any(x=>x.Name == "Responsable de area"))
            .Select(user => new GetResponsablesDto
            {
                Id = user.Id,
                Nombre = user.Nombre,
                Apellidos = user.Apellidos,
                Username = user.UserName,
                Activo = _context.Set<RelAreaResponsable>()
                    .Any(r => r.UsuarioId == user.Id && r.AreaId == areaId)
            })
            .ToListAsync();


            return usuarios;
        }

        public async Task<List<SubAreaDto>> GetArbolAreasResponsable(Guid organizacionId)
        {
            var areasRaiz = await _context.Set<Area>()
                .Where(x => x.OrganizacionId == organizacionId && x.AreaPadre == null && x.Activo == true)
                .ToListAsync();

            var resultado = new List<SubAreaDto>();

            foreach (var area in areasRaiz)
            {
                var nodo = await MapearAreaConHijosResponsable(area);
                resultado.Add(nodo);
            }

            return resultado;
        }

        private async Task<SubAreaDto> MapearAreaConHijosResponsable(Area area)
        {
            var hijos = await _context.Set<Area>()
                .Where(x => x.AreaPadreId == area.Id && x.Activo == true)
                .ToListAsync();

            var nodo = new SubAreaDto
            {
                Id = area.Id,
                Nombre = area.Nombre,
                children = new List<SubAreaDto>()
            };

            foreach (var hijo in hijos)
            {
                var hijoDto = await MapearAreaConHijosResponsable(hijo);
                nodo.children.Add(hijoDto);
            }

            return nodo;
        }

        public async Task<List<SubAreaDto>> GetArbolAreas(Guid organizacionId)
        {
            var areasRaiz = await _context.Set<Area>()
                .Where(x => x.OrganizacionId == organizacionId && x.AreaPadre == null)
                .ToListAsync();

            var resultado = new List<SubAreaDto>();

            foreach (var area in areasRaiz)
            {
                var nodo = await MapearAreaConHijos(area);
                resultado.Add(nodo);
            }

            return resultado;
        }

        private async Task<SubAreaDto> MapearAreaConHijos(Area area)
        {
            var hijos = await _context.Set<Area>()
                .Where(x => x.AreaPadreId == area.Id)
                .ToListAsync();

            var nodo = new SubAreaDto
            {
                Id = area.Id,
                Nombre = area.Nombre,
                children = new List<SubAreaDto>()
            };

            foreach (var hijo in hijos)
            {
                var hijoDto = await MapearAreaConHijos(hijo);
                nodo.children.Add(hijoDto);
            }

            return nodo;
        }
    }
}
