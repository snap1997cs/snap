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
        //1 tarun
        public List<Course> GetCourses()
        {
            List<Course> courses = obj.get_all_course();
            return courses;
        }
        //2
        public List<Course> Get_course_details_by_id(int course_id)
        {
            List<Course> courses = obj.get_course_details_by_id(course_id);
            return courses;
        }
        //3
        public int Add_course_to_lp(int PATHID, int course_id)
        {
            int data = obj.add_course_to_lp(PATHID, course_id);
            return data;
        }
        ////4
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
        public List<int> Get_course_by_path(int pathid)
        {
            return obj.get_course_by_path(pathid);
        }

        public int Get_course_id_by_name(string Cname)
        {
            int cid = obj.get_course_id_by_name(Cname);
            return cid;
        }

        //5
        public int Enroll_for_course(int course_id, int learner_id, int course_status, DateTime completion_date)
        {
            int data = obj.enroll_for_course(course_id, learner_id, course_status, completion_date);
            return data;
        }
        //6 Pavithra
        public List<Learner> Get_learner_det_by_id(int learner_id)
        {

            List<Learner> Learner = obj.get_learner_det_by_id(learner_id);
            return Learner;
        }
        ////7
        public List<Learning_path_masted> Get_learning_path(int learnerid)
        {
            List<Learning_path_masted> learner_Paths = obj.get_learning_path(learnerid);
            return learner_Paths;
        }
        //8 Mallika
        public List<Learner> Get_all_learners()
        {
            List<Learner> learners = obj.get_all_learners();
            return learners;
        }
        ////9
        //public List<Feedback> GetFeedbacks(int course_id)
        //{
        //    List<Feedback> feedbacks = obj.get_feedback(course_id);
        //    return feedbacks;
        //}
        //10
        public List<Feedback> GetAvgFeedbacks(int course_id)
        {
            List<Feedback> feedbacks = obj.get_avg_feedback(course_id);
            return feedbacks;
        }
        ////11 Pavithra
        //public List<Course_recommendation> Get_recommendation(int learner_id)
        //{

        //    List<Course_recommendation> Learner = obj.get_recommendation(learner_id);
        //    return Learner;
        //}

        //12 Nikitha
        //public int Set_Status(int learner_id, int course_id, int course_status)
        //{
        //    int Enrolled = obj.SET_STATUS(learner_id, course_id, course_status);
        //    return Enrolled;
        //}

        //public List<Course_enrolled> Get_Status(int learner_id, int course_id, out int status)
        //{
        //    List<Course_enrolled> Enrolment_status = obj.Get_Status(learner_id, course_id, status);
        //    return Enrolment_status;
        //}
        //public int set_recommendation(int recid, int recby, int recto, string cmnt, int courseid)
        //{
        //    int Recommend = obj.set_recomendation(recid, recby, recto, cmnt, courseid);
        //    return Recommend;
        //}
        //public int set_feedback(int empid, int courseid, string comments, int rating)
        //{
        //    int feedback = obj.SET_FEEDBACK(empid, courseid, comments, rating);
        //    return feedback;
        //}
        public void delete_Null_Path()
        {
            obj.Delete_Null_Path();
        }
    }
}
