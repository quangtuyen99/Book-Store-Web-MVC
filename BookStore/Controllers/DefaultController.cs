using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        BookEntities _db = new BookEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult getNews()
        {
            var v = from t in _db.NEWS
                    where t.hide == true
                    orderby t.order ascending
                    select t;
            return PartialView(v.ToList());
        }

        public ActionResult getBanners()
        {
            var v = from t in _db.BANNERs
                    where t.hide == true
                    orderby t.order ascending
                    select t;
            return PartialView(v.ToList());
        }
        public ActionResult getRecommes()
        {
            ViewBag.meta = "san-pham";
            var v = from t in _db.PRODUCTs
                    where t.hide == true
                    orderby t.order ascending
                    select t;
            return PartialView(v.ToList());
        }
        public ActionResult getAbout()
        {
            var v = from t in _db.Abouts
                    where t.hide == true
                    orderby t.order ascending
                    select t;
            return PartialView(v.ToList());
        }
        public ActionResult getBook()
        {
            ViewBag.meta = "san-pham";
            var v = from t in _db.PRODUCTs
                    where t.hide == true
                    orderby t.order ascending
                    select t;
            return PartialView(v.ToList());
        }
        public ActionResult getCategory()
        {
            ViewBag.meta = "san-pham";
            var v = from t in _db.CATEGORies
                    where t.hide == true
                    orderby t.order ascending
                    select t;
            return PartialView(v.ToList());
        }
        public ActionResult getProduct(long id, string metatitle)
        {
            ViewBag.meta = metatitle;
            var v = from t in _db.PRODUCTs
                    where t.idcategory == id && t.hide == true
                    orderby t.order ascending
                    select t;
            return PartialView(v.ToList());
        }
    }
}