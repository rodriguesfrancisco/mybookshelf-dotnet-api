using System;
using System.Collections.Generic;
using System.Text;

namespace MyBookshelf.Core.Entities
{
    public class StatusHistory
    {
        public int Id { get; set; }
        public UserBook UserBook { get; set; }
        public Status Status { get; set; }
        public DateTime Date { get; set; }
        public StatusHistory(UserBook userBook, Status status)
        {
            UserBook = userBook;
            Status = status;
            Date = DateTime.Now;
        }

        protected StatusHistory() { }
    }
}
