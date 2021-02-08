using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBookshelf.API.Extensions;
using MyBookshelf.Application.Commands.UpdateUserBookStatus;
using MyBookshelf.Application.Queries.GetUserBook;
using MyBookshelf.Application.Queries.ListUserBooks;
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
        public IActionResult ListUserBooks()
        {
            var userId = User.Identity.UserId();
            var command = new ListUserBooks() { UserId = userId.Value };
            return this.ProcessCommand(command, _mediator);
        }

        [HttpGet("{bookId}")]
        public IActionResult GetUserBookFromUserIdAndBookId(int bookId)
        {
            var userId = User.Identity.UserId();
            var command = new GetUserBook() { BookId = bookId, UserId = userId.Value };
            return this.ProcessCommand(command, _mediator);
        }

        [HttpPut]
        [Route("status")]
        public IActionResult UpdateStatusUserBook([FromBody] UpdateUserBookStatus command)
        {
            command.UserId = User.Identity.UserId().Value;
            return this.ProcessCommand(command, _mediator);
        }
    }
}
