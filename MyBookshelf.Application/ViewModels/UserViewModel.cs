using System;
using System.Collections.Generic;
using System.Text;

namespace MyBookshelf.Application.ViewModels
{
    public class UserViewModel
    {
        public string Name { get; private set; }
        public string Email { get; private set; }

        public UserViewModel(string name, string email)
        {
            Name = name;
            Email = email;
        }
    }
}
