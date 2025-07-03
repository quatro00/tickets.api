using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using tickets.api.Models.DTO.CatCategoria;
using tickets.api.Models.Specifications;
using tickets.api.Models;
using tickets.api.Repositories.Interface;

namespace tickets.api.Controllers.Responsable
{
    [Route("api/Responsable/[controller]")]
    [ApiController]
    public class CatCategoriaController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ICatCategoriaRepository catCategoriaRepository;

        public CatCategoriaController(
            IMapper mapper,
            ICatCategoriaRepository catCategoriaRepository
            )
        {
            this.mapper = mapper;
            this.catCategoriaRepository = catCategoriaRepository;
        }

        [HttpGet]
        [Authorize(Roles = "Responsable de area")]
        public async Task<IActionResult> Get()
        {
            try
            {

                FiltroGlobal filtro = new FiltroGlobal() { IncluirInactivos = false };
                var spec = new CatCategoriaSpecification(filtro);
                spec.IncludeStrings = new List<string> { };


                var result = await this.catCategoriaRepository.ListAsync(spec);
                if (result == null)
                {
                    return NotFound(result);
                }

                var dto = mapper.Map<List<CatCategoriaDto>>(result);

                return Ok(dto.OrderBy(x => x.Nombre));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException.Message); // O devolver un BadRequest(400) si el error es de entrada
            }

        }
    }
}
