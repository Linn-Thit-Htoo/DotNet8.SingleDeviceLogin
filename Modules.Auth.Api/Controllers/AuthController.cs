using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modules.Auth.Application.Features.Auth.GetUserList;
using Modules.Auth.Application.Features.Auth.Login;
using Modules.Auth.Application.Features.Auth.Register;
using Modules.Auth.Domain.Interfaces;
using Shared.DTOs.Features.Auth;

namespace Modules.Auth.Api.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;
        private readonly IMediator _mediator;

        public AuthController(IAuthService authService, IMediator mediator)
        {
            _authService = authService;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserList()
        {
            var query = new GetUserListQuery();
            var result = await _mediator.Send(query);

            return Content(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestModel requestModel)
        {
            var command = new RegisterCommand(requestModel);
            var result = await _mediator.Send(command);

            return Content(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestModel requestModel)
        {
            var query = new LoginQuery(requestModel);
            var result = await _mediator.Send(query);

            return Content(result);
        }
    }
}
