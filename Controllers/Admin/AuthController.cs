using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using tickets.api.Models;
using tickets.api.Models.DTO.Admin.Auth;
using tickets.api.Repositories.Interface;

namespace tickets.api.Controllers.Admin
{
    [Route("api/administrador/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ITokenRepository tokenRepository
            )
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.tokenRepository = tokenRepository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
        {
            var user = await userManager.FindByNameAsync(model.Username);
            if (user == null)
                return Unauthorized("Usuario o contraseña incorrectos.");

            var result = await signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (!result.Succeeded)
                return Unauthorized("Usuario o contraseña incorrectos.");

            var roles = await userManager.GetRolesAsync(user);
            if (roles.IndexOf("Administrador") == -1)
            {
                ModelState.AddModelError("error", "Email o password incorrecto.");
                return ValidationProblem(ModelState);
            }

            var jwtToken = tokenRepository.CreateJwtToken(user, roles.ToList());
            var response = new LoginResponseDto()
            {
                Email = user.Email,
                Roles = roles.ToList(),
                Token = jwtToken,
                Nombre = user.Nombre,
                Apellidos = user.Apellidos,
                Username = user.UserName
            };


            return Ok(response);
        }
    }
}
