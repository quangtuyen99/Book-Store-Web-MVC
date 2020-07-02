using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.Models;
namespace BookStore.Controllers
{
    public class FaqController : Controller
    {
        // GET: Faq
        BookEntities _db = new BookEntities();
        public ActionResult Faq(string meta)
        {
            var v = from t in _db.MENUs
                    where meta == "faq"
                    select t;
            return View(v.FirstOrDefault());
        }
    }
}