using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookStore.Help;
using BookStore.Models;

namespace BookStore.Areas.admin.Controllers
{
    public class AUDIOsController : Controller
    {
        private BookEntities db = new BookEntities();

        // GET: admin/AUDIOs
        public ActionResult Index()
        {
            var aUDIOs = db.AUDIOs.Include(a => a.PRODUCT);
            return View(aUDIOs.ToList());
        }

        // GET: admin/AUDIOs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AUDIO aUDIO = db.AUDIOs.Find(id);
            if (aUDIO == null)
            {
                return HttpNotFound();
            }
            return View(aUDIO);
        }

        // GET: admin/AUDIOs/Create
        public ActionResult Create()
        {
            ViewBag.idproduct = new SelectList(db.PRODUCTs, "idproduct", "name");
            return View();
        }

        // POST: admin/AUDIOs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idaudio,name,link,meta,hide,order,datebegin,idproduct")] AUDIO aUDIO)
        {
            if (ModelState.IsValid)
            {

                aUDIO.datebegin = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                aUDIO.meta = Functions.ConvertToUnSign(aUDIO.name);

                db.AUDIOs.Add(aUDIO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idproduct = new SelectList(db.PRODUCTs, "idproduct", "name", aUDIO.idproduct);
            return View(aUDIO);
        }

        // GET: admin/AUDIOs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AUDIO aUDIO = db.AUDIOs.Find(id);
            if (aUDIO == null)
            {
                return HttpNotFound();
            }
            ViewBag.idproduct = new SelectList(db.PRODUCTs, "idproduct", "name", aUDIO.idproduct);
            return View(aUDIO);
        }

        // POST: admin/AUDIOs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idaudio,name,link,meta,hide,order,datebegin,idproduct")] AUDIO aUDIO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aUDIO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idproduct = new SelectList(db.PRODUCTs, "idproduct", "name", aUDIO.idproduct);
            return View(aUDIO);
        }

        // GET: admin/AUDIOs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AUDIO aUDIO = db.AUDIOs.Find(id);
            if (aUDIO == null)
            {
                return HttpNotFound();
            }
            return View(aUDIO);
        }

        // POST: admin/AUDIOs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AUDIO aUDIO = db.AUDIOs.Find(id);
            db.AUDIOs.Remove(aUDIO);
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
