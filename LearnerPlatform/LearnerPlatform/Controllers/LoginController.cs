using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LearnerPlatform.Models;
using Snap97_NS_CS;
using Learning_strategy;
namespace LearnerPlatform.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            Session["id"]= null;
            Session["name"] = null;
            return View();
        }
        [HttpPost]
        public ActionResult Index(Learner lc)
        {
            
            Learning_strategy_class obj = new Learning_strategy_class();
          Learner l= obj.Get_all_learners().Where(x => x.learner_pass== lc.learner_pass.ToString()&&x.learner_email==lc.learner_email).First();
            if(!l.learner_id.Equals(null))
            {
                Session["id"] = l.learner_id;
                Session["name"] = l.learner_name;
                return RedirectToAction("Index","Home");
            }
            else
            {
                ViewData["Message"] = "Login failed!!! invalid credentials";
            }
            return View();
        }
    }
}