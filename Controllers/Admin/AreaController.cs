using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using tickets.api.Helpers;
using tickets.api.Models;
using tickets.api.Models.Domain;
using tickets.api.Models.DTO.Area;
using tickets.api.Models.DTO.Organizacion;
using tickets.api.Models.Specifications;
using tickets.api.Repositories.Interface;

namespace tickets.api.Controllers.Admin
{
    [Route("api/administrador/[controller]")]
    [ApiController]
    public class AreaController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IAreaRepository areaRepository;

        public AreaController(
            IMapper mapper,
            IAreaRepository areaRepository
            )
        {
            this.mapper = mapper;
            this.areaRepository = areaRepository;
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Crear([FromBody] CrearAreaDto model)
        {
            // Validar si el modelo es válido
            if (!ModelState.IsValid)
            {
                User.GetId();
                return BadRequest("Modelo de datos invalido.");
            }

            try
            {
                var dto = mapper.Map<Area>(model);
                dto.UsuarioCreacion = Guid.Parse(User.GetId());
                // Agregar el paciente al repositorio
                await this.areaRepository.AddAsync(dto);

                // Devolver la respuesta con el nuevo paciente
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException.Message); // O devolver un BadRequest(400) si el error es de entrada
            }
        }

        [Authorize(Roles = "Administrador")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(Guid id, [FromBody] CrearAreaDto dto)
        {
            try
            {

                var model = await this.areaRepository.GetByIdAsync(id);
                if (model == null)
                {
                    return NotFound("Dato no encontrado.");
                }

                // Mapear solo los campos permitidos del DTO a la entidad
                mapper.Map(dto, model);

                model.Id = id;
                await this.areaRepository.UpdateAsync(model);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrio un error al actualizar.");
            }
        }

        [HttpGet]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Get()
        {
            try
            {

                FiltroGlobal filtro = new FiltroGlobal() { IncluirInactivos = true };
                var spec = new AreaSpecification(filtro);
                spec.IncludeStrings = new List<string> { "Organizacion", "RelAreaResponsables", "RelAreaResponsables.Usuario" };


                var result = await this.areaRepository.ListAsync(spec);
                if (result == null)
                {
                    return NotFound(result);
                }

                var dto = mapper.Map<List<GetAreasDto>>(result);

                return Ok(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException.Message); // O devolver un BadRequest(400) si el error es de entrada
            }

        }

        [HttpGet("GetResponsablesArea")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> GetResponsablesArea(Guid areaId)
        {
            try
            {

                var result = await this.areaRepository.GetResponsablesAsync(areaId);
                if (result == null)
                {
                    return NotFound(result);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException.Message); // O devolver un BadRequest(400) si el error es de entrada
            }

        }

        [HttpPost("AsignaResponsables")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> AsignaResponsables([FromBody] AsignaResponsablesRequest request)
        {
            try
            {

                var result = await this.areaRepository.AsignaResponsables(request);
                if (result == null)
                {
                    return NotFound(result);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException.Message); // O devolver un BadRequest(400) si el error es de entrada
            }

        }
        [Authorize(Roles = "Administrador")]
        [HttpPut("{id}/desactivar")]
        public async Task<IActionResult> Desactivar(Guid id)
        {
            try
            {
                // Obtener el paciente actual desde la base de datos
                //UpdateContactoDto dto;

                var model = await this.areaRepository.GetByIdAsync(id);
                if (model == null)
                {
                    return NotFound("Dato no encontrado.");
                }

                // Solo actualizamos el campo 'Activo' a false
                model.Activo = false;
                model.UsuarioModificacion = Guid.Parse(User.GetId());
                model.FechaModificacion = DateTime.Now;
                // Guardamos los cambios

                await this.areaRepository.UpdateAsync(model);

                return NoContent(); // Respuesta exitosa sin contenido
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrio un error"); // O devolver un BadRequest(400) si el error es de entrada
            }

        }

        [Authorize(Roles = "Administrador")]
        [HttpPut("{id}/activar")]
        public async Task<IActionResult> Activar(Guid id)
        {
            try
            {
                // Obtener el paciente actual desde la base de datos
                //UpdateContactoDto dto;

                var model = await this.areaRepository.GetByIdAsync(id);
                if (model == null)
                {
                    return NotFound("Dato no encontrado.");
                }

                // Solo actualizamos el campo 'Activo' a false
                model.Activo = true;
                model.UsuarioModificacion = Guid.Parse(User.GetId());
                model.FechaModificacion = DateTime.Now;
                // Guardamos los cambios

                await this.areaRepository.UpdateAsync(model);

                return NoContent(); // Respuesta exitosa sin contenido
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrio un error"); // O devolver un BadRequest(400) si el error es de entrada
            }

        }
    }
}
