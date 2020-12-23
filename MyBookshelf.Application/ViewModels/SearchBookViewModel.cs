using MyBookshelf.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBookshelf.Application.ViewModels
{
    public class SearchBookViewModel
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Description { get; set; }
        public IList<string> Authors { get; set; }
        public string Publisher { get; set; }
        public IList<ISBNViewModel> IndustryIdentifiers { get; set; }
        public int PageCount { get; set; }
        public IList<string> Categories { get; set; }
        public BookImageLinksViewModel ImageLinks { get; set; }
    }
    
}
