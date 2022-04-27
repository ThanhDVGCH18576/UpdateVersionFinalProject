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
    public class InvoicesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Invoices
        [Authorize(Roles = "Salesman, Investor, Supervisor")]
        public ActionResult Index()
        {
            var invoices = db.Invoices.Include(i => i.Project);
            return View(invoices.ToList());
        }

        public FileResult DownloadFile(string fileName)
        {
            //Build the File Path.
            string path = Server.MapPath("~/") + fileName.Substring(1, fileName.Length - 1);

            //Send the File to Download.
            return File(path, "application/octet-stream", fileName);
        }

       

        // GET: Invoices/Create
        [Authorize(Roles = "Salesman")]
        public ActionResult Create()
        {
            Invoice invoice = new Invoice();
            ViewBag.ProjectID = new SelectList(db.Projects, "ProjetcID", "ProjectName");
            return View(invoice);
        }

        // POST: Invoices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Invoice invoice)
        {
            try
            {
                if (invoice.FileUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(invoice.FileUpload.FileName);
                    string extension = Path.GetExtension(invoice.FileUpload.FileName);
                    fileName = fileName + extension;
                    invoice.InvoiceFile = "~/Files/" + fileName;
                    invoice.FileUpload.SaveAs(Path.Combine(Server.MapPath("~/Files/"), fileName));
                }
                db.Invoices.Add(invoice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.ProjectID = new SelectList(db.Projects, "ProjetcID", "ProjectName", invoice.ProjectID);
                return View(invoice);

            }


        }

        // GET: Invoices/Edit/5
        [Authorize(Roles = "Salesman")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectID = new SelectList(db.Projects, "ProjetcID", "ProjectName", invoice.ProjectID);
            return View(invoice);
        }

        // POST: Invoices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Invoice invoice)
        {
            try
            {
                if (invoice.FileUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(invoice.FileUpload.FileName);
                    string extension = Path.GetExtension(invoice.FileUpload.FileName);
                    fileName = fileName + extension;
                    invoice.InvoiceFile = "~/Files/" + fileName;
                    invoice.FileUpload.SaveAs(Path.Combine(Server.MapPath("~/Files/"), fileName));
                }
                db.Entry(invoice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.ProjectID = new SelectList(db.Projects, "ProjetcID", "ProjectName", invoice.ProjectID);
                return View(invoice);
            }
           
        }

        // GET: Invoices/Delete/5
        [Authorize(Roles = "Salesman")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Invoice invoice = db.Invoices.Find(id);
            db.Invoices.Remove(invoice);
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
