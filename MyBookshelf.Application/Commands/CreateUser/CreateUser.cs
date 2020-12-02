using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBookshelf.Application.Commands.CreateUser
{
    public class CreateUser : IRequest
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
