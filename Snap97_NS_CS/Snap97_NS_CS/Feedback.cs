using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snap97_NS_CS
{
    public enum FeedBack_Desc
    {
        Excellent=1,
        Good=2,
        Average=3
    }
    public class Feedback
    {
        public int learner_id { get; set; }

        public int course_id { get; set; }
        public FeedBack_Desc feedback_desc { get; set; }

        public int rating { get; set; }

        public DateTime feedback_date { get; set; }
    }
}
