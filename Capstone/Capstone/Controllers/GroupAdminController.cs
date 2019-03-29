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
            var groupModels = db.GroupModels.Include(g => g.MemberModel);
            return View(groupModels.ToList());
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
            try
            {
                // the user that is logged in is the AdminPersonID
                var newAdmin = User.Identity.GetUserId();
                var makeAdmin = db.MemberModels.Where(m => m.ApplicationUserId == newAdmin).FirstOrDefault().ID;
                newGroup.MemberModelId = makeAdmin;
                db.GroupModels.Add(newGroup);
                db.SaveChanges();
                return View(newGroup);
            }
            catch
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
            ViewBag.MemberModelId = new SelectList(db.MemberModels, "ID", "FirstName", groupModel.MemberModelId);
            return View(groupModel);
        }

        // POST: GroupAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,GroupName,Description,Rules,MemberModelId")] GroupModel groupModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(groupModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MemberModelId = new SelectList(db.MemberModels, "ID", "FirstName", groupModel.MemberModelId);
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
