using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBookshelf.API.Extensions;
using MyBookshelf.Application.Commands.CreateBookUser;
using MyBookshelf.Application.Queries.SearchBook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBookshelf.API.Controllers
{
    [Route("api/books")]
    [Authorize]
    public class BooksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BooksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public IActionResult SearchBook([FromQuery] string q, [FromQuery] int page)
        {
            var searchBookQuery = new SearchBook(q, page);
            return this.ProcessCommand(searchBookQuery, _mediator);
        }

        [HttpPost]
        public IActionResult InsertBook([FromBody] CreateBookUser command)
        {
            command.UserId = HttpContext.User.Identity.UserId();
            return this.ProcessCommand(command, _mediator);
        }
    }
}
