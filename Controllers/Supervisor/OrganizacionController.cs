using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using tickets.api.Helpers;
using tickets.api.Models.DTO.Organizacion;
using tickets.api.Models.Specifications;
using tickets.api.Models;
using tickets.api.Repositories.Interface;

namespace tickets.api.Controllers.Supervisor
{
    [Route("api/supervisor/[controller]")]
    [ApiController]
    public class OrganizacionController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IOrganizacionRepository organizacionRepository;

        public OrganizacionController(
            IMapper mapper,
            UserManager<ApplicationUser> userManager,
            IOrganizacionRepository organizacionRepository
            )
        {
            this.mapper = mapper;
            this.userManager = userManager;
            this.organizacionRepository = organizacionRepository;
        }

        [HttpGet]
        [Authorize(Roles = "Supervisor")]
        public async Task<IActionResult> Get()
        {
            try
            {
                string organizacionId = User.GetOrganizacionId();
                FiltroGlobal filtro = new FiltroGlobal() { IncluirInactivos = false };
                var spec = new OrganizacionSpecification(filtro) { OrganizacionId = Guid.Parse(User.GetOrganizacionId()) };
                //spec.OrganizacionId = Guid.Parse(User.GetOrganizacionId());
                spec.IncludeStrings = new List<string> { };


                var result = await this.organizacionRepository.ListAsync(spec);



                if (result == null)
                {
                    return NotFound(result);
                }

                var dto = mapper.Map<List<OrganizacionDto>>(result);

                return Ok(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException.Message); // O devolver un BadRequest(400) si el error es de entrada
            }

        }
    }
}
