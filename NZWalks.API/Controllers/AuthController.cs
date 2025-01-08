using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
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

        //POST api/auth/register
        [HttpPost]
        [Route("register")]

        public async Task<IActionResult> Register([FromBody] RegisterRequestdto registerRequestdto)
        {
            var IdentityUser = new IdentityUser
            {
                UserName = registerRequestdto.Username,
                Email = registerRequestdto.Username
            };

            var identityResult = await userManager.CreateAsync(IdentityUser, registerRequestdto.Password);

            if (identityResult.Succeeded)
            {
                //ADD roles
                if (registerRequestdto.Roles != null && registerRequestdto.Roles.Any())
                {
                    identityResult = await userManager.AddToRolesAsync(IdentityUser, registerRequestdto.Roles);
                    if (identityResult.Succeeded)
                    {
                        return Ok("User was registered");
                    }
                }

            }
            return BadRequest("Something went wrong");
        }

        //POST: /a[i/auth/Login

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestdto loginRequestdto)
        {
            var user = await userManager.FindByEmailAsync(loginRequestdto.Username);
            if (user != null)
            {
                var checkPasswordResault = await userManager.CheckPasswordAsync(user, loginRequestdto.Password);
                if (checkPasswordResault)
                {
                    // Get roles
                    var roles = await userManager.GetRolesAsync(user);

                    if (roles != null)
                    {
                        // Create JWT token
                        var jwtToken = tokenRepository.CreateJWTToken(user, roles.ToList());

                        var response = new LoginReponseRequestDto
                        {
                            JwToken = jwtToken
                        };
                        return Ok(response);
                    }
                }
            }
                        return BadRequest("User has no roles");
                 
               
        }

    }
}
