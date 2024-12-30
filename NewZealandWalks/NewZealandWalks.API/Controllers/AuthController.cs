using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NewZealandWalks.API.Models.DTO;
using NewZealandWalks.API.Repositories;
using Serilog;
using System.Data;

namespace NewZealandWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenRepository _tokenRepository;
        //private readonly ILogger<AuthController> _logger;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            _userManager = userManager;
            _tokenRepository = tokenRepository;
            //_logger = logger;
        }

        /// <summary>
        /// POST: /API/Auth/Register
        /// Permet d'enregister un nouvel utilisateur dans notre systeme
        /// </summary>
        /// <param name="registerRequestDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.Username,
                Email = registerRequestDto.Username
            };

            var identityResult = await _userManager.CreateAsync(identityUser, registerRequestDto.Password);

            if (identityResult.Succeeded)
            {
                if (registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
                {
                    identityResult = await _userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);

                    if (identityResult.Succeeded)
                    {
                        return Ok("User was registered! Please login.");
                    }
                }
                else
                {
                    return Ok("User was registered! Please login.");
                }
            }

            Log.Error("User registration failed: {Errors}", string.Join(", ", identityResult.Errors.Select(e => e.Description)));
            return BadRequest("User registration failed.");
        }

        // POST: /api/Auth/Login
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(loginRequestDto.Username);

            if (user != null)
            {
                bool checkPasswordResult = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);

                if (checkPasswordResult)
                {
                    IList<string> roles = await _userManager.GetRolesAsync(user);

                    if (roles != null)
                    {
                        string jwtToken = _tokenRepository.CreateJWTToken(user, roles.ToList());

                        LoginResponseDto loginResponseDto = new LoginResponseDto
                        {
                            JwtToken = jwtToken
                        };

                        return Ok(loginResponseDto);
                    }
                }
            }

            Log.Warning("Login failed for user: {Username}", loginRequestDto.Username);
            return Unauthorized("Username or password incorrect.");
        }
    }
}