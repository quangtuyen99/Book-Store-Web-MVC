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
    public class COLLECTIONsController : Controller
    {
        private BookEntities db = new BookEntities();

        // GET: admin/COLLECTIONs
        public ActionResult Index()
        {
            var cOLLECTIONs = db.COLLECTIONs.Include(c => c.USER).Include(c => c.PRODUCT);
            return View(cOLLECTIONs.ToList());
        }

        // GET: admin/COLLECTIONs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COLLECTION cOLLECTION = db.COLLECTIONs.Find(id);
            if (cOLLECTION == null)
            {
                return HttpNotFound();
            }
            return View(cOLLECTION);
        }

        // GET: admin/COLLECTIONs/Create
        public ActionResult Create()
        {
            ViewBag.iduser = new SelectList(db.USERs, "iduser", "username");
            ViewBag.idproduct = new SelectList(db.PRODUCTs, "idproduct", "name");
            return View();
        }

        // POST: admin/COLLECTIONs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "idcollection,meta,hide,order,datebegin,iduser,idproduct")] COLLECTION cOLLECTION)
        {
            if (ModelState.IsValid)
            {
                cOLLECTION.datebegin = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                db.COLLECTIONs.Add(cOLLECTION);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.iduser = new SelectList(db.USERs, "iduser", "username", cOLLECTION.iduser);
            ViewBag.idproduct = new SelectList(db.PRODUCTs, "idproduct", "name", cOLLECTION.idproduct);
            return View(cOLLECTION);
        }

        // GET: admin/COLLECTIONs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COLLECTION cOLLECTION = db.COLLECTIONs.Find(id);
            if (cOLLECTION == null)
            {
                return HttpNotFound();
            }
            ViewBag.iduser = new SelectList(db.USERs, "iduser", "username", cOLLECTION.iduser);
            ViewBag.idproduct = new SelectList(db.PRODUCTs, "idproduct", "name", cOLLECTION.idproduct);
            return View(cOLLECTION);
        }

        // POST: admin/COLLECTIONs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idcollection,meta,hide,order,datebegin,iduser,idproduct")] COLLECTION cOLLECTION)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cOLLECTION).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.iduser = new SelectList(db.USERs, "iduser", "username", cOLLECTION.iduser);
            ViewBag.idproduct = new SelectList(db.PRODUCTs, "idproduct", "name", cOLLECTION.idproduct);
            return View(cOLLECTION);
        }

        // GET: admin/COLLECTIONs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COLLECTION cOLLECTION = db.COLLECTIONs.Find(id);
            if (cOLLECTION == null)
            {
                return HttpNotFound();
            }
            return View(cOLLECTION);
        }

        // POST: admin/COLLECTIONs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            COLLECTION cOLLECTION = db.COLLECTIONs.Find(id);
            db.COLLECTIONs.Remove(cOLLECTION);
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
