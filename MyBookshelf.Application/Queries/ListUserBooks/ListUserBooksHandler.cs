using AutoMapper;
using MediatR;
using MyBookshelf.Application.ViewModels;
using MyBookshelf.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyBookshelf.Application.Queries.ListUserBooks
{
    public class ListUserBooksHandler : IRequestHandler<ListUserBooks, List<UserBookResumedViewModel>>
    {
        private readonly IUserBookRepository _userBookRepository;
        private readonly IMapper _mapper;

        public ListUserBooksHandler(IUserBookRepository userBookRepository, IMapper mapper)
        {
            _userBookRepository = userBookRepository;
            _mapper = mapper;
        }

        public Task<List<UserBookResumedViewModel>> Handle(ListUserBooks query, CancellationToken cancellationToken)
        {
            var userBooks = _userBookRepository.ListByUserId(query.UserId);
            var userBooksMapped = _mapper.Map<List<UserBookResumedViewModel>>(userBooks);

            return Task.FromResult(userBooksMapped);
        }
    }
}
