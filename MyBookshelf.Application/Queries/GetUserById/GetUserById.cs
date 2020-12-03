using MediatR;
using MyBookshelf.Application.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBookshelf.Application.Queries.GetUserById
{
    public class GetUserById : Command<UserViewModel>
    {
        public int Id { get; set; }

        public override void Validate()
        {
            
        }
    }
}
