using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBookshelf.API.Extensions;
using MyBookshelf.Application.Commands.CreateUser;
using MyBookshelf.Application.Queries.CheckUser;
using MyBookshelf.Application.Queries.GetUserById;
using MyBookshelf.Application.Queries.UserLogin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBookshelf.API.Controllers
{
    [Route("api/users")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult CreateNewUser([FromBody] CreateUser command)
        {
            return this.ProcessCommand(command, _mediator);
        }

        [HttpGet("check")]
        public IActionResult CheckUser()
        {
            var userId = User.Identity.UserId().Value;
            var checkUser = new CheckUser() { UserId = userId };
            return this.ProcessCommand(checkUser, _mediator);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginUser command)
        {
            var result = _mediator.Send(command);
            if(command.Invalid)
            {
                return BadRequest(command.Notifications);
            }
            var cookieOptions = new CookieOptions()
            {
                Expires = DateTimeOffset.UtcNow.AddDays(7),
                HttpOnly = true
            };

            Request.HttpContext.Response.Cookies.Append("tid", result.Result.Token, cookieOptions);

            return Ok();
        }
        
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Request.HttpContext.Response.Cookies.Delete("tid");

            return Ok();
        }
    }
}
