using System;
using System.Collections.Generic;
using System.Text;

namespace MyBookshelf.Application.Queries.GetUserById
{
    public class UserViewModel
    {
        public string Nome { get; private set; }
        public string Email { get; private set; }

        public UserViewModel(string nome, string email)
        {
            Nome = nome;
            Email = email;
        }
    }
}
