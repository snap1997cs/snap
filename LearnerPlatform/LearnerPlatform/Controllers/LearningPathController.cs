using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LearnerPlatform.Models;
using LearnerPlatform.Models.Snap97_NS_CS;
using Learning_strategy;


namespace LearnerPlatform.Controllers
{
    public class LearningPathController : Controller
    {
        // GET: LearningPath
        static List<Course> c = new List<Course>();
        public ActionResult Index(string searchTerm)
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                Learning_strategy_class obj = new Learning_strategy_class();
                int i = obj.Get_new_pathid((int)Session["id"]);
                if (i.Equals(0))
                {
                    obj.Create_learning_path((int)Session["id"]);
                    i = obj.Get_new_pathid((int)Session["id"]);
                }
                Session["PathId"] = i;
                IEnumerable<Snap97_NS_CS.Course> courseobj = obj.GetCourses().Where(x => searchTerm == null || x.course_name.ToUpper().StartsWith(searchTerm.ToUpper()));
                List<Course> c = new List<Course>();
                foreach (var course in courseobj)
                {
                    c.Add(
                            new Course()
                            {
                                course_id =course.course_id,
                                course_name=course.course_name,
                                course_description=course.course_description,
                                course_duration=course.course_duration,
                                course_lvl=(course_levels)course.course_lvl
                    }) ;
                }
                
                return View(c);
            }
        }
        public ActionResult AddToPath(int id)
        {
            if (id == 0)
            {
                return View(c);
            }
            else
            {
                Learning_strategy_class obj = new Learning_strategy_class();
                Snap97_NS_CS.Course courseobj = obj.GetCourses().Where(x => x.course_id == id).FirstOrDefault();
                Course course = new Course()
                {
                    course_description = courseobj.course_description,
                    course_duration = courseobj.course_duration,
                    course_id = courseobj.course_id,
                    course_lvl = (course_levels)courseobj.course_lvl,
                    course_name = courseobj.course_name
                };
                foreach (var c1 in c)
                {
                    if (c1.course_id == courseobj.course_id)
                    {
                        return RedirectToAction("index");
                    }
                }
                c.Add(course);
                return View(c);
            }
        }
        public ActionResult remove(int id)
        {
            c.RemoveAll(x => x.course_id == id);
            return RedirectToAction("AddToPath", new { id = 0 });
        }





        public ActionResult CreatePath()
        {
            return View(c);
        }
        [HttpPost]
        public ActionResult CreatePath(Course course)
        {
            Learning_strategy_class obj = new Learning_strategy_class();
            foreach(var i in c)
            {
                obj.Add_course_to_lp((int)Session["PathId"],i.course_id);
            }
            c.Clear();
            obj.delete_Null_Path();
            Session["PathId"] = null;
            return RedirectToAction("index","course");
        }
    }
}