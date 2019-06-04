using Capstone.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Hosting;
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
        public ActionResult Details(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var members = db.MemberModels.Include(m => m.Files).SingleOrDefault(m => m.ID == id);
            if (members == null)
            {
                return HttpNotFound();
            }
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
        public ActionResult Create(MemberModel member, HttpPostedFileBase upload)
        {
            member.ApplicationUserId = User.Identity.GetUserId();

            if (ModelState.IsValid)
            {
                var loggedMember = db.MemberModels.Where(m => m.ID == member.ID);
                if (upload != null && upload.ContentLength > 0)
                {
                    var avatar = new Models.File
                    {
                        FileName = System.IO.Path.GetFileName(upload.FileName),
                        FileType = FileType.Avatar,
                        ContentType = upload.ContentType
                    };
                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        avatar.Content = reader.ReadBytes(upload.ContentLength);
                    }
                    member.Files = new List<Models.File> { avatar };
                }
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
            var member = db.MemberModels.Include(m => m.Files).SingleOrDefault(s => s.ID == id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // POST: Member/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MemberModel member, HttpPostedFileBase upload)
        {
            var userId = User.Identity.GetUserId();
            var memberInDb = db.MemberModels.Where(m => m.ApplicationUserId == userId).Single();
            //var memberInDb = db.MemberModels.Find(id);
            memberInDb.FirstName = member.FirstName;
            memberInDb.MiddleName = member.MiddleName;
            memberInDb.LastName = member.LastName;
            memberInDb.Age = member.Age;
            memberInDb.FavoriteBook = member.FavoriteBook;
            memberInDb.Rating = member.Rating;
            memberInDb.CurrentlyReading = member.CurrentlyReading;
            memberInDb.ProgressInBook = member.ProgressInBook;
            memberInDb.AboutYourself = member.AboutYourself;
            

            {
                if (upload != null && upload.ContentLength > 0)
                {
                    if (memberInDb.Files.Any(f => f.FileType == FileType.Avatar))
                    {
                        db.Files.Remove(memberInDb.Files.First(f => f.FileType == FileType.Avatar));
                    }
                    var avatar = new Models.File
                    {
                        FileName = System.IO.Path.GetFileName(upload.FileName),
                        FileType = FileType.Avatar,
                        ContentType = upload.ContentType
                    };
                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        avatar.Content = reader.ReadBytes(upload.ContentLength);
                    }
                    memberInDb.Files = new List<Models.File> { avatar };
                }
            }
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

        [HttpGet]
        public ActionResult GroupsCreated()
        {
            var newGroup = db.GroupModels.ToList();
            return View(newGroup);
        }


        [HttpGet]
        public ActionResult MyGroup()
        {
            var myGroupMember = new GroupModel();

            var loggedInUser = User.Identity.GetUserId();
            var members = db.MemberModels.Where(m => m.ApplicationUserId == loggedInUser).FirstOrDefault();
            var registeredGroup = db.Groupmembers.Where(g => g.MemberId == members.ID).ToList();
            List<GroupModel> thing = new List<GroupModel>();
            foreach (GroupMembersModel otherthing in registeredGroup)
            {
                thing.Add(db.GroupModels.Where(p => p.Id == otherthing.GroupId).SingleOrDefault());
            }

            return View(thing);
        }

        [HttpGet]
        public ActionResult JoinGroup(GroupModel group)
        {
            var userLoggedIn = User.Identity.GetUserId();
            var member = db.MemberModels.Where(m => m.ApplicationUserId == userLoggedIn).FirstOrDefault();
            var groupToJoin = db.GroupModels.Where(g => g.Id == group.Id).FirstOrDefault();
            GroupMembersModel joinedGroup = new GroupMembersModel();
            joinedGroup.MemberId = member.ID;
            joinedGroup.GroupId = groupToJoin.Id;
            joinedGroup.GroupMembershipStatus = joinedGroup.GroupMembershipStatus;
            db.Groupmembers.Add(joinedGroup);
            db.SaveChanges();
            return RedirectToAction("MyGroup", "Member");
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
            
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}
