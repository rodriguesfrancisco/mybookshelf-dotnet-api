using MyBookshelf.Core.Entities;
using MyBookshelf.Core.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyBookshelf.Core.Interfaces.Repositories
{
    public interface IBookRepository
    {
        Task<PagedList<object>> Search(string searchTerm, int page, int pageSize);
        Book FindByIsbn(string isbn);
        int Save(Book book);
    }
}
