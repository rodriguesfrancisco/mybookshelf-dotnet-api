﻿using Flunt.Validations;
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
            VerifyIfEmailExists(command);
            if(command.Invalid) return Task.FromResult(Unit.Value);

            var user = new User(
                command.Name, 
                command.Email, 
                LoginService.ComputeSha256Hash(command.Password)
            );
            _userRepository.Add(user);
            return Task.FromResult(Unit.Value);
        }

        private void VerifyIfEmailExists(CreateUser command)
        {
            var emailExists = _userRepository.EmailExists(command.Email);
            command.AddNotifications(new Contract()
                .Requires()
                .IsFalse(emailExists, "Email", "Email already exists.")
            );
        }
    }
}
