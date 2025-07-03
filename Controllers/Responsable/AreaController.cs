using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using tickets.api.Repositories.Interface;

namespace tickets.api.Controllers.Responsable
{
    [Route("api/responsable/[controller]")]
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

        [HttpGet("GetArbolAreas")]
        [Authorize(Roles = "Responsable de area")]
        public async Task<IActionResult> GetArbolAreas(Guid organizacionId)
        {
            try
            {

                var result = await this.areaRepository.GetArbolAreasResponsable(organizacionId);
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
