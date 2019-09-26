
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LearnerPlatform.Models
{
    namespace Snap97_NS_CS
    {
        public enum course_status
        {
            enrolled = 1,
            pending = 2,
            completed = 3

        }
        public class Course_enrolled
        {
            public int course_id { get; set; }
            public int learner_id { get; set; }
            public DateTime enrolled_date { get; set; }
            public DateTime completion_date { get; set; }
            //public course_status status { get; set; }
        }
    }
}