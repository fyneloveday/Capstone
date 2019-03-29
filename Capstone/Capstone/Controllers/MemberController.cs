using Capstone.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Capstone.Controllers
{
    public class MemberController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Member
        public ActionResult Index()
        {
            var members = db.MemberModels.ToList();
            return View(members);
        }

        // GET: Member/Details/5
        public ActionResult Details(int id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var members = db.MemberModels.SingleOrDefault();
            return View(members);
        }

        // GET: Member/Create
        public ActionResult Create(int id)
        {
            return View();
        }

        // POST: Member/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MemberModel member)
        {
            if (ModelState.IsValid)
            {
                db.MemberModels.Add(member);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View (member);
        }

        // GET: Member/Edit/5
        public ActionResult Edit(int id)
        {
            var member = db.MemberModels.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // POST: Member/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MemberModel member)
        {
            if (ModelState.IsValid)
            {
                db.Entry(member).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(member);
        }

        // GET: Member/Delete/5
        public ActionResult Delete(int id)
        {
            MemberModel member = db.MemberModels.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // POST: Member/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
                MemberModel member = db.MemberModels.Find(id);
                db.MemberModels.Remove(member);
                db.SaveChanges();
                return RedirectToAction("Index");          
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult BookEntrySubmission()
        {
            return View();
        }

        [HttpPost]
        public ActionResult BookEntrySubmission(BookEntryModel newBook)
        {
           if (ModelState.IsValid)
            {
                db.BookEntryModels.Add(newBook);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return View(newBook);
            }
        }

        public ActionResult CreateGroup(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateGroup(GroupModel newGroup)
        {
           try
            {
                // the user that is logged in is the AdminPersonID
                var newAdmin = User.Identity.GetUserId();
                var makeAdmin = db.MemberModels.Where(m => m.ApplicationUserId == newAdmin).FirstOrDefault().ID;
                newGroup.MemberModelId = makeAdmin;
                db.GroupModels.Add(newGroup);
                db.SaveChanges();
                return View("Index", "GroupAdmin");
            }
            catch
            {
                return View(newGroup);
            }
        }
    }
}
