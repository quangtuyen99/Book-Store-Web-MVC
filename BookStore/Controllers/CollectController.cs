using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Controllers
{
    public class CollectController : Controller
    {
        // GET: Collect
        BookEntities _db = new BookEntities();
        public ActionResult Index()
        {
            int id = (int)Session["iduser"];
            var v = from t in _db.COLLECTIONs
                    where t.iduser == id
                    orderby t.order ascending
                    select t;

            return View(v.ToList());
        }

        public ActionResult Collect(int id)
        {
            var v = from t in _db.PRODUCTs
                    where t.idproduct == id
                    orderby t.order ascending
                    select t;
            return PartialView(v.FirstOrDefault());
        }

    }
}