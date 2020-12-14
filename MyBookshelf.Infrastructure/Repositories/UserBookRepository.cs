using Dapper;
using Microsoft.Extensions.Configuration;
using MyBookshelf.Core.Entities;
using MyBookshelf.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace MyBookshelf.Infrastructure.Repositories
{
    public class UserBookRepository : IUserBookRepository
    {
        private readonly IConfiguration _configuration;

        public UserBookRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Save(UserBook userBook)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                var sql = "INSERT INTO BookUser(IdUser, IdBook) VALUES(@IdUser, @IdBook);";

                connection.Execute(sql, new { IdUser = userBook.UserId, IdBook = userBook.BookId });
            }
        }
    }
}
