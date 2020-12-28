using MyBookshelf.Application.Commands;
using MyBookshelf.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBookshelf.Application.Queries.ListUserBooks
{
    public class ListUserBooks : Command<List<UserBookResumedViewModel>>
    {
        public int UserId { get; set; }
        public override void Validate()
        {
            
        }
    }
}
