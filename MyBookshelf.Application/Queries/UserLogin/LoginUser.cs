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
        public string Senha { get; set; }
        public override void Validate()
        {
            AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrEmpty(Email, "Email", "Email deve ser preenchido.")
                .HasMinLen(Senha, 6, "Senha", "Senha deve ter um mínimo de 6 caracteres.")
            );
        }
    }
}
