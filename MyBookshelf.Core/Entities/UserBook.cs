using System;
using System.Collections.Generic;
using System.Text;

namespace MyBookshelf.Core.Entities
{
    public class UserBook
    {
        public int Id { get; set; }
        public User User { get; set; }
        public Book Book { get; set; }
        public Status Status { get; set; }
        public DateTime? ConclusionDate { get; set; }
        public int Rating { get; set; }
        public List<StatusHistory> StatusHistories { get; set; }

        public UserBook(User user, Book book, Status status)
        {
            User = user;
            Book = book;
            StatusHistories = new List<StatusHistory>();
            UpdateStatus(status);
        }

        public void UpdateStatus(Status newStatus)
        {
            Status = newStatus;
            StatusHistories.Add(new StatusHistory(this));
        }
        protected UserBook() 
        {
            StatusHistories = new List<StatusHistory>();
        }
    }
}
