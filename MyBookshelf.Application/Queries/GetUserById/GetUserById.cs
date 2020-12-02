using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBookshelf.Application.Queries.GetUserById
{
    public class GetUserById : IRequest<UserViewModel>
    {
        public int Id { get; set; }
    }
}
