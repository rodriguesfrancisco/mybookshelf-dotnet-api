using MyBookshelf.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBookshelf.Core.Interfaces.Repositories
{
    public interface IUserRepository
    {
        User GetById(int id);
        void Add(User user);
        User LoginUser(string email, string password);
        bool EmailExists(string email);
    }
}
