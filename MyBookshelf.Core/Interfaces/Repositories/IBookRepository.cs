using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyBookshelf.Core.Interfaces.Repositories
{
    public interface IBookRepository
    {
        Task<IList<object>> Search(string searchTerm);
    }
}
