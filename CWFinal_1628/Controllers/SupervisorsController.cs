using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CWFinal_1628.Models;

namespace CWFinal_1628.Controllers
{
    public class SupervisorsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Supervisors
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(db.Supervisors.ToList());
        }

        // GET: Supervisors/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            Supervisor supervisor = new Supervisor();
            return View(supervisor);
        }

        // POST: Supervisors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Supervisor supervisor)
        {
            try
            {
                if (supervisor.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(supervisor.ImageUpload.FileName);
                    string extension = Path.GetExtension(supervisor.ImageUpload.FileName);
                    fileName = fileName + extension;
                    supervisor.Image = "~/Content/images/" + fileName;
                    supervisor.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), fileName));
                }
                db.Supervisors.Add(supervisor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch { return View(); }
            
        }

        // GET: Supervisors/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supervisor supervisor = db.Supervisors.Find(id);
            if (supervisor == null)
            {
                return HttpNotFound();
            }
            return View(supervisor);
        }

        // POST: Supervisors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Supervisor supervisor)
        {
            try
            {
                if (supervisor.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(supervisor.ImageUpload.FileName);
                    string extension = Path.GetExtension(supervisor.ImageUpload.FileName);
                    fileName = fileName + extension;
                    supervisor.Image = "~/Content/images/" + fileName;
                    supervisor.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), fileName));
                }
                db.Entry(supervisor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch { return View(); }
            
        }

        // GET: Supervisors/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supervisor supervisor = db.Supervisors.Find(id);
            if (supervisor == null)
            {
                return HttpNotFound();
            }
            return View(supervisor);
        }

        // POST: Supervisors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Supervisor supervisor = db.Supervisors.Find(id);
            db.Supervisors.Remove(supervisor);
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
