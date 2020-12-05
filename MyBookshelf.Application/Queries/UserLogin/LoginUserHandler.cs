using Flunt.Validations;
using MediatR;
using MyBookshelf.Core.Interfaces.Repositories;
using MyBookshelf.Core.Interfaces.Security;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyBookshelf.Application.Queries.UserLogin
{
    public class LoginUserHandler : IRequestHandler<LoginUser, LoginUserViewModel>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtProvider _jwtProvider;

        public LoginUserHandler(IUserRepository userRepository, IJwtProvider jwtProvider)
        {
            _userRepository = userRepository;
            _jwtProvider = jwtProvider;
        }

        public Task<LoginUserViewModel> Handle(LoginUser command, CancellationToken cancellationToken)
        {
            var encryptedPassword = LoginService.ComputeSha256Hash(command.Senha);

            var user = _userRepository.LoginUser(command.Email, encryptedPassword);
            command.AddNotifications(new Contract()
                .Requires()
                .IsNotNull(user, "User", "Email ou senha inválido")
            );

            if (command.Invalid) return Task.FromResult<LoginUserViewModel>(null);

            return Task.FromResult(new LoginUserViewModel(user.Email, _jwtProvider.GenerateToken(user.Email, user.Id)));
        }
    }
}
