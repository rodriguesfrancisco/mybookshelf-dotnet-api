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
    public class UserBookRepository : IUserBookRepository
    {
        private readonly IConfiguration _configuration;

        public UserBookRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public int Save(UserBook userBook)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    var sql = "INSERT INTO BookUser(IdUser, IdBook, IdStatus) VALUES(@IdUser, @IdBook, @IdStatus); SELECT SCOPE_IDENTITY();";

                    var userBookId = connection.ExecuteScalar<int>(sql, new { IdUser = userBook.User.Id, IdBook = userBook.Book.Id, IdStatus = userBook.Status.Id }, transaction);
                    userBook.Id = userBookId;

                    SaveStatusHistories(userBook, connection, transaction);
                    transaction.Commit();
                    return userBookId;
                }
            }
        }

        public UserBook FindByUserIdAndBookId(int userId, int bookId)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                var sqlUserBook = @"SELECT BU.Id AS UserBookId, BU.Id, BU.ConclusionDate, BU.Rating,  
                              B.Id AS BookId, B.Id, B.Description, B.Isbn, B.PageCount, B.Publisher, B.SmallThumbnail, B.Subtitle, B.Thumbnail, B.Title,
                              S.Id AS StatusId, S.Id, S.Description,
                              U.Id AS UserId, U.Id, U.Email, U.Nome,
                              A.Id AS AuthorId, A.Id, A.Name,
                              C.Id AS CategoryId, C.Id, C.Name
                            FROM [Book] B
                              INNER JOIN BookUser BU ON B.Id = BU.IdBook
                              INNER JOIN Usuario U ON U.Id = BU.IdUser
                              INNER JOIN Status S ON S.Id = BU.IdStatus  
                              LEFT JOIN BookAuthor BA ON BA.IdBook = B.Id
                              LEFT JOIN Author A ON BA.IdAuthor = A.Id
                              LEFT JOIN BookCategory BC ON BC.IdBook = B.Id
                              LEFT JOIN Category C ON C.Id = BC.IdCategory
                            WHERE BU.IdUser = @IdUser
	                            AND BU.IdBook = @IdBook;";

                var userBook = connection.Query<UserBook, Book, Status, User, Author, Category, UserBook>(sqlUserBook,
                    map: (userBook, book, status, user, author, category) =>
                    {
                        book.AddAuthor(author);
                        book.AddCategory(category);
                        userBook.Book = book;
                        userBook.Status = status;
                        userBook.User = user;
                        return userBook;
                    }, new { IdUser = userId, IdBook = bookId }, splitOn: "UserBookId, BookId, StatusId, UserId, AuthorId, CategoryId").SingleOrDefault();

                var sqlStatusHistory = @"SELECT SH.Id AS StatusHistoryId, SH.Id, SH.Date,
                                          S.Id AS StatusId, S.Id, S.Description
                                        FROM StatusHistory SH
                                          INNER JOIN Status S ON S.Id = SH.IdStatus
                                        WHERE SH.IdUserBook = @IdUserBook";

                var statusHistoryList = connection.Query<StatusHistory, Status, StatusHistory>(sqlStatusHistory,
                    map: (statusHistory, status) =>
                    {
                        statusHistory.Status = status;
                        return statusHistory;
                    }, new { IdUserBook = userBook.Id }, splitOn: "StatusHistoryId, StatusId").ToList();

                userBook.StatusHistories.AddRange(statusHistoryList);

                return userBook;
            }
                
        }

        public void Update(UserBook userBook)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    var sql = "UPDATE BookUser SET IdStatus = @IdStatus, ConclusionDate = @ConclusionDate, Rating = @Rating WHERE Id = @Id;";

                    connection.Execute(sql, new 
                    { 
                        IdStatus = userBook.Status.Id,
                        userBook.ConclusionDate,
                        userBook.Rating,
                        userBook.Id
                    }, transaction);
                    SaveStatusHistories(userBook, connection, transaction);
                    transaction.Commit();
                }
                
            }
        }

        private void SaveStatusHistories(UserBook userBook, SqlConnection connection, SqlTransaction transaction)
        {
            var sqlStatusHistory = "INSERT INTO StatusHistory VALUES(@IdUserBook, @IdStatus, @Date);";
            foreach (var statusHistory in userBook.StatusHistories)
            {
                if (statusHistory.Id == null)
                {
                    connection.Execute(sqlStatusHistory, new 
                    { 
                        IdUserBook = statusHistory.UserBook.Id, 
                        IdStatus = statusHistory.Status.Id,
                        statusHistory.Date
                    } , transaction);
                }
            }
        }
    }
}
