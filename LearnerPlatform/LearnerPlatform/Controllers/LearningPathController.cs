﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LearnerPlatform.Models;
using LearnerPlatform.Models.Snap97_NS_CS;
using Learning_strategy;
using PagedList;


namespace LearnerPlatform.Controllers
{
    public class LearningPathController : Controller
    {
        // GET: LearningPath
        static List<Course> c = new List<Course>();
        public ActionResult Index(string searchTerm, string currentFilter, string sortOrder, int? page)
        {
            ViewData["Message"] = Session["Add_Message"];
            Learning_strategy_class obj = new Learning_strategy_class();
            obj.delete_Null_Path();
            if (Session["id"] == null)
            {
                Session["timeout"] = "Session Expired.....";
                return RedirectToAction("Index", "Login");
            }

            else
            {
                ViewBag.CurrentSort = sortOrder;
                ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
                if (searchTerm != null)
                {
                    page = 1;
                }
                else
                {
                    searchTerm = currentFilter;
                }
                ViewBag.CurrentFilter = searchTerm;
                int i = obj.Get_new_pathid((int)Session["id"]);
                if (i.Equals(0))
                {
                    obj.Create_learning_path((int)Session["id"]);
                    i = obj.Get_new_pathid((int)Session["id"]);
                }
                Session["PathId"] = i;
                IEnumerable<Snap97_NS_CS.Course> courseobj = 
                    obj.GetCourses().Where(x => searchTerm == null ||
                    x.course_name.ToUpper().Contains(searchTerm.ToUpper())||
                    x.course_description.ToUpper().Contains(searchTerm.ToUpper()));
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

                switch (sortOrder)
                {
                    case "name_desc":
                        c = c.OrderByDescending(s => s.course_name).ToList();
                        break;
                    default:
                        c = c.OrderBy(s => s.course_name).ToList();
                        break;
                }
                int pageSize = 5;
                int pageNumber = (page ?? 1);
                return View(c.ToPagedList(pageNumber, pageSize));
            }
        }
        public ActionResult AddToPath(int id)
        {
            if (Session["id"] == null)
            {
                Session["timeout"] = "Session Expired.....";
                return RedirectToAction("Index", "Login");
            }

            else
            {
                if (id == 0)
                {
                    return View(c);
                }
                else
                {
                    Session["Add_Message"] = " ";
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
                            Session["Add_Message"] = "Course Already Present in the Path";
                            return RedirectToAction("index");
                        }
                    }
                    c.Add(course);
                    return View(c);
                }
            }
        }
        public ActionResult delete(int id)
        {
            if (Session["id"] == null)
            {
                Session["timeout"] = "Session Expired.....";
                return RedirectToAction("Index", "Login");
            }

            else
            {
                Learning_strategy_class obj = new Learning_strategy_class();
                Snap97_NS_CS.Course course = obj.GetCourses().Where(x => x.course_id == id).FirstOrDefault();
                Course c = new Course();
                c.course_id = course.course_id;
                c.course_name = course.course_name;
                c.course_description = course.course_description;
                c.course_duration = course.course_duration;
                c.course_lvl = (course_levels)course.course_lvl;
                return View(c);
            }
        }
        public ActionResult remove(int id)
        {
            if (Session["id"] == null)
            {
                Session["timeout"] = "Session Expired.....";
                return RedirectToAction("Index", "Login");
            }

            else
            {
                c.RemoveAll(x => x.course_id == id);
                return RedirectToAction("AddToPath", new { id = 0 });
            }
        }





        public ActionResult CreatePath()
        {
            if (Session["id"] == null)
            {
                Session["timeout"] = "Session Expired.....";
                return RedirectToAction("Index", "Login");
            }

            else
            {
                if (c.Count() != 0)
                {
                    return View(c);
                }
                else
                {
                    Session["Add_Message"] = "Add Course Before You Create Path";
                    return RedirectToAction("Index");
                }
            }
        }
        [HttpPost]
        public ActionResult CreatePath(string pathname)
        {
            if (Session["id"] == null)
            {
                Session["timeout"] = "Session Expired.....";
                return RedirectToAction("Index", "Login");
            }

            else
            {
                Learning_strategy_class obj = new Learning_strategy_class();
                obj.Name_my_path((int)Session["PathId"], pathname);
                foreach (var i in c)
                {
                    obj.Add_course_to_lp((int)Session["PathId"], i.course_id);

                }
                c.Clear();
                obj.delete_Null_Path();
                Session["PathId"] = null;
                return RedirectToAction("index", "course");
            }
        }
    }
}