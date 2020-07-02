using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.Models;
using BookStore.Encrypt;
namespace BookStore.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        BookEntities _db = new BookEntities();
        public ActionResult Login(string meta)
        {
            var v = from t in _db.MENUs
                    where meta == "login"
                    select t;
            return View(v.FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Login(BookStore.Models.USER usermodel)
        {
            using (BookEntities db = new BookEntities())
            {
      
                var userDetails = db.USERs.Where(x => x.username == usermodel.username && x.password == usermodel.password).FirstOrDefault();
                if (userDetails == null)
                {
                    usermodel.LoginErrorMessage = "Username or password wrong";
                    return View("Login", usermodel);
                }    
                else
                {
                    Session["iduser"] = userDetails.iduser;
                    Session["username"] = userDetails.username;
                    return RedirectToAction("Index", "Default");
                }                    
            }
            
        }
        public ActionResult LogOut()
        {
            int userid = (int)Session["iduser"];
            Session.Abandon();
            return RedirectToAction("Index", "Default");
        }
    }
}