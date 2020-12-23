using System;
using System.Collections.Generic;
using System.Text;

namespace MyBookshelf.Core.Entities
{
    public class UserBook
    {
        public int Id { get; private set; }
        public User User { get; private set; }
        public Book Book { get; private set; }
        public Status Status { get; private set; }
        public DateTime ConclusionDate { get; private set; }
        public int Rating { get; private set; }

        public UserBook(User user, Book book, Status status)
        {
            User = user;
            Book = book;
            Status = status;
        }
        protected UserBook() { }
    }
}
