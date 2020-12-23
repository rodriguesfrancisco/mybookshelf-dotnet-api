using AutoMapper;
using MediatR;
using MyBookshelf.Application.ViewModels;
using MyBookshelf.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyBookshelf.Application.Queries.GetUserBook
{
    public class GetUserBookHandler : IRequestHandler<GetUserBook, UserBookViewModel>
    {
        private readonly IUserBookRepository _userBookRepository;
        private readonly IMapper _mapper;

        public GetUserBookHandler(IUserBookRepository userBookRepository, IMapper mapper)
        {
            _userBookRepository = userBookRepository;
            _mapper = mapper;
        }

        public Task<UserBookViewModel> Handle(GetUserBook query, CancellationToken cancellationToken)
        {
            var userBook = _userBookRepository.FindByUserIdAndBookId(query.UserId, query.BookId);
            var userBookViewModel = _mapper.Map<UserBookViewModel>(userBook);

            return Task.FromResult(userBookViewModel);
        }
    }
}
