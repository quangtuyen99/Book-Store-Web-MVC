using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.Models;
namespace BookStore.Controllers
{
    public class AboutController : Controller
    {
        // GET: About
        BookEntities _db = new BookEntities();
        public ActionResult About(string meta)
        {
            var v = from t in _db.Abouts
                    where t.hide == true
                    select t;
            return View(v.FirstOrDefault());
        }
    }
}