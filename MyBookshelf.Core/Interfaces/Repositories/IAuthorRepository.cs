using MyBookshelf.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBookshelf.Core.Interfaces.Repositories
{
    public interface IAuthorRepository
    {
        Author FindByName(string name);
    }
}
