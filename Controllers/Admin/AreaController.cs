using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using tickets.api.Helpers;
using tickets.api.Models.Domain;
using tickets.api.Models.DTO.Area;
using tickets.api.Models.DTO.Organizacion;
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
                var result = await this.areaRepository.ListAsync();
                if (result == null)
                {
                    return NotFound(result);
                }

                var dto = mapper.Map<List<AreaDto>>(result);

                return Ok(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException.Message); // O devolver un BadRequest(400) si el error es de entrada
            }

        }
    }
}
