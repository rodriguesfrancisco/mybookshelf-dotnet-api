using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyBookshelf.API.Extensions;
using MyBookshelf.Application.Commands.CreateUser;
using MyBookshelf.Application.Queries.GetUserById;
using MyBookshelf.Application.Queries.UserLogin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBookshelf.API.Controllers
{
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public IActionResult CreateNewUser([FromBody] CreateUser command)
        {
            return this.ProcessCommand(command, _mediator);
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var getUserByIdCommand = new GetUserById() { Id = id };
            return this.ProcessCommand(getUserByIdCommand, _mediator);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginUser command)
        {
            return this.ProcessCommand(command, _mediator);
        }
    }
}
