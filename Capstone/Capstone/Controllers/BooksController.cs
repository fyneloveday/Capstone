using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Routing;

namespace Capstone.Controllers
{
    public class BooksController : ApiController
    {
        public string Get()
        {
            return "Current Bestselling Books";
        }
        public List<string> Get(int Id)
        {
            return new List<string>
            {
                "Data1", "Data2"
            };
        }
    }
}
