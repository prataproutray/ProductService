using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductService.core.application.command;
using ProductService.core.application.query;
using ProductService.core.domain;
using ZstdSharp.Unsafe;

namespace ProductService.presentation.WebApi
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand createUserCommand)
        {
            var resp = await _mediator.Send(createUserCommand);
            return Ok(resp);
        }
        [HttpGet("{userid}")]
        public async Task<ActionResult<User>> GetUserById(int userid)
        {
            var query = new GetUserById() {userid= userid};
            var resp = await _mediator.Send(query);
            return Ok(resp);
        }
        [HttpGet]
        public async Task<ActionResult<List<User>>> GetAllUser()
        {
            var getAllUser = new GetAllUser() { };
            var resp = await _mediator.Send(getAllUser);
            return Ok();
        }
    }
}
