using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LearnerPlatform.Models.Snap97_NS_CS;
using System.Data.SqlClient;
using System.Data;
using Learning_strategy;
using System.Configuration;
using log4net;
using LearnerPlatform.Utilities;


namespace LearnerPlatform.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Learner learner)
        {
            
                Learning_strategy_class obj = new Learning_strategy_class();
                try
                {
                    List<Snap97_NS_CS.Learner> learners = obj.Get_all_learners().Where(x => x.learner_email == learner.learner_email).ToList();
                    if (learners.Count() == 0)
                    {
                        obj.Add_Account(learner.learner_name, learner.learner_email, learner.learner_grade, learner.learner_roll, learner.learner_pass);
                        Session["account_validate"] = "Account Created sucessfully";
                        return RedirectToAction("Index", "Login");
                    }
                    else
                    {
                        ViewData["message"] = "User Already exist!!!";
                        return View();
                    }
                }
                catch (Exception ex)
                {

                    Log.Info(ex.Message);
                    return View();
                }
            
        }
    }
}