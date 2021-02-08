using Flunt.Validations;
using MediatR;
using MyBookshelf.Application.ViewModels;
using MyBookshelf.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyBookshelf.Application.Queries.CheckUser
{
    public class CheckUserHandler : IRequestHandler<CheckUser, UserViewModel>
    {
        private readonly IUserRepository _userRepository;

        public CheckUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<UserViewModel> Handle(CheckUser request, CancellationToken cancellationToken)
        {
            var user = _userRepository.GetById(request.UserId);

            request.AddNotifications(new Contract()
                .Requires()
                .IsNotNull(user, "User", "User not found")
            );

            if (request.Invalid) return Task.FromResult<UserViewModel>(null);

            return Task.FromResult(new UserViewModel(user.Name, user.Email));
        }
    }
}
