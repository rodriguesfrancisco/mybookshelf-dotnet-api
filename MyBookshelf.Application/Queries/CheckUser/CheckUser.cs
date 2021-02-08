using Flunt.Validations;
using MyBookshelf.Application.Commands;
using MyBookshelf.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBookshelf.Application.Queries.CheckUser
{
    public class CheckUser : Command<UserViewModel>
    {
        public int UserId { get; set; }

        public override void Validate()
        {
            AddNotifications(new Contract()
                .Requires()
                .IsNotNull(UserId, "User", "User not logged in")
            );
        }
    }
}
