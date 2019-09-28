using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace LearnerPlatform.Models
{
    namespace Snap97_NS_CS
    {
        public class Learner
        {
            
            public int learner_id { get; set; }



            
            [Display(Name = "Name")]
            [Required(ErrorMessage ="This Field is required")]
            public string learner_name { get; set; }


            
            [Display(Name = "Grade")]
            [Required(ErrorMessage = "This Field is required")]
            public string learner_grade { get; set; }



            
            [Display(Name = "Role")]
            [Required(ErrorMessage = "This Field is required")]
            public string learner_roll { get; set; }


            
            
            [Display(Name ="Email")]
            [Required(ErrorMessage = "Enter your MailID")]
            [DataType(DataType.EmailAddress)]
            public string learner_email { get; set; }





            [Required(ErrorMessage = "Enter password")]
            [Display(Name = "Password")]
            [DataType(DataType.Password)]
            public string learner_pass { get; set; }

        }
    }
}