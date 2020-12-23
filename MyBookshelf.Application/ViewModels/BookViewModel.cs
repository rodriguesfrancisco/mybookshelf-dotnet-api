using System;
using System.Collections.Generic;
using System.Text;

namespace MyBookshelf.Application.ViewModels
{
    public class BookViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Publisher { get; set; }
        public string ISBN { get; set; }
        public int PageCount { get; set; }
        public string Thumbnail { get; set; }
        public string SmallThumbnail { get; set; }
        public string Description { get; set; }
        public List<CategoryViewModel> Categories { get; set; }
        public List<AuthorViewModel> Authors { get; set; }
    }
}
