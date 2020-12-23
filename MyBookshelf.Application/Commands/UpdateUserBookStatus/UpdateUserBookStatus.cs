using System;
using System.Collections.Generic;
using System.Text;

namespace MyBookshelf.Application.Commands.UpdateUserBookStatus
{
    public class UpdateUserBookStatus : Command
    {
        public int UserId { get; set; }
        public int StatusId { get; set; }
        public override void Validate()
        {
            
        }
    }
}
