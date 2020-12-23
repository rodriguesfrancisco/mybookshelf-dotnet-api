using MediatR;
using MyBookshelf.Application.Commands;
using MyBookshelf.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyBookshelf.Application.Queries.GetUserBook
{
    public class GetUserBook : Command<UserBookViewModel>
    {
        public int UserId { get; set; }
        public int BookId { get; set; }
        public override void Validate()
        {

        }
    }
}
