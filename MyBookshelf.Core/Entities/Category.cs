using System;
using System.Collections.Generic;
using System.Text;

namespace MyBookshelf.Core.Entities
{
    public class Category
    {
        public int? Id { get; private set; }
        public string Name { get; private set; }
        public Category(string name)
        {
            Name = name;
        }
        protected Category() { }
    }
}
