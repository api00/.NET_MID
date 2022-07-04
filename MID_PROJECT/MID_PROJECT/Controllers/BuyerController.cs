using MID_PROJECT.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MID_PROJECT.Models.database;

namespace MID_PROJECT.Controllers
{
    //[Buyer_Auth]
    public class BuyerController : Controller
    {
        // GET: Buyer
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult trans(int id)
        {

            var db = new MIDEntities10();
            var user = (from p in db.s_appartment_details where p.id == id select p).SingleOrDefault();
            if (user != null)
            {
                return View(user);
                
            }
            return View();
        }
    }   
}