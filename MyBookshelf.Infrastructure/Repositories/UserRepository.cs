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
                var sql = "SELECT * FROM [User] WHERE Id = @Id";

                return connection.Query<User>(sql, new { Id = id }).SingleOrDefault();
            }
        }
        public void Add(User user)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                var sql = "INSERT INTO [User] VALUES (@Name, @Email, @Password);";

                connection.Execute(sql, user);
            }
        }

        public User LoginUser(string email, string password)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                var sql = "SELECT TOP (1) Id, Email FROM [User] WHERE Email = @Email AND Password = @Password;";

                return connection.Query<User>(sql, new { Email = email, Password = password }).SingleOrDefault();
            }
        }

        public bool EmailExists(string email)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                var sql = "SELECT TOP (1) Id, Email FROM [User] WHERE Email = @Email;";

                return connection.Query<User>(sql, new { Email = email }).Any();
            }
        }
    }
}
