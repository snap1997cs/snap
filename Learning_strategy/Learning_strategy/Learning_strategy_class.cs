using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Snap97_NS_CS;
using learning_DAL;

namespace Learning_strategy
{
    public class Learning_strategy_class
    {
        public learner_DAL obj { get; set; }
        public Learning_strategy_class()
        {
            obj = new learner_DAL();
        }

        public List<Course> GetCourses()
        {
            List<Course> courses = obj.get_all_course();
            return courses;
        }

        //public List<Course> Get_course_details_by_id(int course_id)
        //{
        //    List<Course> courses = obj.get_course_details_by_id(course_id);
        //    return courses;
        //}

        public int Add_course_to_lp(int PATHID, int course_id)
        {
            int data = obj.add_course_to_lp(PATHID, course_id);
            return data;
        }

        public int Create_learning_path(int learnerid)
        {
            int data = obj.create_learning_path(learnerid);
            return data;
        }
        public int Get_new_pathid(int LearnerId)
        {
            int data = obj.get_new_pathid(LearnerId);
            return data;
        }
        public void Name_my_path(int pathid,string pathname)
        {
            obj.name_my_path(pathid, pathname);
            
        }
        public List<int> Get_course_by_path(int pathid)
        {
            return obj.get_course_by_path(pathid);
        }

        //public int Get_course_id_by_name(string Cname)
        //{
        //    int cid = obj.get_course_id_by_name(Cname);
        //    return cid;
        //}
        public void Add_Account(string name, string email, string grade, string role, string password)
        {
            obj.Add_account(name, email, grade, role, password);
        }
        //public int Enroll_for_course(int course_id, int learner_id, int course_status, DateTime completion_date)
        //{
        //    int data = obj.enroll_for_course(course_id, learner_id, course_status, completion_date);
        //    return data;
        //}
        //public List<Learner> Get_learner_det_by_id(int learner_id)
        //{

        //    List<Learner> Learner = obj.get_learner_det_by_id(learner_id);
        //    return Learner;
        //}

        public List<Learning_path_masted> Get_learning_path(int learnerid)
        {
            List<Learning_path_masted> learner_Paths = obj.get_learning_path(learnerid);
            return learner_Paths;
        }
        public List<Learner> Get_all_learners()
        {
            List<Learner> learners = obj.get_all_learners();
            return learners;
        }
        public void delete_Null_Path()
        {
            obj.Delete_Null_Path();
        }
    }
}
