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
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _configuration;
        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration; 
        }
        public User GetById(int id)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                var sql = "SELECT * FROM Usuario WHERE Id = @Id";

                return connection.Query<User>(sql, new { Id = id }).SingleOrDefault();
            }
        }
        public void Add(User user)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                var sql = "INSERT INTO Usuario VALUES (@Nome, @Email, @Senha);";

                connection.Execute(sql, user);
            }
        }
    }
}
