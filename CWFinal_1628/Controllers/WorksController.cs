using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CWFinal_1628.Models;

namespace CWFinal_1628.Controllers
{
    public class WorksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Works
        [Authorize(Roles = "Builder, Supervisor")]
        public ActionResult Index()
        {
            var works = db.Works.Include(w => w.Builder).Include(w => w.Project).Include(w => w.Supervisor);
            return View(works.ToList());
        }


        // GET: Works/Create
        [Authorize(Roles = "Supervisor")]
        public ActionResult Create()
        {
            ViewBag.BuilderID = new SelectList(db.Builders, "ID", "FullName");
            ViewBag.ProjectID = new SelectList(db.Projects, "ProjetcID", "ProjectName");
            ViewBag.SupervisorID = new SelectList(db.Supervisors, "ID", "FullName");
            return View();
        }

        // POST: Works/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "WorkID,WorkName,Deadline,ProjectID,SupervisorID,BuilderID,Progress")] Work work)
        {
            if (ModelState.IsValid)
            {
                db.Works.Add(work);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BuilderID = new SelectList(db.Builders, "ID", "FullName", work.BuilderID);
            ViewBag.ProjectID = new SelectList(db.Projects, "ProjetcID", "ProjectName", work.ProjectID);
            ViewBag.SupervisorID = new SelectList(db.Supervisors, "ID", "FullName", work.SupervisorID);
            return View(work);
        }

        // GET: Works/Edit/5
        [Authorize(Roles = "Supervisor")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Work work = db.Works.Find(id);
            if (work == null)
            {
                return HttpNotFound();
            }
            ViewBag.BuilderID = new SelectList(db.Builders, "ID", "FullName", work.BuilderID);
            ViewBag.ProjectID = new SelectList(db.Projects, "ProjetcID", "ProjectName", work.ProjectID);
            ViewBag.SupervisorID = new SelectList(db.Supervisors, "ID", "FullName", work.SupervisorID);
            return View(work);
        }

        // POST: Works/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WorkID,WorkName,Deadline,ProjectID,SupervisorID,BuilderID,Progress")] Work work)
        {
            if (ModelState.IsValid)
            {
                db.Entry(work).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BuilderID = new SelectList(db.Builders, "ID", "FullName", work.BuilderID);
            ViewBag.ProjectID = new SelectList(db.Projects, "ProjetcID", "ProjectName", work.ProjectID);
            ViewBag.SupervisorID = new SelectList(db.Supervisors, "ID", "FullName", work.SupervisorID);
            return View(work);
        }

        // GET: Works/Delete/5
        [Authorize(Roles = "Supervisor")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Work work = db.Works.Find(id);
            if (work == null)
            {
                return HttpNotFound();
            }
            return View(work);
        }

        // POST: Works/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Work work = db.Works.Find(id);
            db.Works.Remove(work);
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
