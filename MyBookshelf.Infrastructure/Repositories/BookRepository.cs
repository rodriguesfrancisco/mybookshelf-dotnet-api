using MyBookshelf.Application.Queries.SearchBook;
using MyBookshelf.Core.Entities;
using MyBookshelf.Core.Interfaces.Repositories;
using MyBookshelf.Core.Utils;
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
                    var pagedList = new PagedList<object>(booksList, (int)totalItems, page, pageSize);
                    return pagedList;
                }
            }
        }

    }
}
