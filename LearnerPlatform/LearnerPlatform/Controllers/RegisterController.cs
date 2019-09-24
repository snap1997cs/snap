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
            int data;
            string connectionstring = ConfigurationManager.ConnectionStrings["Snap"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "INSERT INTO [dbo].[learner]"+
                "([learner_ID],[learner_NAME],[learner_EMAIL],[learner_GRADE],[learner_ROLE],[learner_pass])"+
                "VALUES("+learner.learner_id+",'"+learner.learner_name+"','"+learner.learner_email+"','"+learner.learner_grade+"','"+learner.learner_roll+"','"+learner.learner_pass+"')";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            conn.Open();
            try
            { 
                 data = cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                 data = 0;
                Log.Info(ex.Message);
            }
            if (data == 1)
            {
                ViewData["message"] = "Account Created sucessfully";
                return RedirectToAction("Index", "Login");
            }
            else
            {
                
                ViewData["message"] = "Invalid Details.... Please check the Credentials and enter again";
                return View();
            }
        }
    }
}