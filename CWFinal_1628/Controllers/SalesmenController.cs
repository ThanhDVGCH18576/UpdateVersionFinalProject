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
    public class SalesmenController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Salesmen
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(db.Salesmen.ToList());
        }

        // GET: Salesmen/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            Salesman salesman = new Salesman();
            return View(salesman);
        }

        // POST: Salesmen/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Salesman salesman)
        {
            try
            {
                if (salesman.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(salesman.ImageUpload.FileName);
                    string extension = Path.GetExtension(salesman.ImageUpload.FileName);
                    fileName = fileName + extension;
                    salesman.Image = "~/Content/images/" + fileName;
                    salesman.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), fileName));
                }
                db.Salesmen.Add(salesman);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch { return View(); }
            
        }

        // GET: Salesmen/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salesman salesman = db.Salesmen.Find(id);
            if (salesman == null)
            {
                return HttpNotFound();
            }
            return View(salesman);
        }

        // POST: Salesmen/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Salesman salesman)
        {
            try
            {
                if (salesman.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(salesman.ImageUpload.FileName);
                    string extension = Path.GetExtension(salesman.ImageUpload.FileName);
                    fileName = fileName + extension;
                    salesman.Image = "~/Content/images/" + fileName;
                    salesman.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), fileName));
                }
                db.Entry(salesman).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch { return View(); }
            
        }

        // GET: Salesmen/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salesman salesman = db.Salesmen.Find(id);
            if (salesman == null)
            {
                return HttpNotFound();
            }
            return View(salesman);
        }

        // POST: Salesmen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Salesman salesman = db.Salesmen.Find(id);
            db.Salesmen.Remove(salesman);
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
