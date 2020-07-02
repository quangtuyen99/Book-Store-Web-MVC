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
    public class NEWSController : Controller
    {
        private BookEntities db = new BookEntities();

        // GET: admin/NEWS
        public ActionResult Index()
        {
            return View(db.NEWS.ToList());
        }

        // GET: admin/NEWS/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NEWS nEWS = db.NEWS.Find(id);
            if (nEWS == null)
            {
                return HttpNotFound();
            }
            return View(nEWS);
        }

        // GET: admin/NEWS/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/NEWS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "idnew,name,description,meta,hide,order,datebegin,country")] NEWS nEWS)
        {
            if (ModelState.IsValid)
            {
                nEWS.datebegin = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                nEWS.meta = Functions.ConvertToUnSign(nEWS.name);
                db.NEWS.Add(nEWS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(nEWS);
        }

        // GET: admin/NEWS/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NEWS nEWS = db.NEWS.Find(id);
            if (nEWS == null)
            {
                return HttpNotFound();
            }
            return View(nEWS);
        }

        // POST: admin/NEWS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "idnew,name,description,meta,hide,order,datebegin,country")] NEWS nEWS)
        {
            NEWS temp = getById(nEWS.idnew);
            if (ModelState.IsValid)
            {
                temp.name = nEWS.name;
                temp.description = nEWS.description;
                temp.meta = Functions.ConvertToUnSign(nEWS.meta);
                temp.hide = nEWS.hide;
                temp.order = nEWS.order;
                temp.country = nEWS.country;

                db.Entry(temp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nEWS);
        }
        public NEWS getById(long id)
        {
            return db.NEWS.Where(x => x.idnew == id).FirstOrDefault();
        }

        // GET: admin/NEWS/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NEWS nEWS = db.NEWS.Find(id);
            if (nEWS == null)
            {
                return HttpNotFound();
            }
            return View(nEWS);
        }

        // POST: admin/NEWS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NEWS nEWS = db.NEWS.Find(id);
            db.NEWS.Remove(nEWS);
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
