using System;
using System.Collections.Generic;
using System.Text;

namespace MyBookshelf.Application.ViewModels
{
    public class UserBookViewModel
    {
        public int Id { get; set; }
        public UserViewModel User { get; set; }
        public BookViewModel Book { get; set; }
        public StatusViewModel Status { get; set; }
        public List<StatusHistoryViewModel> StatusHistories { get; set; }
        public DateTime? ConclusionDate { get; set; }
        public int Rating { get; set; }
    }
}
