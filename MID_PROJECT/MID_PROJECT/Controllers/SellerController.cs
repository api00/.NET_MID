using MID_PROJECT.Models.database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Data.Entity.Validation;
using MID_PROJECT.Auth;

namespace MID_PROJECT.Controllers
{
    //[seller_auth]
    public class SellerController : Controller
    {
        // GET: Seller
        public ActionResult Index()
        {
           
            return View();
        }

        [HttpGet]
        public ActionResult ApDetails()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ApDetails(s_appartment_details s1)
        {
            
            string fileName = Path.GetFileNameWithoutExtension(s1.File.FileName);
            string extension = Path.GetExtension(s1.File.FileName);
            fileName = fileName + extension;
            s1.img = "~/image/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/image/"), fileName);
            s1.File.SaveAs(fileName);
         using(MIDEntities10 db = new MIDEntities10())
            {
                db.s_appartment_details.Add(s1);
                db.SaveChanges();
            }

            return View();
        }

     
        public ActionResult showApp()
        {
            var db = new MIDEntities10();
            var user = db.s_appartment_details.ToList();
            return View(user);
            
        }
        public ActionResult deleteapp(int id )
        {

            var db = new MIDEntities10();
            var user = (from p in db.s_appartment_details where p.id == id select p).SingleOrDefault();
            if (user != null)
            {
                db.s_appartment_details.Remove(user);
                db.SaveChanges();
                return RedirectToAction("showApp");
            }
            return View(user);

        }

    }
}