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
        public ActionResult Create()
        {
            return View();
        }

        // POST: Member/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MemberModel member)
        {
            member.ApplicationUserId = User.Identity.GetUserId();

            if (ModelState.IsValid)
            {
                var loggedMember = db.MemberModels.Where(m => m.ID == member.ID);
                db.MemberModels.Add(member);
                db.SaveChanges();
               return RedirectToAction("Index", "Member");
            }
            else
            {
                return View(member);
            }
            
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
            var memberInDb = db.MemberModels.Single(m => m.ID == member.ID);
            memberInDb.FirstName = member.FirstName;
            memberInDb.MiddleName = member.MiddleName;
            memberInDb.LastName = member.LastName;
           // memberInDb.Email = member.Email;
            memberInDb.FavoriteBook = member.FavoriteBook;
            memberInDb.AboutYourself = member.AboutYourself;
            db.SaveChanges();

            return RedirectToAction("Index", "Member");
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

        public ActionResult CurrentlyReading()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CurrentlyReading(ReadingListModel newBook)
        {
            if (ModelState.IsValid)
            {
                db.ReadingListModels.Add(newBook);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return View(newBook);
            }
        }

    }
}
