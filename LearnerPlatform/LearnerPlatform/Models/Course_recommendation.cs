using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LearnerPlatform.Models
{
    namespace Snap97
    {
        public class Course_recommendation
        {
            public int rec_id { get; set; }
            public int course_id { get; set; }
            public int recommmendation_by { get; set; }
            public int recommendation_to { get; set; }
            public DateTime rec_date { get; set; }
        }
    }
}