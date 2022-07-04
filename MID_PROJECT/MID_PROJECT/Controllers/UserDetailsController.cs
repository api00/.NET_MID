using MID_PROJECT.Models.database;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
//using MID_PROJECT.viewModel;

namespace MID_PROJECT.Controllers
{
    public class UserDetailsController : Controller
    {
        // GET: UserDetails
        public ActionResult Index()
        {
            var db = new MIDEntities10();
          
            ViewBag.prof = db.s_profile.ToList();
            ViewBag.blg = db.blogs.ToList();
            ViewBag.app = db.s_appartment_details.ToList();
          
            return View();
        }
        public ActionResult show()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }
     

        [HttpPost]
        public ActionResult Registration(user_details s1)
        {


            string fileName = Path.GetFileNameWithoutExtension(s1.File.FileName);
            string extension = Path.GetExtension(s1.File.FileName);
            fileName = fileName + extension;
            s1.img = "~/image/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/image/"), fileName);
            s1.File.SaveAs(fileName);




            if (ModelState.IsValid)
            {
                MIDEntities10 db = new MIDEntities10();
               
                db.user_details.Add(s1);
                db.SaveChanges();

                if (s1.rules.Equals("Seller"))
                {
                    //adv_details ad = new adv_details();
                    s_profile sp = new s_profile();
                    sp.s_id = s1.user_id;
                    sp.s_email = s1.email;
                    sp.s_name = s1.name;
                    sp.s_img = s1.img;
                    //ad.user_id = s1.user_id;
                    db.s_profile.Add(sp);
                    //db.adv_details.Add(ad);
                    db.SaveChanges();
                }
                 if (s1.rules.Equals("Buyer"))
                {
                    b_profile p = new b_profile();
                    p.b_id = s1.user_id;
                    p.b_email = s1.email;
                    p.b_name = s1.name;
                    p.b_img = s1.img;
                    db.b_profile.Add(p);
                    db.SaveChanges();
                }
                if (s1.rules.Equals("Tenant"))
                {
                    t_profile t = new t_profile();
                    t.t_id= s1.user_id;
                    t.t_email = s1.email;
                    t.t_name = s1.name;
                    t.t_img = s1.img;
                    db.t_profile.Add(t);
                    db.SaveChanges();
                }
                if (s1.rules.Equals("Admin"))
                {
                    //blog b = new blog();
                    a_profile t = new a_profile();
                    t.a_id = s1.user_id;
                    t.a_email = s1.email;
                    t.a_name = s1.name;
                    t.a_img = s1.img;
                    //b.a_id = s1.user_id;
                    db.a_profile.Add(t);
                    //db.blogs.Add(b);
                    db.SaveChanges();
                }

                TempData["name"] = "Bill";
                return RedirectToAction("Registration");
            }
            return View(s1);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(user_details s1)
        {
            var db = new MIDEntities10();
            var users = (from p in db.user_details where p.email == s1.email && p.password == s1.password  select p).SingleOrDefault();
            if (users != null)
            {
                FormsAuthentication.SetAuthCookie(users.name, false); 
                
                if (users.rules.Equals("Admin") && users.status.Equals("Active"))
                {
                    Session["username"] = users.name;
                    Session["role"] = users.rules;
                    Session["user_id"] = users.user_id;
                    return RedirectToAction("Index","Admin");
                }
                else if (users.rules.Equals("Seller") && users.status.Equals("Active"))
                {
                    Session["username"] = users.name;
                    Session["role"] = users.rules;
                    Session["user_id"] = users.user_id;
                    return RedirectToAction("Index", "Seller");
                }
                else if (users.rules.Equals("Buyer") && users.status.Equals("Active"))
                {
                    Session["username"] = users.name;
                    Session["role"] = users.rules;
                    Session["user_id"] = users.user_id;
                    return RedirectToAction("Index", "Buyer");
                }
              


            }
            
                TempData["name"] = "Bill";
               return View(s1);
            
            
            
        }
        public ActionResult AdminDashbord()
        {
            return View();
        }
        public ActionResult Logout()
        {
            Session.Remove("username");
            Session.RemoveAll();
            return RedirectToAction("Index");
        }
    }
}