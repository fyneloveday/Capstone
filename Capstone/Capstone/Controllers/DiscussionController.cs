using Capstone.Models;
using PusherServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Capstone.Controllers
{
    public class DiscussionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View(db.AssignedBook.AsQueryable());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(AssignedBook post)
        {
            db.AssignedBook.Add(post);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            return View(db.AssignedBook.Find(id));
        }

        public ActionResult Comments(int? id)
        {
            var comments = db.AssignedBook.Where(x => x.BlogPostID == id).ToArray();
            return Json(comments, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> Comment(Comments data)
        {
            db.Comment.Add(data);
            db.SaveChanges();
            var options = new PusherOptions();
            options.Cluster = "XXX_APP_CLUSTER";
            var pusher = new Pusher("XXX_APP_ID", "XXX_APP_KEY", "XXX_APP_SECRET", options);
            ITriggerResult result = await pusher.TriggerAsync("asp_channel", "asp_event", data);
            return Content("ok");
        }
    }
}