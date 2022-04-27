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
    public class InvestorsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Investors
        [Authorize(Roles ="Admin")]
        public ActionResult Index()
        {
            return View(db.Investors.ToList());
        }

        // GET: Investors/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            Investor investor = new Investor();
            return View(investor);
        }

        // POST: Investors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Investor investor)
        {
            try
            {
                if (investor.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(investor.ImageUpload.FileName);
                    string extension = Path.GetExtension(investor.ImageUpload.FileName);
                    fileName = fileName + extension;
                    investor.Image = "~/Content/images/" + fileName;
                    investor.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), fileName));
                }
                db.Investors.Add(investor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch { return View(); }

            
        }
        // GET: Investors/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Investor investor = db.Investors.Find(id);
            if (investor == null)
            {
                return HttpNotFound();
            }
            return View(investor);
        }

        // POST: Investors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Investor investor)
        {
            try
            {
                if (investor.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(investor.ImageUpload.FileName);
                    string extension = Path.GetExtension(investor.ImageUpload.FileName);
                    fileName = fileName + extension;
                    investor.Image = "~/Content/images/" + fileName;
                    investor.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), fileName));
                }
                db.Entry(investor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch { return View(); }
            
        }

        // GET: Investors/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Investor investor = db.Investors.Find(id);
            if (investor == null)
            {
                return HttpNotFound();
            }
            return View(investor);
        }

        // POST: Investors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Investor investor = db.Investors.Find(id);
            db.Investors.Remove(investor);
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
