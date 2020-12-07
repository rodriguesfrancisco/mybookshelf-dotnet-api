using MediatR;
using MyBookshelf.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyBookshelf.Application.Queries.SearchBook
{
    public class SearchBookHandler : IRequestHandler<SearchBook, IList<object>>
    {
        private readonly IBookRepository _bookRepository;

        public SearchBookHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IList<object>> Handle(SearchBook query, CancellationToken cancellationToken)
        {
            var result = await _bookRepository.Search(query.SearchTerm);

            return result;
        }
    }
}
