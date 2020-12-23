using System;
using System.Collections.Generic;
using System.Text;

namespace MyBookshelf.Core.Entities
{
    public class Status
    {
        public int Id { get; private set; }
        public string Description { get; private set; }
        public Status(string description)
        {
            Description = description;
        }
        protected Status() { }
    }
}
