using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NET_API.Models.DTO.Auth;
using NET_API.Repositorys;

namespace NET_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Registor([FromBody] RegistorRequestDTO addRequestAuthDTO)
        {
            var identityUser = new IdentityUser
            {
                UserName = addRequestAuthDTO.Username,
                Email = addRequestAuthDTO.Username
            };
            var identityResult = await userManager.CreateAsync(identityUser, addRequestAuthDTO.Password);
            if (identityResult.Succeeded)
            {
                // Add roles to this User
                if (addRequestAuthDTO.Roles != null && addRequestAuthDTO.Roles.Any())
                {
                    identityResult = await userManager.AddToRolesAsync(identityUser, addRequestAuthDTO.Roles);
                    if (identityResult.Succeeded)
                    {
                        return Ok("User was registered! Please login.");
                    }
                }
            }
            return BadRequest("Something went wrong");
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDto)
        {
            var user = await userManager.FindByEmailAsync(loginRequestDto.Username);
            if (user != null)
            {
                var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);
                if (checkPasswordResult)
                {
                    // Get Roles for this user
                    var roles = await userManager.GetRolesAsync(user);
                    if (roles != null)
                    {
                        // Create Token
                        var jwtToken = tokenRepository.CreateJWTToken(user, roles.ToList());
                        var response = new LoginResponseDTO
                        {
                            JwtToken = jwtToken
                        };
                        return Ok(response);
                    }
                }
            }
            return BadRequest("Username or password incorrect");
        }
    }
}
