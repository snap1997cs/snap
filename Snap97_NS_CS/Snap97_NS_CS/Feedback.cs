using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snap97_NS_CS
{
    public class Feedback
    {
        public int learner_id { get; set; }

        public int course_id { get; set; }
        public string feedback_desc { get; set; }

        public int rating { get; set; }

        public DateTime feedback_date { get; set; }
    }
}
