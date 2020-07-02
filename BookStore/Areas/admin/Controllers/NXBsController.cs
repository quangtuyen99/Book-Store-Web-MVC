using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookStore.Models;
using BookStore.Help;
namespace BookStore.Areas.admin.Controllers
{
    public class NXBsController : Controller
    {
        private BookEntities db = new BookEntities();

        // GET: admin/NXBs
        public ActionResult Index()
        {
            return View(db.NXBs.ToList());
        }

        // GET: admin/NXBs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NXB nXB = db.NXBs.Find(id);
            if (nXB == null)
            {
                return HttpNotFound();
            }
            return View(nXB);
        }

        // GET: admin/NXBs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/NXBs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "idnxb,name,meta,hide,order,datebegin")] NXB nXB)
        {
            
            if (ModelState.IsValid)
            {
                nXB.datebegin = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                nXB.meta = Functions.ConvertToUnSign(nXB.name);
                db.NXBs.Add(nXB);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(nXB);
        }
        
        // GET: admin/NXBs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NXB nXB = db.NXBs.Find(id);
            if (nXB == null)
            {
                return HttpNotFound();
            }
            return View(nXB);
        }

        // POST: admin/NXBs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "idnxb,name,meta,hide,order,datebegin")] NXB nXB)
        {
            NXB temp = getById(nXB.idnxb);
            if (ModelState.IsValid)
            {
                temp.name = nXB.name;
                temp.meta = Functions.ConvertToUnSign(nXB.meta);
                temp.hide = nXB.hide;
                temp.order = nXB.order;

                db.Entry(temp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nXB);
        }
        public NXB getById(long id)
        {
            return db.NXBs.Where(x => x.idnxb == id).FirstOrDefault();
        }
        // GET: admin/NXBs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NXB nXB = db.NXBs.Find(id);
            if (nXB == null)
            {
                return HttpNotFound();
            }
            return View(nXB);
        }

        // POST: admin/NXBs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NXB nXB = db.NXBs.Find(id);
            db.NXBs.Remove(nXB);
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
