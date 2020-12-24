using Flunt.Validations;
using MediatR;
using MyBookshelf.Core.Entities;
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
        private readonly IUserBookRepository _userBookRepository;

        public UpdateUserBookStatusHandler(
            IUserRepository userRepository,
            IStatusRepository statusRepository,
            IUserBookRepository userBookRepository)
        {
            _userRepository = userRepository;
            _statusRepository = statusRepository;
            _userBookRepository = userBookRepository;
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

            var userBook = _userBookRepository.FindByUserIdAndBookId(user.Id, command.BookId);
            userBook.UpdateStatus(status);
            _userBookRepository.Update(userBook);

            return Task.FromResult(Unit.Value);
        }
    }
}
