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
    public class AssignProjectsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AssignProjects
        [Authorize]
        public ActionResult Index()
        {
            var assignProjects = db.AssignProjects.Include(a => a.Builder).Include(a => a.Project).Include(a => a.Supervisor);
            return View(assignProjects.ToList());
        }

        // GET: AssignProjects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignProject assignProject = db.AssignProjects.Find(id);
            if (assignProject == null)
            {
                return HttpNotFound();
            }
            return View(assignProject);
        }

        // GET: AssignProjects/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.BuilderID = new SelectList(db.Builders, "ID", "FullName");
            ViewBag.ProjectID = new SelectList(db.Projects, "ProjetcID", "ProjectName");
            ViewBag.SupervisorID = new SelectList(db.Supervisors, "ID", "FullName");
            return View();
        }

        // POST: AssignProjects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AssignID,ProjectID,BuilderID,SupervisorID")] AssignProject assignProject)
        {
            if (ModelState.IsValid)
            {
                db.AssignProjects.Add(assignProject);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BuilderID = new SelectList(db.Builders, "ID", "FullName", assignProject.BuilderID);
            ViewBag.ProjectID = new SelectList(db.Projects, "ProjetcID", "ProjectName", assignProject.ProjectID);
            ViewBag.SupervisorID = new SelectList(db.Supervisors, "ID", "FullName", assignProject.SupervisorID);
            return View(assignProject);
        }

        // GET: AssignProjects/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignProject assignProject = db.AssignProjects.Find(id);
            if (assignProject == null)
            {
                return HttpNotFound();
            }
            ViewBag.BuilderID = new SelectList(db.Builders, "ID", "FullName", assignProject.BuilderID);
            ViewBag.ProjectID = new SelectList(db.Projects, "ProjetcID", "ProjectName", assignProject.ProjectID);
            ViewBag.SupervisorID = new SelectList(db.Supervisors, "ID", "FullName", assignProject.SupervisorID);
            return View(assignProject);
        }

        // POST: AssignProjects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AssignID,ProjectID,BuilderID,SupervisorID")] AssignProject assignProject)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assignProject).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BuilderID = new SelectList(db.Builders, "ID", "FullName", assignProject.BuilderID);
            ViewBag.ProjectID = new SelectList(db.Projects, "ProjetcID", "ProjectName", assignProject.ProjectID);
            ViewBag.SupervisorID = new SelectList(db.Supervisors, "ID", "FullName", assignProject.SupervisorID);
            return View(assignProject);
        }

        // GET: AssignProjects/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignProject assignProject = db.AssignProjects.Find(id);
            if (assignProject == null)
            {
                return HttpNotFound();
            }
            return View(assignProject);
        }

        // POST: AssignProjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AssignProject assignProject = db.AssignProjects.Find(id);
            db.AssignProjects.Remove(assignProject);
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
