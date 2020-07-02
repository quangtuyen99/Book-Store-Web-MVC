using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.Models;
namespace BookStore.Areas.admin.Controllers
{
    public class DefaultController : Controller
    {
        // GET: admin/Default
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult VisualizeStudentsResults()
        {
            BookEntities db = new BookEntities();
            int truyenngan = db.PRODUCTs.Where(x => x.idcategory == 3).Count();
            int kinang = db.PRODUCTs.Where(x => x.idcategory == 2).Count();
            Ratio obj = new Ratio();
            obj.kinang = kinang;
            obj.truyenngan = truyenngan;
             
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public class Ratio
        {
            public int kinang { get; set; }
            public int truyenngan { get; set; }
        }
    }
}