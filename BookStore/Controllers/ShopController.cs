using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.Models;
namespace BookStore.Controllers
{
    public class ShopController : Controller
    {
        BookEntities _db = new BookEntities();
        // GET: Shop
        public ActionResult Shop()
        {
            var v = from t in _db.PRODUCTs
                    where t.hide == true
                    select t;
            return View(v.FirstOrDefault());
        }
    }
}