using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using tickets.api.Helpers;
using tickets.api.Models.Domain;
using tickets.api.Models.DTO.Ticket;
using tickets.api.Repositories.Interface;

namespace tickets.api.Controllers.Admin
{
    [Route("api/administrador/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ITicketRepository ticketRepository;
        private readonly ICatEstatusTicketRepository catEstatusTicketRepository;

        public TicketController(
            IMapper mapper,
            ITicketRepository ticketRepository, 
            ICatEstatusTicketRepository catEstatusTicketRepository
            )
        {
            this.mapper = mapper;
            this.ticketRepository = ticketRepository;
            this.catEstatusTicketRepository = catEstatusTicketRepository;
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)]
        public async Task<IActionResult> Crear([FromForm] CrearTicketDto model)
        {
            // Validar si el modelo es válido
            if (!ModelState.IsValid)
            {
                User.GetId();
                return BadRequest("Modelo de datos invalido.");
            }

            try
            {
                // Devolver la respuesta con el nuevo paciente
                string rutaPublica = $"{Request.Scheme}://{Request.Host}/archivos/";

                await this.ticketRepository.CrearTicket(model,User.GetId(), rutaPublica);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException.Message); // O devolver un BadRequest(400) si el error es de entrada
            }
        }

        [HttpGet("GetDetalle/{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> GetDetalle(Guid id)
        {
            try
            {
                /*
                var result = await this.areaRepository.GetByIdAsync(id);
                if (result == null)
                {
                    return NotFound(result);
                }

                var dto = mapper.Map<GetAreasDto>(result);
                */
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException.Message); // O devolver un BadRequest(400) si el error es de entrada
            }

        }

        [HttpGet("GetTicketsAbiertos")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> GetTicketsAbiertos()
        {
            try
            {
                GetTicketsAbiertosDto model = new GetTicketsAbiertosDto();
                var result = await this.ticketRepository.GetTicketsAbiertos(model);
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
