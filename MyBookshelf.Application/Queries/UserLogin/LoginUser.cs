using Flunt.Validations;
using MyBookshelf.Application.Commands;
using MyBookshelf.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBookshelf.Application.Queries.UserLogin
{
    public class LoginUser : Command<LoginUserViewModel>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public override void Validate()
        {
            AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrEmpty(Email, "Email", "Email must not be empty")
                .HasMinLen(Password, 6, "Password", "Password must have a minimun of 6 characters")
            );
        }
    }
}
