using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using tickets.api.Helpers;
using tickets.api.Models;
using tickets.api.Models.Domain;
using tickets.api.Models.DTO.CatCategoria;
using tickets.api.Models.DTO.EquipoTrabajo;
using tickets.api.Models.DTO.Organizacion;
using tickets.api.Models.Specifications;
using tickets.api.Repositories.Interface;

namespace tickets.api.Controllers.Admin
{
    [Route("api/administrador/[controller]")]
    [ApiController]
    public class EquipoTrabajoController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IEquipoTrabajoRepository equipoTrabajoRepository;

        public EquipoTrabajoController(
            IMapper mapper,
            IEquipoTrabajoRepository equipoTrabajoRepository
            )
        {
            this.mapper = mapper;
            this.equipoTrabajoRepository = equipoTrabajoRepository;
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Crear([FromBody] CrearEquipoTrabajoDto model)
        {
            // Validar si el modelo es válido
            if (!ModelState.IsValid)
            {
                User.GetId();
                return BadRequest("Modelo de datos invalido.");
            }

            try
            {
                var dto = mapper.Map<EquipoTrabajo>(model);
                dto.UsuarioCreacion = Guid.Parse(User.GetId());
                // Agregar el paciente al repositorio
                await this.equipoTrabajoRepository.AddAsync(dto);

                // Devolver la respuesta con el nuevo paciente
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException.Message); // O devolver un BadRequest(400) si el error es de entrada
            }
        }

        [HttpGet]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Get()
        {
            try
            {

                FiltroGlobal filtro = new FiltroGlobal() { IncluirInactivos = true };
                var spec = new EquipoTrabajoSpecification(filtro);
                spec.IncludeStrings = new List<string> { "Organizacion", "Supervisor" };


                var result = await this.equipoTrabajoRepository.ListAsync(spec);
                if (result == null)
                {
                    return NotFound(result);
                }

                var dto = mapper.Map<List<GetEquipoTrabajoDto>>(result);

                return Ok(dto.OrderBy(x => x.Nombre));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException.Message); // O devolver un BadRequest(400) si el error es de entrada
            }

        }

        [HttpPost("AsignarAgentes")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> AsignarAgentes([FromBody] AsignarAgentesRequest request)
        {
            try
            {

                var result = await this.equipoTrabajoRepository.AsignarAgentes(request, User.GetId());
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

        [HttpGet("GetAgentesResponsables")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> GetAgentesResponsables(Guid equipoTrabajoId)
        {
            try
            {

                var result = await this.equipoTrabajoRepository.GetAgentesResponsables(equipoTrabajoId);
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

        [HttpGet("GetAgentesByTicket")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> GetAgentesByTicket(Guid ticketId)
        {
            try
            {

                var result = await this.equipoTrabajoRepository.GetAgentes(ticketId);
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

        [HttpGet("GetCategoriasAsignadas")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> GetCategoriasAsignadas(Guid equipoTrabajoId)
        {
            try
            {

                var result = await this.equipoTrabajoRepository.GetCategoriasAsignadas(equipoTrabajoId);
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

        [HttpPost("AsignarCategorias")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> AsignarCategorias([FromBody] AsignarCategoriasRequest request)
        {
            try
            {

                var result = await this.equipoTrabajoRepository.AsignarCategorias(request, User.GetId());
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
    }
}
