﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MyBookshelf.Core.Entities
{
    public class UserBook
    {
        public int UserId { get; private set; }
        public int BookId { get; private set; }
        public UserBook(int userId, int bookId)
        {
            UserId = userId;
            BookId = bookId;
        }
    }
}