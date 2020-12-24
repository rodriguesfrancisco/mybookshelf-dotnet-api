using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBookshelf.API.Extensions;
using MyBookshelf.Application.Commands.UpdateUserBookStatus;
using MyBookshelf.Application.Queries.GetUserBook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBookshelf.API.Controllers
{
    [Route("api/user-books")]
    [Authorize]
    public class UserBookController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserBookController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public IActionResult GetUserBookFromUserIdAndBookId([FromQuery]int bookId)
        {
            var userId = HttpContext.User.Identity.UserId();
            var command = new GetUserBook() { BookId = bookId, UserId = userId.Value };
            return this.ProcessCommand(command, _mediator);
        }

        [HttpPut]
        [Route("status")]
        public IActionResult UpdateStatusUserBook([FromBody] UpdateUserBookStatus command)
        {
            command.UserId = HttpContext.User.Identity.UserId().Value;
            return this.ProcessCommand(command, _mediator);
        }
    }
}
