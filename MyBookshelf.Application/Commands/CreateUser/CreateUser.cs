using Flunt.Notifications;
using Flunt.Validations;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBookshelf.Application.Commands.CreateUser
{
    public class CreateUser : Command
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public override void Validate()
        {
            AddNotifications(new Contract()
                .IsNotNullOrEmpty(Name, "Name", "Name must not be empty")
                .IsNotNullOrEmpty(Email, "Email", "Email must not be empty")
                .IsNotNullOrEmpty(Password, "Password", "Password must not be empty")
                .HasMinLen(Password, 6, "Password", "Password must have a minimum of 6 characters")
            );
        }
    }
}
