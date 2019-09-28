using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LearnerPlatform.Models
{
    public class Courses_in_path
    {
        public enum course_levels
        {
            beginner = 1,
            intermediate = 2,
            advanced = 3
        }
        public enum Course_status
        {
            enrolled = 1,
            pending = 2,
            completed = 3

        }

            [Display(Name = "ID")]
            public int course_id { get; set; }
            [Display(Name = "Course")]
            public string course_name { get; set; }
            [Display(Name = "Duration")]
            public int course_duration { get; set; }
            [Display(Name = "Description")]
            public string course_description { get; set; }
            [Display(Name = "Course Level")]
            public course_levels course_lvl { get; set; }

            public Course_status CourseStatus { get; set; }

    }
}