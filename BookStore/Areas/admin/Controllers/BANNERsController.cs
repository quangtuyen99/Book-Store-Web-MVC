using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Razor.Generator;
using BookStore.Models;
using BookStore.Help;
namespace BookStore.Areas.admin.Controllers
{
    public class BANNERsController : Controller
    {
        private BookEntities db = new BookEntities();

        // GET: admin/BANNERs
        public ActionResult Index()
        {
            return View(db.BANNERs.ToList());
        }

        // GET: admin/BANNERs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BANNER bANNER = db.BANNERs.Find(id);
            if (bANNER == null)
            {
                return HttpNotFound();
            }
            return View(bANNER);
        }

        // GET: admin/BANNERs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/BANNERs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "idbanner,name,link,meta,hide,order,datebegin")] BANNER bANNER, HttpPostedFileBase link)
        {
            var path = "";
            var filename = "";
            if (ModelState.IsValid)
            {
                if (link != null)
                {
                    filename = link.FileName;
                    path = Path.Combine(Server.MapPath("~/Content/images"), filename);
                    link.SaveAs(path);
                    bANNER.link = filename;
                }
                else
                {
                    bANNER.link = "banner1.jpg";
                }
                bANNER.datebegin = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                bANNER.meta = Functions.ConvertToUnSign(bANNER.name);
                db.BANNERs.Add(bANNER);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bANNER);
        }

        // GET: admin/BANNERs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BANNER bANNER = db.BANNERs.Find(id);
            if (bANNER == null)
            {
                return HttpNotFound();
            }
            return View(bANNER);
        }

        // POST: admin/BANNERs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "idbanner,name,link,meta,hide,order,datebegin")] BANNER bANNER, HttpPostedFileBase img)
        {
            var path = "";
            var filename = "";
            BANNER temp = getById(bANNER.idbanner);
            if (ModelState.IsValid)
            {
                if (img != null)
                {
                    filename = DateTime.Now.ToString("dd-MM-yy-hh-mm-ss-") + img.FileName;
                    path = Path.Combine(Server.MapPath("~/Content/images"), filename);
                    img.SaveAs(path);
                    temp.link = filename;
                }
                temp.name = bANNER.name;
                temp.meta = Functions.ConvertToUnSign(bANNER.meta);
                temp.hide = bANNER.hide;
                temp.order = bANNER.order;
                db.Entry(temp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bANNER);
        }
        public BANNER getById(long id)
        {
            return db.BANNERs.Where(x => x.idbanner == id).FirstOrDefault();
        }
        // GET: admin/BANNERs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BANNER bANNER = db.BANNERs.Find(id);
            if (bANNER == null)
            {
                return HttpNotFound();
            }
            return View(bANNER);
        }

        // POST: admin/BANNERs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BANNER bANNER = db.BANNERs.Find(id);
            db.BANNERs.Remove(bANNER);
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
