using IntroEF.Auth;
using MID_PROJECT.Models.database;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MID_PROJECT.Controllers

{
    //[Logged]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        
       
    public ActionResult UserDetail()
        {
            var db = new MIDEntities10();
            var user = db.user_details.ToList();
            return View(user);
            
        }

        [HttpPost]
        public ActionResult Status(String name ,String option)

        {
            MIDEntities10 db = new MIDEntities10();
            var user = (from p in db.user_details where p.name == name select p).FirstOrDefault();

            user.status = option;
            try
            {
                db.SaveChanges();
                return RedirectToAction("UserDetail");
            }
            catch (DbEntityValidationException s)
            {
                return View();
            }


            
        }


        public ActionResult delete(int id)
        {
            var db = new MIDEntities10();
            var user = (from p in db.user_details where p.id == id select p).SingleOrDefault();
            if(user != null)
            {
                db.user_details.Remove(user);
                db.SaveChanges();
                return RedirectToAction("UserDetail");
            }
            return View(user);
            

        }

        [HttpGet]
        public ActionResult blogPage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult blogPage(blog s1)
        {
            string fileName = Path.GetFileNameWithoutExtension(s1.File.FileName);
            string extension = Path.GetExtension(s1.File.FileName);
            fileName = fileName + extension;
            s1.img = "~/image/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/image/"), fileName);
            s1.File.SaveAs(fileName);
            MIDEntities10 db = new MIDEntities10();
            //var user = (from p in db.blogs where p.a_id == s1.a_id select p).SingleOrDefault();
            blog user = new blog();

            user.a_id = s1.a_id;
            user.date = s1.date;
            user.details = s1.details;

            user.title = s1.title;
                db.blogs.Add(s1);
                db.SaveChanges();
            
            return View();
        }
        

         public ActionResult showblg()
        {
            var db = new MIDEntities10();
            var user = db.blogs.ToList();
            return View(user);
          
        }
        
           public ActionResult deleteBlg(int id)
        {
            var db = new MIDEntities10();
            var user = (from p in db.blogs where p.id == id select p).SingleOrDefault();
            if (user != null)
            {
                db.blogs.Remove(user);
                db.SaveChanges();
                return RedirectToAction("showblg");
            }
            return View();

        }
        

              public ActionResult showAppDetails()
        {
            var db = new MIDEntities10();
            var user = db.s_appartment_details.ToList();
            return View(user);

        }
        public ActionResult retrvApp(String name)
        {
            MIDEntities10 db = new MIDEntities10();
            var user = (from p in db.s_appartment_details where p.country == name select p).FirstOrDefault();

            return View(user);

        }
        public ActionResult Chat()
        {
            return View();
        }



    }
}