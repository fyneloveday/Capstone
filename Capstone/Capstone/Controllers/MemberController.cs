using Capstone.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
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
            memberInDb.CurrentlyReading = member.CurrentlyReading;
            memberInDb.AboutYourself = member.AboutYourself;
            db.SaveChanges();

            return RedirectToAction("Index");
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

        public ActionResult JoinGroup(int? Id)
        {
            var groupToJoin = db.GroupModels.Find(Id);
            return View(groupToJoin);
        }

        [HttpPost]
        public ActionResult JoinGroup(MemberModel newMember)
        {
           newMember.ApplicationUserId = User.Identity.GetUserId();
            var becomeMember = User.Identity.GetUserId();
            GroupModel groupToJoin = new GroupModel();
            db.MemberModels.Add(newMember);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult BookEntrySubmissionIndex()
        {
            var newEntry = db.BookEntryModels.ToList();
            return View();
        }

         public ActionResult BookEntrySubmission()
        {
            return View();
        }

        [HttpPost]
        public ActionResult BookEntrySubmission(BookEntryModel newBook)
        {
            //try
            //{
            //    WebMail.SmtpServer = "smtp.gmail.com";
            //    WebMail.SmtpPort = 587;
            //    WebMail.SmtpUseDefaultCredentials = true;
            //    WebMail.EnableSsl = true;
            //    WebMail.UserName = db.Users.Select(u => u.Email).ToString();                //"fynecode@gmail.com";
            //    WebMail.Password = db.Users.Select(u => u.PasswordHash).ToString();               //"codemaster"; 

            //    WebMail.From = db.Users.Select(u => u.Email).ToString();                   //"";

            //    WebMail.Send(to: "fynecode@gmail.com", title: newBook.Title, firstName: newBook.AuthorFirstName, middleName: newBook.AuthorMiddleName, lastName: newBook.AuthorLastName, yearPublished: newBook.YearPublished, isbn: newBook.ISBN, publisher: newBook.Publisher, synopsis: newBook.Synopsis, rating: newBook.Rating, isBodyHtml: true);
            //    ViewBag.status = "Email Sent Successfully.";
            //}
            //catch
            //{
            //    ViewBag.Status = "Problem while sending email, Please check details.";
            //}
            //return View();


            //if (ModelState.IsValid)
            // {
            //     db.BookEntryModels.Add(newBook);
            //     db.SaveChanges();

            //     return RedirectToAction("BookEntrySubmissionIndex");
            // }
            // else
            {
                return View(newBook);
            }
        }

        public ActionResult ReadingListIndex()
        {
            var newList = db.ReadingListModels.ToList();
            return View(newList);
        }

        public ActionResult ReadingList()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ReadingList(ReadingListModel newBook)
        {
            if (ModelState.IsValid)
            {
                db.ReadingListModels.Add(newBook);
                db.SaveChanges();

                return RedirectToAction("ReadingListIndex");
            }
            else
            {
                return View(newBook);
            }
        }

        // GET: Member/Delete/5
        public ActionResult DeleteBook(int id)
        {
            ReadingListModel erasedBook = db.ReadingListModels.Find(id);
            if (erasedBook == null)
            {
                return HttpNotFound();
            }
            return View(erasedBook);
        }

        // POST: Member/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BookDeleted(int id)
        {
            ReadingListModel erasedBook = db.ReadingListModels.SingleOrDefault(r => r.ID == id);
            db.ReadingListModels.Remove(erasedBook);
            db.SaveChanges();
            var erasedBooks = db.ReadingListModels.ToList();

            return View("ReadingListIndex", erasedBook);
        }


        //public ActionResult SubmitBook()
        //{
        //    return View();
        //}

        //public ActionResult SubmitBook()
        //{
        //    return View();
        //}

        
        //protected override void Dispose(bool disposing)
        //{
        //    db.Dispose();
        //    base.Dispose(disposing);
        //}

        //public ActionResult BookRating()
        //{
        //    return View();
        //}

        //public ActionResult BookRating(int ratedBook, int rank)
        //{
        //    ReadingListModel rating = new ReadingListModel();
        //    rating.Rating = rank;
        //    rating.ID = ratedBook;
        //    rating.ApplicationUserId = User.Identity.GetUserId();

        //    db.ReadingListModels.Add(rating);
        //    db.SaveChanges();

        //    return RedirectToAction("ReadingList");//, "Member", new { id = ratedBookId });
        //}

    }
}
