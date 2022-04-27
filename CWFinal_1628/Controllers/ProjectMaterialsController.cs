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
    public class ProjectMaterialsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ProjectMaterials
        [Authorize]
        public ActionResult Index()
        {
            var projectMaterials = db.ProjectMaterials.Include(p => p.Material).Include(p => p.Project);
            return View(projectMaterials.ToList());
        }

        // GET: ProjectMaterials/Create
        [Authorize(Roles = "Builder")]
        public ActionResult Create()
        {
            ViewBag.MaterialID = new SelectList(db.Materials, "MaterialID", "MaterialName");
            ViewBag.ProjectID = new SelectList(db.Projects, "ProjetcID", "ProjectName");
            return View();
        }

        // POST: ProjectMaterials/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProjectMaterialID,MaterialID,ProjectID,Date,Quantity,Comment,Status")] ProjectMaterial projectMaterial)
        {
            if (ModelState.IsValid)
            {
                db.ProjectMaterials.Add(projectMaterial);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaterialID = new SelectList(db.Materials, "MaterialID", "MaterialName", projectMaterial.MaterialID);
            ViewBag.ProjectID = new SelectList(db.Projects, "ProjetcID", "ProjectName", projectMaterial.ProjectID);
            return View(projectMaterial);
        }

        // GET: ProjectMaterials/Edit/5
        [Authorize(Roles = "Builder, Supervisor")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectMaterial projectMaterial = db.ProjectMaterials.Find(id);
            if (projectMaterial == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaterialID = new SelectList(db.Materials, "MaterialID", "MaterialName", projectMaterial.MaterialID);
            ViewBag.ProjectID = new SelectList(db.Projects, "ProjetcID", "ProjectName", projectMaterial.ProjectID);
            return View(projectMaterial);
        }

        // POST: ProjectMaterials/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProjectMaterialID,MaterialID,ProjectID,Date,Quantity,Comment,Status")] ProjectMaterial projectMaterial)
        {
            if (ModelState.IsValid)
            {
                db.Entry(projectMaterial).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaterialID = new SelectList(db.Materials, "MaterialID", "MaterialName", projectMaterial.MaterialID);
            ViewBag.ProjectID = new SelectList(db.Projects, "ProjetcID", "ProjectName", projectMaterial.ProjectID);
            return View(projectMaterial);
        }

        // GET: ProjectMaterials/Delete/5
        [Authorize(Roles = "Builder, Supervisor")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectMaterial projectMaterial = db.ProjectMaterials.Find(id);
            if (projectMaterial == null)
            {
                return HttpNotFound();
            }
            return View(projectMaterial);
        }

        // POST: ProjectMaterials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProjectMaterial projectMaterial = db.ProjectMaterials.Find(id);
            db.ProjectMaterials.Remove(projectMaterial);
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
