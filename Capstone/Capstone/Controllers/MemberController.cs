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
            var loggedInUser = User.Identity.GetUserId();
            var members = db.MemberModels.Where(m => m.ApplicationUserId == loggedInUser).FirstOrDefault();
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
            memberInDb.ProgressInBook = member.ProgressInBook;
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

        [HttpGet]
        public ActionResult JoinGroup(int id)
        {
            var findGroup = db.GroupModels.Find(id);
            if (findGroup == null)
            {
                return HttpNotFound();
            }
            return View(findGroup);
            //return RedirectToAction("Index", "GroupAdmin");
        }

        [HttpPost]
        public ActionResult JoinGroup(GroupModel group)
        {
            var userLoggedIn = User.Identity.GetUserId();
            var member = db.MemberModels.Where(m => m.ApplicationUserId == userLoggedIn).FirstOrDefault();
            var groupToJoin = db.GroupModels.Where(g => g.Id == group.Id).FirstOrDefault();
            GroupMembersModel thing = new GroupMembersModel();
            thing.MemberId = member.ID;
            thing.GroupId = groupToJoin.Id;
            db.Groupmembers.Add(thing);
            db.SaveChanges();
            

            return View();
            
            
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

        public ActionResult ReadingList()
        {
            var newList = db.ReadingListModels.ToList();
            return View(newList);
        }

        public ActionResult ReadingListAdd()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ReadingListAdd(ReadingListModel addedBook)
        {
            try
            {
                db.ReadingListModels.Add(addedBook);
                db.SaveChanges();
                return RedirectToAction("ReadingList");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult ReadingListEdit(int id)
        {
            var bookToEdit = db.ReadingListModels.Find(id);
            return View(bookToEdit);
        }

        [HttpPost]
        public ActionResult ReadingListEdit(ReadingListModel newBook)
        {
            try
            {
                db.Entry(newBook).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("ReadingList");
            }
            catch
            {
                return View(newBook);
            }
        }

        // GET: Member/Delete/5
        public ActionResult ReadingListDelete(int? id)
        {
            ReadingListModel erasedBook = db.ReadingListModels.Find(id);
            return View(erasedBook);
        }

        // POST: Member/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ReadingListDelete(int id)
        {
            try
            {
                var erasedBook = db.ReadingListModels.Find(id);
                db.ReadingListModels.Remove(erasedBook);
                db.SaveChanges();
                return RedirectToAction("ReadingList");
            }
            catch
            {
                return View();

            }
            //ReadingListModel erasedBook = db.ReadingListModels.SingleOrDefault(r => r.ID == id);
            //db.ReadingListModels.Remove(erasedBook);
            //db.SaveChanges();
            //var erasedBooks = db.ReadingListModels.ToList();

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
