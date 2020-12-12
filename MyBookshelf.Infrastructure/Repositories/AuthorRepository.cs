using Dapper;
using Microsoft.Extensions.Configuration;
using MyBookshelf.Core.Entities;
using MyBookshelf.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace MyBookshelf.Infrastructure.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly IConfiguration _configuration;

        public AuthorRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Author FindByName(string name)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                var sql = "SELECT TOP (1) * FROM Author WHERE Name = @Name;";

                return connection.Query<Author>(sql, new { Name = name }).SingleOrDefault();
            }
        }
    }
}
