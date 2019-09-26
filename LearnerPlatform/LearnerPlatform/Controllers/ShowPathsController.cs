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
        public ActionResult Index()
        {
            Learning_strategy_class obj = new Learning_strategy_class();
            List<Snap97_NS_CS.Learning_path_masted> master_path = obj.Get_learning_path((int)Session["id"]);
            List<Learning_path_masted> local_master_path = new List<Learning_path_masted>();
            foreach(var element in master_path)
            {
                local_master_path.Add(new Learning_path_masted() {
                    learner_id=element.learner_id,
                    path_id=element.path_id,
                    creation_date=element.creation_date
                });
            }
            return View(local_master_path);
        }
        public ActionResult GetPathDetails(int id)
        {
            List<Course> courses = new List<Course>();
            Learning_strategy_class obj = new Learning_strategy_class();
            List<int> courseIDs = obj.Get_course_by_path(id);
            List<Snap97_NS_CS.Course> AllCourses = obj.GetCourses();
            foreach(int courseid in courseIDs)
            {
                Snap97_NS_CS.Course c = AllCourses.Where(x => x.course_id == courseid).FirstOrDefault();
                courses.Add(
                    new Course() {
                        course_id=c.course_id,
                        course_duration=c.course_duration,
                        course_description=c.course_description,
                        course_lvl=(course_levels)c.course_lvl,
                        course_name=c.course_name
                    }
                    );
            }
            return View(courses);
        }
    }
}