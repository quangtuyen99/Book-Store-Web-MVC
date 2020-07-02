using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.Models;
using BookStore.Encrypt;
using System.Data.Entity.Validation;

namespace BookStore.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        [HttpGet]
        public ActionResult AddOrEdit(int id=0)
        {
            USER userModel = new USER();
            return View(userModel);
        }
        [HttpPost]
        public ActionResult AddOrEdit(USER usermodel)
        {
            using (BookEntities dbModel = new BookEntities())
            {
                try
                {
                    if (dbModel.USERs.Any(x => x.username == usermodel.username))
                    {
                        ViewBag.DuplicateMessage = "Username is exsist";
                        return View("AddOrEdit", usermodel);
                    }
                    //string temp = usermodel.password;
                    //temp = Encryptor.MD5Hash(temp);
                    //usermodel.password = temp;
                    dbModel.USERs.Add(usermodel);
                    dbModel.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw;
                }
                
            }
            ModelState.Clear();
            ViewBag.SuccessMessage = "Register succesfull";
            //return View("AddOrEdit", new USER());
            return RedirectToAction("Login", "Login");
        }


    }
}