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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IConfiguration _configuration;

        public CategoryRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Category FindByName(string name)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                var sql = "SELECT TOP (1) * FROM Category WHERE Name = @Name;";

                return connection.Query<Category>(sql, new { Name = name }).SingleOrDefault();
            }
        }
    }
}
