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
    public class PRODUCTsController : Controller
    {
        private BookEntities db = new BookEntities();

        // GET: admin/PRODUCTs
        public ActionResult Index()
        {
            var pRODUCTs = db.PRODUCTs.Include(p => p.AUTHOR).Include(p => p.CATEGORY).Include(p => p.NXB);
            return View(pRODUCTs.ToList());
        }

        // GET: admin/PRODUCTs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRODUCT pRODUCT = db.PRODUCTs.Find(id);
            if (pRODUCT == null)
            {
                return HttpNotFound();
            }
            return View(pRODUCT);
        }

        // GET: admin/PRODUCTs/Create
        public ActionResult Create()
        {
            ViewBag.idauthor = new SelectList(db.AUTHORs, "idauthor", "name");
            ViewBag.idcategory = new SelectList(db.CATEGORies, "idcategory", "namecategory");
            ViewBag.idnxb = new SelectList(db.NXBs, "idnxb", "name");
            return View();
        }

        // POST: admin/PRODUCTs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "idproduct,idnxb,idcategory,idauthor,name,meta,hide,order,datebegin,img,description,content,audio,filetype")] PRODUCT pRODUCT, HttpPostedFileBase img)
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
                    pRODUCT.img = filename;
                }
                else
                {
                    pRODUCT.img = "1.png";
                }
                pRODUCT.datebegin = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                pRODUCT.meta = Functions.ConvertToUnSign(pRODUCT.name);

                db.PRODUCTs.Add(pRODUCT);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idauthor = new SelectList(db.AUTHORs, "idauthor", "name", pRODUCT.idauthor);
            ViewBag.idcategory = new SelectList(db.CATEGORies, "idcategory", "namecategory", pRODUCT.idcategory);
            ViewBag.idnxb = new SelectList(db.NXBs, "idnxb", "name", pRODUCT.idnxb);
            return View(pRODUCT);
        }

        // GET: admin/PRODUCTs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRODUCT pRODUCT = db.PRODUCTs.Find(id);
            if (pRODUCT == null)
            {
                return HttpNotFound();
            }
            ViewBag.idauthor = new SelectList(db.AUTHORs, "idauthor", "name", pRODUCT.idauthor);
            ViewBag.idcategory = new SelectList(db.CATEGORies, "idcategory", "namecategory", pRODUCT.idcategory);
            ViewBag.idnxb = new SelectList(db.NXBs, "idnxb", "name", pRODUCT.idnxb);
            return View(pRODUCT);
        }

        // POST: admin/PRODUCTs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "idproduct,idnxb,idcategory,idauthor,name,meta,hide,order,datebegin,img,description,content,audio,filetype")] PRODUCT pRODUCT, HttpPostedFileBase img)
        {
            var path = "";
            var filename = "";
            PRODUCT temp = getById(pRODUCT.idproduct);

            if (ModelState.IsValid)
            {
                if (img != null)
                {
                    filename = DateTime.Now.ToString("dd-MM-yy-hh-mm-ss-") + img.FileName;
                    path = Path.Combine(Server.MapPath("~/Content/images"), filename);
                    img.SaveAs(path);
                    temp.img = filename;
                }
                temp.name = pRODUCT.name;
                temp.idnxb = pRODUCT.idnxb;
                temp.idcategory = pRODUCT.idcategory;
                temp.idauthor = pRODUCT.idauthor;
                temp.meta = Functions.ConvertToUnSign(pRODUCT.meta);
                temp.hide = pRODUCT.hide;
                temp.order = pRODUCT.order;
                temp.description = pRODUCT.description;
                temp.content = pRODUCT.content;
                
                db.Entry(temp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");

                //db.Entry(pRODUCT).State = EntityState.Modified;
                //db.SaveChanges();
                //return RedirectToAction("Index");
            }
            //ViewBag.idauthor = new SelectList(db.AUTHORs, "idauthor", "name", pRODUCT.idauthor);
            //ViewBag.idcategory = new SelectList(db.CATEGORies, "idcategory", "namecategory", pRODUCT.idcategory);
            //ViewBag.idnxb = new SelectList(db.NXBs, "idnxb", "name", pRODUCT.idnxb);
            return View(pRODUCT);
        }
        public PRODUCT getById(long id)
        {
            return db.PRODUCTs.Where(x => x.idproduct == id).FirstOrDefault();
        }
        // GET: admin/PRODUCTs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRODUCT pRODUCT = db.PRODUCTs.Find(id);
            if (pRODUCT == null)
            {
                return HttpNotFound();
            }
            return View(pRODUCT);
        }

        // POST: admin/PRODUCTs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PRODUCT pRODUCT = db.PRODUCTs.Find(id);
            db.PRODUCTs.Remove(pRODUCT);
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
