using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LearnerPlatform.Models.Snap97_NS_CS;
using Learning_strategy;
using Snap97_NS_CS;

namespace LearnerPlatform.Controllers
{
    public class LearningPathController : Controller
    {
        // GET: LearningPath
        public ActionResult Index()
        {
            Learning_strategy_class obj = new Learning_strategy_class();
            List<Snap97_NS_CS.Learner_Path> l = obj.Get_learning_path((int)Session["id"]);
            List<LearnerPlatform.Models.Snap97_NS_CS.Learner_Path> learners = new List<LearnerPlatform.Models.Snap97_NS_CS.Learner_Path>();
            foreach(var l1 in l)
            {
                foreach (var l2 in l1.details)
                {
                    learners.Add(new Models.Snap97_NS_CS.Learner_Path()
                    {
                        details = { new Models.Snap97_NS_CS.Learner_path_details() {
                            course_id=l2.course_id,
                            path_id=l2.path_id
                        } },
                        master = { new Models.Snap97_NS_CS.Learning_path_masted() {
                            path_id=l2.path_id,
                            learner_id=(int)Session["id"]
                        } }
                    });
                }
            }
            return View();
        }
    }
}