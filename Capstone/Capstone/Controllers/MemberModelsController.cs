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
    public class MemberModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MemberModels
        public ActionResult Index()
        {
            var newMember = User.Identity.GetUserId();
            return View(newMember);
        }

        // GET: MemberModels/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return View("Index");
            }
            MemberModel member = db.MemberModels.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(member);

            }

        }

        // GET: MemberModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MemberModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FirstName,LastName,Password,Age,Gender")] MemberModel memberModel)
        {
            if (ModelState.IsValid)
            {
                db.MemberModels.Add(memberModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(memberModel);
        }

        // GET: MemberModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberModel memberModel = db.MemberModels.Find(id);
            if (memberModel == null)
            {
                return HttpNotFound();
            }
            return View(memberModel);
        }

        // POST: MemberModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FirstName,LastName,Password,Age,Gender")] MemberModel memberModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(memberModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(memberModel);
        }

        // GET: MemberModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberModel memberModel = db.MemberModels.Find(id);
            if (memberModel == null)
            {
                return HttpNotFound();
            }
            return View(memberModel);
        }

        // POST: MemberModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MemberModel memberModel = db.MemberModels.Find(id);
            db.MemberModels.Remove(memberModel);
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
