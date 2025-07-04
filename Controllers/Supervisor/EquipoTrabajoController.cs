using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using tickets.api.Helpers;
using tickets.api.Repositories.Interface;

namespace tickets.api.Controllers.Supervisor
{
    [Route("api/supervisor/[controller]")]
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

        [HttpGet("GetAgentesByResponsable")]
        [Authorize(Roles = "Supervisor")]
        public async Task<IActionResult> GetAgentesByResponsable()
        {
            try
            {

                var result = await this.equipoTrabajoRepository.GetAgentesByResponsable(User.GetId());
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
