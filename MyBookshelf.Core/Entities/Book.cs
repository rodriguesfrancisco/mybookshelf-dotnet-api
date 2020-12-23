using System;
using System.Collections.Generic;
using System.Text;

namespace MyBookshelf.Core.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; private set; }
        public string Subtitle { get; private set; }
        public string Publisher { get; private set; }
        public string ISBN { get; private set; }
        public int PageCount { get; private set; }
        public string Thumbnail { get; private set; }
        public string SmallThumbnail { get; private set; }
        public string Description { get; private set; }
        public List<Category> Categories { get; private set; }
        public List<Author> Authors { get; private set; }

        public Book(
            string title,
            string subtitle,
            string publisher,
            string isbn,
            int pageCount,
            string thumbnail,
            string smallThumbnail, 
            string description)
        {
            Title = title;
            Subtitle = subtitle;
            Publisher = publisher;
            ISBN = isbn;
            PageCount = pageCount;
            Thumbnail = thumbnail;
            SmallThumbnail = smallThumbnail;
            Description = description;
            Categories = new List<Category>();
            Authors = new List<Author>();
        }
        protected Book() 
        {
            Authors = new List<Author>();
            Categories = new List<Category>();
        }

        public void AddAuthor(Author author)
        {            
            Authors.Add(author);            
        }

        public void AddCategory(Category category)
        {
            Categories.Add(category);
        }
    }
}
