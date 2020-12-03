using Flunt.Notifications;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBookshelf.Application.Commands
{
    public abstract class Command : Notifiable, IRequest
    {
        public abstract void Validate();
    }
    public abstract class Command<TResponse> : Notifiable, IRequest<TResponse>
    {
        public abstract void Validate();
    }
}
