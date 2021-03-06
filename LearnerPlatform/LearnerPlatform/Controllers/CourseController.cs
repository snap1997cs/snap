﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Learning_strategy;
using LearnerPlatform.Models.Snap97_NS_CS;
using PagedList;


namespace LearnerPlatform.Controllers
{ 
    public class CourseController : Controller
    {
        public ActionResult Index(string searchTerm, string currentFilter, int? page)
        {
            if (Session["id"] == null)
            {
                Session["timeout"] = "Session Expired.....";
                return RedirectToAction("Index", "Login");
            }

            else
            {
                if (searchTerm != null)
                {
                page = 1;
                }
                else
                {
                searchTerm = currentFilter;
                }

                ViewBag.CurrentFilter = searchTerm;
                Learning_strategy_class obj = new Learning_strategy_class();
                IEnumerable<Snap97_NS_CS.Course> courseobj = obj.GetCourses().Where(x=>searchTerm==null||x.course_name.ToUpper().StartsWith(searchTerm.ToUpper()));
                List<Course> c = new List<Course>();
                foreach (var i in courseobj)
                { 
                    c.Add(
                        new Course()
                        {
                        course_description =i.course_description,
                        course_duration =i.course_duration,
                        course_id =i.course_id,
                        course_name =i.course_name
                        });
                }
            
                int pageSize = 5;
                int pageNumber = (page ?? 1);
                return View(c.ToPagedList(pageNumber, pageSize));
            }
        }
        [HandleError]
        public ActionResult Details(int id)
        {
            if (Session["id"] == null)
            {
                Session["timeout"] = "Session Expired.....";
                return RedirectToAction("Index", "Login");
            }

            else
            {
                Learning_strategy_class obj = new Learning_strategy_class();
                Snap97_NS_CS.Course courseobj = obj.GetCourses().Where(x => x.course_id == id).FirstOrDefault();
                Course c = new Course()
                {
                    course_description = courseobj.course_description,
                    course_duration = courseobj.course_duration,
                    course_id = courseobj.course_id,
                    course_lvl = (course_levels)courseobj.course_lvl,
                    course_name = courseobj.course_name
                };
                ViewBag.name = "Level of Course";
                ViewBag.value = c.course_lvl;
                return View(c);
            }
        }
    }
}