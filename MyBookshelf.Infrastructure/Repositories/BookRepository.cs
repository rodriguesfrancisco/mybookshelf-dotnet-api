using Dapper;
using Microsoft.Extensions.Configuration;
using MyBookshelf.Application.Queries.SearchBook;
using MyBookshelf.Core.Entities;
using MyBookshelf.Core.Interfaces.Repositories;
using MyBookshelf.Core.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyBookshelf.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly IConfiguration _configuration;

        public BookRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<PagedList<object>> Search(string searchTerm, int page, int pageSize)
        {
            using (var httpClient = new HttpClient())
            {
                var bookPage = page == 0 ? 0 : pageSize * page;
                using (var response = await httpClient.GetAsync(
                    $"https://www.googleapis.com/books/v1/volumes?q={searchTerm}&filter=partial&printType=books&maxResults={pageSize}&startIndex={bookPage}")
                )
                {
                    var booksList = new List<object>();
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var apiResponseDeserialized = JObject.Parse(apiResponse).SelectToken("items") as JArray;
                    var totalItems = JObject.Parse(apiResponse).SelectToken("totalItems");
                    foreach (var result in apiResponseDeserialized)
                    {
                        var volumeInfo = result.SelectToken("volumeInfo");
                        var bookViewModel = JsonConvert.DeserializeObject<BookViewModel>(volumeInfo.ToString());
                        booksList.Add(bookViewModel);
                    }
                    return new PagedList<object>(booksList, (int)totalItems, page, pageSize);
                }
            }
        }

        public Book FindByIsbn(string isbn)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                var sql = "SELECT TOP (1) * FROM Book WHERE Isbn = @Isbn;";

                return connection.Query<Book>(sql, new { Isbn = isbn }).SingleOrDefault();
            }
        }

        public int Save(Book book)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var insertBookSql = @"INSERT INTO Book 
                                            VALUES (@Title, @Subtitle, @Publisher, @Isbn, @PageCount, @Thumbnail, @SmallThumbnail, @Description); 
                                            SELECT SCOPE_IDENTITY();";
                        var insertedBookId = connection.ExecuteScalar<int>(insertBookSql, book, transaction);

                        foreach (var author in book.Authors)
                        {
                            var authorId = author.Id;
                            if (authorId == null)
                            {
                                var insertAuthorSql = "INSERT INTO Author VALUES (@Name); SELECT SCOPE_IDENTITY();";
                                authorId = connection.ExecuteScalar<int>(insertAuthorSql, author, transaction);                                
                            }
                            var insertBookAuthorSql = "INSERT INTO BookAuthor VALUES (@IdBook, @IdAuthor);";
                            connection.Execute(insertBookAuthorSql, new { IdBook = insertedBookId, IdAuthor = authorId }, transaction);
                        }

                        foreach (var category in book.Categories)
                        {
                            var categoryId = category.Id;
                            if (categoryId == null)
                            {
                                var insertCategorySql = "INSERT INTO Category VALUES (@Name); SELECT SCOPE_IDENTITY();";
                                categoryId = connection.ExecuteScalar<int>(insertCategorySql, category, transaction);                                
                            }
                            var insertBookCategorySql = "INSERT INTO BookCategory VALUES (@IdBook, @IdCategory);";
                            connection.Execute(insertBookCategorySql, new { IdBook = insertedBookId, IdCategory = categoryId }, transaction);
                        }

                        transaction.Commit();

                        return insertedBookId;
                    } catch(Exception e)
                    {
                        transaction.Rollback();
                        throw e;
                    }
                    
                }
                
            }
                
        }
    }
}
