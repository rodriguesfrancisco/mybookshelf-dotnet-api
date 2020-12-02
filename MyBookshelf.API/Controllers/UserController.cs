using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyBookshelf.Application.Commands.CreateUser;
using MyBookshelf.Application.Queries.GetUserById;
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
        public void CreateNewUser([FromBody] CreateUser command)
        {
            _mediator.Send(command);
        }

        [HttpGet("{id}")]
        public UserViewModel GetUserById(int id)
        {
            var getUserByIdCommand = new GetUserById() { Id = id };
            var result = _mediator.Send(getUserByIdCommand);
            return result.Result;
        }
    }
}
