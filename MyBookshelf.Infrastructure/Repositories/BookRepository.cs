using MyBookshelf.Application.Queries.SearchBook;
using MyBookshelf.Core.Entities;
using MyBookshelf.Core.Interfaces.Repositories;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyBookshelf.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        public async Task<IList<object>> Search(string searchTerm)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(
                    $"https://www.googleapis.com/books/v1/volumes?q={searchTerm}&filter=partial&printType=books&maxResults=20&startIndex=0")
                )
                {
                    var booksList = new List<object>();
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var apiResponseDeserialized = JObject.Parse(apiResponse).SelectToken("items") as JArray;
                    foreach (var result in apiResponseDeserialized)
                    {
                        var volumeInfo = result.SelectToken("volumeInfo");
                        var bookViewModel = JsonConvert.DeserializeObject<BookViewModel>(volumeInfo.ToString());
                        booksList.Add(bookViewModel);
                    }
                    return booksList;
                }
            }
        }
    }
}
