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
    public class MENUsController : Controller
    {
        private BookEntities db = new BookEntities();

        // GET: admin/MENUs
        public ActionResult Index()
        {
            return View(db.MENUs.ToList());
        }

        // GET: admin/MENUs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MENU mENU = db.MENUs.Find(id);
            if (mENU == null)
            {
                return HttpNotFound();
            }
            return View(mENU);
        }

        // GET: admin/MENUs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/MENUs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "idmenu,name,link,meta,hide,order,datebegin")] MENU mENU)
        {
            if (ModelState.IsValid)
            {
                mENU.datebegin = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                mENU.meta = Functions.ConvertToUnSign(mENU.name);
                db.MENUs.Add(mENU);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(mENU);
        }

        // GET: admin/MENUs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MENU mENU = db.MENUs.Find(id);
            if (mENU == null)
            {
                return HttpNotFound();
            }
            return View(mENU);
        }

        // POST: admin/MENUs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "idmenu,name,link,meta,hide,order,datebegin")] MENU mENU)
        {
            MENU temp = getById(mENU.idmenu);
            if (ModelState.IsValid)
            {
                temp.name = mENU.name;
                temp.link = mENU.link;
                temp.meta = Functions.ConvertToUnSign(mENU.meta);
                temp.hide = mENU.hide;
                temp.order = mENU.order;

                db.Entry(temp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mENU);
        }
        public MENU getById(long id)
        {
            return db.MENUs.Where(x => x.idmenu == id).FirstOrDefault();
        }
        // GET: admin/MENUs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MENU mENU = db.MENUs.Find(id);
            if (mENU == null)
            {
                return HttpNotFound();
            }
            return View(mENU);
        }

        // POST: admin/MENUs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MENU mENU = db.MENUs.Find(id);
            db.MENUs.Remove(mENU);
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
