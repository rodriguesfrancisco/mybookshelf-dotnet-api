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
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

        public override void Validate()
        {
            AddNotifications(new Contract()
                .IsNotNullOrEmpty(Nome, "Nome", "Nome deve ser preenchido.")
                .IsNotNullOrEmpty(Email, "Email", "Email deve ser preenchido.")
                .IsNotNullOrEmpty(Senha, "Senha", "Senha deve ser preenchido.")
                .HasMinLen(Senha, 6, "Senha", "Senha deve ter no mínimo 6 caracteres.")
            );
        }
    }
}
