using Flunt.Validations;
using MyBookshelf.Application.Commands;
using MyBookshelf.Core.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBookshelf.Application.Queries.SearchBook
{
    public class SearchBook : Command<PagedList<object>>
    {
        public SearchBook(string searchTerm, int page = 0)
        {
            SearchTerm = searchTerm;
            Page = page;
        }

        public string SearchTerm { get; private set; }
        public int Page { get; private set; }

        public override void Validate()
        {
            AddNotifications(new Contract()
                .IsNotNullOrEmpty(SearchTerm, "SearchTerm", "O termo de busca deve ser preenchido.")
            );
        }
    }
}
