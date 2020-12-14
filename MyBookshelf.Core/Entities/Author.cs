using System;
using System.Collections.Generic;
using System.Text;

namespace MyBookshelf.Core.Entities
{
    public class Author
    {
        public int? Id { get; private set; }
        public string Name { get; private set; }
        public Author(string name)
        {
            Name = name;
        }

        protected Author() { }
    }
}
