using Application.Features.Authorizations.Commands.Register;
using Application.Features.Authorizations.Dtos;
using Core.Security.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthsController : ControllerBase
    {
        IMediator _mediator;

        public AuthsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserForRegisterDto userForRegisterDto)
        {
            RegisterCommand registerCommand = new RegisterCommand
            {
                UserForRegisterDto = userForRegisterDto
            };

            RegisteredDto registeredDto = await _mediator.Send(registerCommand);
            return Created("",registeredDto.AccessToken);
        }
    }
}

