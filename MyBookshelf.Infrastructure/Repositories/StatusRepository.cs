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
    public class StatusRepository : IStatusRepository
    {
        private readonly IConfiguration _configuration;
        public Status FindById(int id)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                var sql = "SELECT * FROM Status WHERE Id = @Id";

                return connection.Query<Status>(sql, new { Id = id }).SingleOrDefault();
            }
        }
    }
}
