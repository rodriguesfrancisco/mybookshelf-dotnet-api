using Flunt.Validations;
using MyBookshelf.Application.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBookshelf.Application.Queries.SearchBook
{
    public class SearchBook : Command<IList<object>>
    {
        public SearchBook(string searchTerm)
        {
            SearchTerm = searchTerm;
        }

        public string SearchTerm { get; private set; }

        public override void Validate()
        {
            AddNotifications(new Contract()
                .IsNotNullOrEmpty(SearchTerm, "SearchTerm", "O termo de busca deve ser preenchido.")
            );
        }
    }
}
