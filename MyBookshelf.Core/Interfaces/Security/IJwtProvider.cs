using System;
using System.Collections.Generic;
using System.Text;

namespace MyBookshelf.Core.Interfaces.Security
{
    public interface IJwtProvider
    {
        string GenerateToken(string email);
    }
}
