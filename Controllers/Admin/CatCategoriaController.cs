using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using tickets.api.Models.DTO.Area;
using tickets.api.Models.Specifications;
using tickets.api.Models;
using tickets.api.Repositories.Interface;
using tickets.api.Models.DTO.CatCategoria;

namespace tickets.api.Controllers.Admin
{
    [Route("api/administrador/[controller]")]
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
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Get()
        {
            try
            {

                FiltroGlobal filtro = new FiltroGlobal() { IncluirInactivos = true };
                var spec = new AreaSpecification(filtro);
                spec.IncludeStrings = new List<string> { };


                var result = await this.catCategoriaRepository.ListAsync();
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
