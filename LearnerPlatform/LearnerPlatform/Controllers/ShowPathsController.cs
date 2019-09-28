using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Learning_strategy;
using LearnerPlatform.Models.Snap97_NS_CS;

namespace LearnerPlatform.Controllers
{
    public class ShowPathsController : Controller
    {
        // GET: ShowPaths
        //@Html.ActionLink("Show Courses", "GetPathDetails", new { id=item.path_id})
        public ActionResult Index()
        {
            if (Session["id"] == null)
            {
                Session["timeout"] = "Session Expired.....";
                return RedirectToAction("Index", "Login");
            }

            else
            {
                Learning_strategy_class obj = new Learning_strategy_class();
                obj.delete_Null_Path();
                List<Snap97_NS_CS.Learning_path_masted> master_path = obj.Get_learning_path((int)Session["id"]);
                List<Learning_path_masted> local_master_path = new List<Learning_path_masted>();
                foreach (var element in master_path)
                {
                    local_master_path.Add(new Learning_path_masted()
                    {
                        learner_id = element.learner_id,
                        path_id = element.path_id,
                        pathname = element.pathName,
                        creation_date = element.creation_date
                    });
                }
                return View(local_master_path);
            }
        }
        public ActionResult GetPathDetails(int id, string sortOrder)
        {
            if (Session["id"] == null)
            {
                Session["timeout"] = "Session Expired.....";
                return RedirectToAction("Index", "Login");
            }

            else
            {
                ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "course_dec" : "";
                ViewBag.DurationSortParm = sortOrder == "Duration" ? "Duration_desc" : "Duration";
                List<Course> courses = new List<Course>();
                Learning_strategy_class obj = new Learning_strategy_class();
                List<int> courseIDs = obj.Get_course_by_path(id);
                List<Snap97_NS_CS.Course> AllCourses = obj.GetCourses();
                foreach (int courseid in courseIDs)
                {
                    Snap97_NS_CS.Course c = AllCourses.Where(x => x.course_id == courseid).FirstOrDefault();
                    courses.Add(
                        new Course()
                        {
                            course_id = c.course_id,
                            course_duration = c.course_duration,
                            course_description = c.course_description,
                            course_lvl = (course_levels)c.course_lvl,
                            course_name = c.course_name,
                        //CourseStatus = course_status.enrolled
                    }
                        );
                }
                switch (sortOrder)
                {
                    case "course_dec":
                        courses = courses.OrderByDescending(s => s.course_name).ToList();
                        break;
                    case "Duration":
                        courses = courses.OrderBy(s => s.course_duration).ToList();
                        break;
                    case "Duration_desc":
                        courses = courses.OrderByDescending(s => s.course_duration).ToList();
                        break;
                    default:
                        courses = courses.OrderBy(s => s.course_name).ToList();
                        break;
                }
                return View(courses);
            }
        }
    }
}