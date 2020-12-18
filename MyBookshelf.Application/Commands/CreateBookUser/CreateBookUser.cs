using Flunt.Validations;
using MyBookshelf.Application.Queries.SearchBook;
using MyBookshelf.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBookshelf.Application.Commands.CreateBookUser
{
    public class CreateBookUser : Command
    {
        public int? UserId { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Description { get; set; }
        public IList<string> Authors { get; set; }
        public string Publisher { get; set; }
        public IList<ISBNViewModel> IndustryIdentifiers { get; set; }
        public int PageCount { get; set; }
        public IList<string> Categories { get; set; }
        public BookImageLinksViewModel ImageLinks { get; set; }
        public int IdStatus { get; set; }
        public override void Validate()
        {
            AddNotifications(new Contract()
                .Requires()
                .IsNotNull(UserId, "UserId", "User should be logged in.")
            );
        }
    }
}
