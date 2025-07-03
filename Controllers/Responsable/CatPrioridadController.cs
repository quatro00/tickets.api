using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using tickets.api.Models.DTO.CatPrioridad;
using tickets.api.Models.Specifications;
using tickets.api.Models;
using tickets.api.Repositories.Interface;

namespace tickets.api.Controllers.Responsable
{
    [Route("api/responsable/[controller]")]
    [ApiController]
    public class CatPrioridadController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ICatPrioridadRepository catPrioridadRepository;

        public CatPrioridadController(
            IMapper mapper,
            ICatPrioridadRepository catPrioridadRepository
            )
        {
            this.mapper = mapper;
            this.catPrioridadRepository = catPrioridadRepository;
        }

        [HttpGet]
        [Authorize(Roles = "Responsable de area")]
        public async Task<IActionResult> Get()
        {
            try
            {

                FiltroGlobal filtro = new FiltroGlobal() { IncluirInactivos = false };
                var spec = new CatPrioridadSpecification(filtro);
                spec.IncludeStrings = new List<string> { };


                var result = await this.catPrioridadRepository.ListAsync(spec);
                if (result == null)
                {
                    return NotFound(result);
                }

                var dto = mapper.Map<List<CatPrioridadDto>>(result);

                return Ok(dto.OrderBy(x => x.Valor));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException.Message); // O devolver un BadRequest(400) si el error es de entrada
            }

        }
    }
}
