using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LearnerPlatform.Models
{
    namespace Snap97_NS_CS
    {
        public class Learning_path_masted
        {
            [Display(Name ="Paths")]
            public int path_id { get; set; }
            [Display(Name ="Path Name")]
            public string pathname { get; set; }
            public int learner_id { get; set; }
            [Display(Name="Created On")]
            [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
            public DateTime creation_date { get; set; }

        }
    }
}