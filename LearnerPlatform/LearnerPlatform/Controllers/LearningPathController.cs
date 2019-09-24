using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LearnerPlatform.Models.Snap97_NS_CS;
using Learning_strategy;


namespace LearnerPlatform.Controllers
{
    public class LearningPathController : Controller
    {
        // GET: LearningPath
        public ActionResult Index(string searchTerm)
        {
            Learning_strategy_class obj = new Learning_strategy_class();
            int i = (int)Session["id"];
            obj.Create_learning_path(i);
            IEnumerable<Snap97_NS_CS.Course> courseobj = obj.GetCourses().Where(x => searchTerm == null || x.course_name.ToUpper().StartsWith(searchTerm.ToUpper()));
            List<Course> c = new List<Course>();
            foreach (var course in courseobj)
            {
                c.Add(
                    new Course()
                    {
                        course_description = course.course_description,
                        course_duration = course.course_duration,
                        course_id = course.course_id,
                        course_name = course.course_name
                    });
            }
            if (Session["id"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
                return View(c);
        }
        public ActionResult AddToPath(Course course)
        {
            List<Course> c = new List<Course>();
            c.Add(course);
            return View(c);
        }
    }
}