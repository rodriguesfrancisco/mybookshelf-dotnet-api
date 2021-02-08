using MediatR;
using MyBookshelf.Application.ViewModels;
using MyBookshelf.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyBookshelf.Application.Queries.GetUserById
{
    public class GetUserByIdHandler : IRequestHandler<GetUserById, UserViewModel>
    {
        private readonly IUserRepository _userRepository;
        public GetUserByIdHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<UserViewModel> Handle(GetUserById query, CancellationToken cancellationToken)
        {
            var user = _userRepository.GetById(query.Id);
            var userViewModel = new UserViewModel(user.Name, user.Email);

            return Task.FromResult(userViewModel);
        }
    }
}
