using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Capstone.Controllers
{
    public class BookAPIController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        BookAPIModel[] books = new BookAPIModel[]
        {
           new BookAPIModel{ }
        };

        public IEnumerable<BookAPIModel> GetAllBooks()
        {
            return books;
        }

        //public ActionResult GetBook(int id)
        //{
        //    var book = books.FirstOrDefault(b => b.Id == id);
        //    if (book == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(book);
        //}

        public async Task<ActionResult> GetBestsellerFromApi()
        {
            List<BookAPIModel> currentBestsellers = new List<BookAPIModel>();
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), "https://api.nytimes.com/svc/books/v3/lists.json?list=hardcover-fiction&published-date=2019-03-27&api-key=61cHxzn3reL5IVuycznI8dnRqobCBnhB"))
                {
                    request.Headers.TryAddWithoutValidation("Accept", "application/json");

                    var response = await httpClient.SendAsync(request);
                    var stringResult = await response.Content.ReadAsStringAsync();
                    var json = JObject.Parse(stringResult);
                    for (int i = 0; i < json["results"].Count(); i++)
                    {
                        var title = json["results"][i]["book_details"][0]["title"].ToString();
                        var author = json["results"][i]["book_details"][0]["author"].ToString();
                        var publisher = json["results"][i]["book_details"][0]["publisher"].ToString();
                        var isbn = json["results"][i]["book_details"][0]["primary_isbn13"].ToString();
                        var description = json["results"][i]["book_details"][0]["description"].ToString();

                        BookAPIModel newBook = new BookAPIModel()
                        {
                            Title = title,
                            Author = author,
                            Publisher = publisher,
                            ISBN = isbn,
                            Synopsis = description,
                        };
                        db.BookAPIModels.Add(newBook);
                        currentBestsellers.Add(newBook);
                        db.SaveChanges();
                        
                    }
                }
            }
            return View("GetBestsellerFromApi", currentBestsellers);
        }
    }
}
