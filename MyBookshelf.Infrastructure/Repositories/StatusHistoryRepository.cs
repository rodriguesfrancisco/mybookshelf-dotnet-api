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
    public class StatusHistoryRepository : IStatusHistoryRepository
    {
        private readonly IConfiguration _configuration;

        public StatusHistoryRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Save(StatusHistory statusHistory)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                var sql = "INSERT INTO StatusHistory VALUES(@IdUserBook, @IdStatus, @Date);";

                connection.Execute(sql, new { IdUserBook = statusHistory.UserBook.Id, IdStatus = statusHistory.Status.Id, Date = statusHistory.Date });
            }
        }
    }
}
