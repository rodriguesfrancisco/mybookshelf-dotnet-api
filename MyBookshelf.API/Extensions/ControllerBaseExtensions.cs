using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyBookshelf.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBookshelf.API.Extensions
{
    public static class ControllerBaseExtensions
    {
        public static IActionResult ProcessCommand(this ControllerBase controller, Command command, IMediator mediator)
        {
            command.Validate();
            if (command.Invalid)
            {
                return controller.BadRequest(command.Notifications);
            }
            else
            {
                mediator.Send(command);
                return controller.Ok();
            }
        }

        public static IActionResult ProcessCommand<TResponse>(this ControllerBase controller, Command<TResponse> command, IMediator mediator)
        {
            command.Validate();
            if (command.Invalid)
            {
                return controller.BadRequest(command.Notifications);
            }
            else
            {
                var result = mediator.Send(command);
                return controller.Ok(result.Result);
            }
        }
    }
}
