using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Protocols;
using BookStore.Models;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;


namespace BookStore.Controllers
{
    public class PlayController : Controller
    {
        // GET: Play
        BookEntities _db = new BookEntities();
        public ActionResult Index(long id)
        {
            ViewBag.data = Server.MapPath("~/Audio") + @"\a.mp3";

            var v = from t in _db.PRODUCTs
                    where t.idproduct == id
                    orderby t.order ascending
                    select t;
            return View(v.FirstOrDefault());
        }
        public ActionResult getAudio(long id)
        {
            var v = from t in _db.AUDIOs
                    where t.idproduct == id && t.hide == true
                    orderby t.order ascending
                    select t;
            return PartialView(v.ToList());
        }
        public ActionResult getAuthor(long id)
        {
            var v = from t in _db.AUTHORs
                    where t.idauthor == id && t.hide == true
                    orderby t.order ascending
                    select t;
            return PartialView(v.FirstOrDefault());
        }


    }
}