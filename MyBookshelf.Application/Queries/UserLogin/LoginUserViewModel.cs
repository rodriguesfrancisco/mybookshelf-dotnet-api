using System;
using System.Collections.Generic;
using System.Text;

namespace MyBookshelf.Application.Queries.UserLogin
{
    public class LoginUserViewModel
    {
        public string Email { get; private set; }
        public string Token { get; private set; }

        public LoginUserViewModel(string email, string token)
        {
            Email = email;
            Token = token;
        }
    }
}
