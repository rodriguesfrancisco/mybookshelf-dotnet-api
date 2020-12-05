using MediatR;
using MyBookshelf.Application.Queries.UserLogin;
using MyBookshelf.Core.Entities;
using MyBookshelf.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyBookshelf.Application.Commands.CreateUser
{
    public class CreateUserHandler : IRequestHandler<CreateUser>
    {
        private readonly IUserRepository _userRepository;
        public CreateUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public Task<Unit> Handle(CreateUser command, CancellationToken cancellationToken)
        {
            var user = new User(
                command.Nome, 
                command.Email, 
                LoginService.ComputeSha256Hash(command.Senha)
            );
            _userRepository.Add(user);
            return Task.FromResult(Unit.Value);
        }
    }
}
