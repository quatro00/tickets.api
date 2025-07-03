using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using tickets.api.Helpers;
using tickets.api.Models;
using tickets.api.Models.Domain;
using tickets.api.Models.DTO.Area;
using tickets.api.Models.DTO.Usuario;
using tickets.api.Models.Specifications;
using tickets.api.Repositories.Interface;

namespace tickets.api.Controllers.Admin
{
    [Route("api/administrador/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;
        private readonly IAspNetUsersRepository aspNetUsersRepository;

        public UsuarioController(
            UserManager<ApplicationUser> userManager,
            IMapper mapper,
            IAspNetUsersRepository aspNetUsersRepository
            )
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.aspNetUsersRepository = aspNetUsersRepository;
        }
        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Crear([FromForm] CrearUsuarioDto model)
        {
            // Validar si el modelo es válido
            if (!ModelState.IsValid)
            {
                User.GetId();
                return BadRequest("Modelo de datos invalido.");
            }

            try
            {
                var colaborador = await this.userManager.FindByEmailAsync(model.Correo);
                if (colaborador != null)
                    return NotFound("El correo ya se encuentra registrado.");

                var usuarioExistente = await this.userManager.FindByNameAsync(model.Usuario);
                if (usuarioExistente != null)
                    return BadRequest("EL nombre de usuario ya se encuentra registrado.");


                string? avatar = null;
                string? pathCompleto = null;
                string? rutaPublica = null;

                if (model.Avatar != null)
                {
                    avatar = Guid.NewGuid().ToString();
                    var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot","img", "avatar");
                    var ext = Path.GetExtension(model.Avatar.FileName);
                    avatar = avatar + ext;
                    pathCompleto = Path.Combine(uploadPath, avatar);
                    using var stream = new FileStream(pathCompleto, FileMode.Create);
                    await model.Avatar.CopyToAsync(stream);
                    rutaPublica = $"{Request.Scheme}://{Request.Host}/img/avatar/{avatar}";
                }
                ApplicationUser usuario = new ApplicationUser
                {
                    UserName = model.Usuario,
                    Email = model.Correo,
                    NormalizedEmail = model.Correo.ToUpper(),
                    NormalizedUserName = model.Usuario.ToUpper(),
                    Nombre = model.Nombre,
                    Apellidos = model.Apellidos,
                    OrganizacionId = model.OrganizacionId,
                    Avatar = rutaPublica,
                };

                var resultado = await this.userManager.CreateAsync(usuario, model.Password);

                if (!resultado.Succeeded)
                    return BadRequest(resultado.Errors);

                await this.userManager.AddToRoleAsync(usuario, model.Rol);
                /*
                var dto = mapper.Map<asp>(model);
                dto.UsuarioCreacion = Guid.Parse(User.GetId());
                // Agregar el paciente al repositorio
                await this.aspNetUsersRepository.AddAsync(dto);
                */
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
                var spec = new AspNetUsersSpecification(filtro);
                spec.IncludeStrings = new List<string> { "Organizacion", "Roles" };

                var result = await this.aspNetUsersRepository.ListAsync(spec);
                if (result == null)
                {
                    return NotFound(result);
                }
                result = result.Where(x => x.Roles.Any(y => y.SistemaId == 3)).ToList();
                var dto = mapper.Map<List<GetUsuariosDto>>(result);

                return Ok(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException.Message); // O devolver un BadRequest(400) si el error es de entrada
            }

        }

        [HttpGet("GetResponsables")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> GetResponsables()
        {
            try
            {
                FiltroGlobal filtro = new FiltroGlobal() { IncluirInactivos = true };
                var spec = new AspNetUsersSpecification(filtro);
                spec.IncludeStrings = new List<string> { "Organizacion", "Roles" };
                spec.Rol = "Responsable de area";

                var result = await this.aspNetUsersRepository.ListAsync(spec);
                if (result == null)
                {
                    return NotFound(result);
                }

                var dto = mapper.Map<List<GetUsuariosDto>>(result);

                return Ok(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException.Message); // O devolver un BadRequest(400) si el error es de entrada
            }

        }

        [HttpGet("GetSupervisores")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> GetSupervisores()
        {
            try
            {
                FiltroGlobal filtro = new FiltroGlobal() { IncluirInactivos = true };
                var spec = new AspNetUsersSpecification(filtro);
                spec.IncludeStrings = new List<string> { "Organizacion", "Roles" };
                spec.Rol = "Supervisor";

                var result = await this.aspNetUsersRepository.ListAsync(spec);
                if (result == null)
                {
                    return NotFound(result);
                }

                var dto = mapper.Map<List<GetUsuariosDto>>(result);

                return Ok(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException.Message); // O devolver un BadRequest(400) si el error es de entrada
            }

        }

        //[Authorize(Roles = "Administrador")]
        [HttpPut("{id}/desactivar")]
        public async Task<IActionResult> Desactivar(string id)
        {
            try
            {
                // Obtener el paciente actual desde la base de datos
                //UpdateContactoDto dto;
                var usuarios = await this.aspNetUsersRepository.ListAsync();
                var usuario = usuarios.Where(x => x.Id == id).FirstOrDefault();
                if (usuario == null)
                {
                    return NotFound("Dato no encontrado.");
                }

                // Solo actualizamos el campo 'Activo' a false
                usuario.Activo = false;
                // Guardamos los cambios

                await this.aspNetUsersRepository.UpdateAsync(usuario);

                return NoContent(); // Respuesta exitosa sin contenido
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrio un error"); // O devolver un BadRequest(400) si el error es de entrada
            }

        }

        //[Authorize(Roles = "Administrador")]
        [HttpPut("{id}/activar")]
        public async Task<IActionResult> Activar(string id)
        {
            try
            {
                // Obtener el paciente actual desde la base de datos
                //UpdateContactoDto dto;
                var usuarios = await this.aspNetUsersRepository.ListAsync();
                var usuario = usuarios.Where(x=>x.Id == id).FirstOrDefault();
                if (usuario == null)
                {
                    return NotFound("Dato no encontrado.");
                }

                // Solo actualizamos el campo 'Activo' a false
                usuario.Activo = true;
                // Guardamos los cambios

                await this.aspNetUsersRepository.UpdateAsync(usuario);

                return NoContent(); // Respuesta exitosa sin contenido
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrio un error"); // O devolver un BadRequest(400) si el error es de entrada
            }

        }
    }
}
