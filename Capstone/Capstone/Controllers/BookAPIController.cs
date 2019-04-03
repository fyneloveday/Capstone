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
using Microsoft.AspNet.Identity;

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
                    using (var coverArt = new HttpRequestMessage(new HttpMethod("GET"), "https://www.googleapis.com/books/v1/volumes?q=jquery&Key=AIzaSyCGLb8F7CAkFUWVMVlowMQywW42e571Z7k"))
                    {
                        request.Headers.TryAddWithoutValidation("Accept", "application/json");

                        var response = await httpClient.SendAsync(request);
                        var response2 = await httpClient.SendAsync(coverArt);
                        var stringResult = await response.Content.ReadAsStringAsync();
                        var stringResult2 = await response2.Content.ReadAsStringAsync();
                        var json = JObject.Parse(stringResult);
                        var json2 = JObject.Parse(stringResult2);
                        for (int i = 0; i < json["results"].Count(); i++)
           
                        {
                            var title = json["results"][i]["book_details"][0]["title"].ToString();
                            var author = json["results"][i]["book_details"][0]["author"].ToString();
                            var publisher = json["results"][i]["book_details"][0]["publisher"].ToString();
                            var isbn = json["results"][i]["book_details"][0]["primary_isbn13"].ToString();
                            var description = json["results"][i]["book_details"][0]["description"].ToString();
                            var imgString = json2["items"][1]["volumeInfo"]["imageLinks"]["thumbnail"];

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
            }
            return View("GetBestsellerFromApi", currentBestsellers);


            //public async Task<ActionResult> GetBookDetailsWithAPI()
            //{

            //}
                       
        }

        //public ActionResult BookRating()
        //{
        //    return View();
        //}

        //public ActionResult BookRating(int ratedBookId, int rank)
        //{
        //    ReadingListModel rating = new ReadingListModel();
        //    rating.Rating = rank;
        //    rating.ID = ratedBookId;
        //    rating.ApplicationUserId = User.Identity.GetUserId();

        //    db.ReadingListModels.Add(rating);
        //    db.SaveChanges();

        //    return RedirectToAction("ReadingList", "Member", new { id = ratedBookId });
        //}

    }

}
