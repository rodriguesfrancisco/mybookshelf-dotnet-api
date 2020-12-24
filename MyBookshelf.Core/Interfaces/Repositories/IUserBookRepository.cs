using MyBookshelf.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBookshelf.Core.Interfaces.Repositories
{
    public interface IUserBookRepository
    {
        int Save(UserBook userBook);
        UserBook FindByUserIdAndBookId(int userId, int bookId);
        void Update(UserBook userBook);
    }
}
