
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Snap97_NS_CS
{
    public enum course_levels
    {
        beginner=1,
        intermediate=2,
        advanced=3
    }
    public class Course
    {
        public int course_id{ get; set; }
        public string course_name { get; set; }
        public int course_duration { get; set; }
        public string course_description { get; set; }
        public course_levels course_lvl { get; set; }
    }
}
