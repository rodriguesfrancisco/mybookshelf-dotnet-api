using Flunt.Validations;
using MediatR;
using MyBookshelf.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyBookshelf.Application.Commands.UpdateUserBookStatus
{
    public class UpdateUserBookStatusHandler : IRequestHandler<UpdateUserBookStatus>
    {
        private readonly IUserRepository _userRepository;
        private readonly IStatusRepository _statusRepository;

        public UpdateUserBookStatusHandler(IUserRepository userRepository, IStatusRepository statusRepository)
        {
            _userRepository = userRepository;
            _statusRepository = statusRepository;
        }

        public Task<Unit> Handle(UpdateUserBookStatus command, CancellationToken cancellationToken)
        {
            var user = _userRepository.GetById(command.UserId);

            command.AddNotifications(new Contract()
                .Requires()
                .IsNotNull(user, "User", "User not found")
            );

            var status = _statusRepository.FindById(command.StatusId);

            command.AddNotifications(new Contract()
                .Requires()
                .IsNotNull(status, "Status", "Status not found")
            );

            return Task.FromResult(Unit.Value);
        }
    }
}
