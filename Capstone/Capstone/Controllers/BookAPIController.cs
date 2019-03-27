using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Capstone.Controllers
{
    public class BookAPIController : ApiController
    {
        BookAPIModel[] books = new BookAPIModel[]
        {
           new BookAPIModel{ }
        };

        public IEnumerable<BookAPIModel> GetAllBooks()
        {
            return books;
        }

        public IHttpActionResult GetBook(int id)
        {
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }
    }
}
