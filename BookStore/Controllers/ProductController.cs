using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.Models;
namespace BookStore.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        BookEntities _db = new BookEntities();


        public ActionResult Detail(long id)
        {
            ViewBag.meta = "san-pham";
            var v = from t in _db.PRODUCTs
                    where t.idproduct == id
                    select t;
            return View(v.FirstOrDefault());
        }

        public ActionResult Index(string meta)
        {
            var v = from t in _db.CATEGORies
                    where t.meta == meta
                    select t;
            return View(v.FirstOrDefault());
        }
        public ActionResult Create(int idpd)
        {
            using (BookEntities dbModel = new BookEntities())
            {

                int id = (int)Session["iduser"];
                COLLECTION collect = new COLLECTION();
                collect.idproduct = idpd;
                collect.iduser = id;
                dbModel.COLLECTIONs.Add(collect);
                dbModel.SaveChanges();
            }
            ModelState.Clear();
            return RedirectToAction("Index", "Default");
        }
        public ActionResult getNXB(int id)
        {
            var v = from t in _db.NXBs
                    where t.idnxb == id
                    select t;
            return View(v.FirstOrDefault());
        }
    }
}