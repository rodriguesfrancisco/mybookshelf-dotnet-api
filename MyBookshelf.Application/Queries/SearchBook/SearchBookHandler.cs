using MediatR;
using MyBookshelf.Core.Interfaces.Repositories;
using MyBookshelf.Core.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyBookshelf.Application.Queries.SearchBook
{
    public class SearchBookHandler : IRequestHandler<SearchBook, PagedList<object>>
    {
        private readonly IBookRepository _bookRepository;
        private readonly int PAGE_SIZE = 20;

        public SearchBookHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<PagedList<object>> Handle(SearchBook query, CancellationToken cancellationToken)
        {
            var result = await _bookRepository.Search(query.SearchTerm, query.Page, PAGE_SIZE);

            return result;
        }
    }
}
