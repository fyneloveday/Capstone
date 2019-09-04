using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Capstone.Models;
using Microsoft.AspNet.Identity;

namespace Capstone.Controllers
{
    public class GroupAdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: GroupAdmin
        public ActionResult Index()
        {
            var groupModels = db.GroupModels.ToList();
            return View(groupModels);
        }

        // GET: GroupAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroupModel groupModel = db.GroupModels.Find(id);
            if (groupModel == null)
            {
                return HttpNotFound();
            }
            return View(groupModel);
        }

        // GET: GroupAdmin/Create
        public ActionResult CreateGroup()
        {
            return View();
        }

        // POST: GroupAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult CreateGroup(GroupModel newGroup)
        {
            var newAdmin = User.Identity.GetUserId();
            if (User.Identity == null)
            {
                return RedirectToAction("Register", "Account");
            }
            else if (ModelState.IsValid)
            {
                // the user that is logged in is the AdminPersonID
                var makeAdmin = db.MemberModels.Where(m => m.ApplicationUserId == newAdmin).FirstOrDefault();
                newGroup.GroupAdminId = makeAdmin.ID;

                db.GroupModels.Add(newGroup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        // GET: GroupAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroupModel groupModel = db.GroupModels.Find(id);
            if (groupModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.MemberModelId = new SelectList(db.MemberModels, "ID", "FirstName", groupModel.GroupAdminId);
            return View(groupModel);
        }

        // POST: GroupAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,GroupName,Description,Rules,GroupAdminId,ReadingAssignment,GroupRole")] GroupModel groupModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(groupModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MemberModelId = new SelectList(db.MemberModels, "ID", "FirstName", groupModel.GroupAdminId);
            return View(groupModel);
        }

        // GET: GroupAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroupModel groupModel = db.GroupModels.Find(id);
            if (groupModel == null)
            {
                return HttpNotFound();
            }
            return View(groupModel);
        }

        // POST: GroupAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GroupModel groupModel = db.GroupModels.Find(id);
            db.GroupModels.Remove(groupModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        

        [HttpGet]
        public ActionResult Members(int id)
        {
            var groupsJoined = db.GroupModels.Where(g => g.Id == id).FirstOrDefault();
            var appMembers = db.Groupmembers.Where(g => g.GroupId == groupsJoined.Id).Select(g => g.MemberId).ToList();
            var membersWhoJoined = db.MemberModels.Where(m => appMembers.Contains(m.ID)).ToList();

            var groupMemberViewModel = new GroupMembersViewModel() { Members = membersWhoJoined, GroupId = groupsJoined.Id };
            return View(groupMemberViewModel);
        }

        public ActionResult MembersToDelete(int id, int secondaryId)
        {           
            var groupMember = db.Groupmembers.First(g => g.GroupId == secondaryId && g.MemberId == id);

            return View(groupMember);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MembersToDelete(GroupMembersModel memberToRemove)
        {
            var groupMember = db.Groupmembers.First(g => g.GroupId == memberToRemove.GroupId && g.MemberId == memberToRemove.MemberId);

            db.Groupmembers.Remove(groupMember);
            db.SaveChanges();
            return RedirectToAction("Members", new { id = memberToRemove.GroupId });
            //try
            //{
            //var member = db.MemberModels.Find(id);
            //db.MemberModels.Remove(member);
            //return RedirectToAction("Members");
            //}
            //catch
            //{
            //    return View();
            //}
        }

        //SqlException: The DELETE statement conflicted with the REFERENCE constraint "FK_dbo.GroupMembersModels_dbo.MemberModels_MemberId". 
        //The conflict occurred in database "aspnet-Capstone-20190322091738", table "dbo.GroupMembersModels", column 'MemberId'.




        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
