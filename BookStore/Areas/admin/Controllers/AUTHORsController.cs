using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookStore.Models;
using BookStore.Help;
namespace BookStore.Areas.admin.Controllers
{
    public class AUTHORsController : Controller
    {
        private BookEntities db = new BookEntities();

        // GET: admin/AUTHORs
        public ActionResult Index()
        {
            return View(db.AUTHORs.ToList());
        }

        // GET: admin/AUTHORs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AUTHOR aUTHOR = db.AUTHORs.Find(id);
            if (aUTHOR == null)
            {
                return HttpNotFound();
            }
            return View(aUTHOR);
        }

        // GET: admin/AUTHORs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/AUTHORs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "idauthor,name,img,meta,hide,order,datebegin")] AUTHOR aUTHOR, HttpPostedFileBase img)
        {
            var path = "";
            var filename = "";


            if (ModelState.IsValid)
            {
                if (img != null)
                {
                    filename = img.FileName;
                    path = Path.Combine(Server.MapPath("~/Content/images"), filename);
                    img.SaveAs(path);
                    aUTHOR.img = filename;
                }
                else
                {
                    aUTHOR.img = "1.png";
                }
                aUTHOR.datebegin = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                aUTHOR.meta = Functions.ConvertToUnSign(aUTHOR.name);

                db.AUTHORs.Add(aUTHOR);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            


            return View(aUTHOR);
        }

        // GET: admin/AUTHORs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AUTHOR aUTHOR = db.AUTHORs.Find(id);
            if (aUTHOR == null)
            {
                return HttpNotFound();
            }
            return View(aUTHOR);
        }

        // POST: admin/AUTHORs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "idauthor,name,img,meta,hide,order,datebegin")] AUTHOR aUTHOR, HttpPostedFileBase img)
        {
            var path = "";
            var filename = "";
            AUTHOR temp = getById(aUTHOR.idauthor);
            if (ModelState.IsValid)
            {
                if (img != null)
                {
                    filename = DateTime.Now.ToString("dd-MM-yy-hh-mm-ss-") + img.FileName;
                    path = Path.Combine(Server.MapPath("~/Content/images"), filename);
                    img.SaveAs(path);
                    temp.img = filename;
                }
                temp.name = aUTHOR.name;
                temp.meta = Functions.ConvertToUnSign(aUTHOR.meta);
                temp.hide = aUTHOR.hide;
                temp.order = aUTHOR.order;

                db.Entry(temp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");


            }
            return View(aUTHOR);
        }
        public AUTHOR getById(long id)
        {
            return db.AUTHORs.Where(x => x.idauthor == id).FirstOrDefault();
        }
        // GET: admin/AUTHORs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AUTHOR aUTHOR = db.AUTHORs.Find(id);
            if (aUTHOR == null)
            {
                return HttpNotFound();
            }
            return View(aUTHOR);
        }

        // POST: admin/AUTHORs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AUTHOR aUTHOR = db.AUTHORs.Find(id);
            db.AUTHORs.Remove(aUTHOR);
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
