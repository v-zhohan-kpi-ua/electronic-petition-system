using DigitalPetitions.Features.Auth.Dto;
using DigitalPetitions.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DigitalPetitions.Features.Auth
{
    [Route("auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator) 
        {
            _mediator = mediator;
        }
        
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login([FromBody] LoginCommand command)
        {
            var user = await _mediator.Send(command);

            if (user == null)
            {
                return BadRequest();
            }

            var claims = new List<Claim>
            {
                new Claim(type: ClaimTypes.NameIdentifier, value: $"{user.Id}"),
                new Claim(type: ClaimTypes.Email, value: user.Email),
                new Claim(type: ClaimTypes.Role, value: user.Role.ToString())
            };

            Console.WriteLine($"LOGIN ROLE: {user.Role.ToString()}");

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(identity)
            );

            return user;
        }

        [Authorize]
        [HttpGet("logout")]
        public async Task Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
