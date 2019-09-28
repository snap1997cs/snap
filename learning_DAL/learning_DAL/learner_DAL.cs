using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Snap97_NS_CS;

namespace learning_DAL
{
    public class learner_DAL
    {
        public string connectionstring { get; set; }
        public SqlConnection conn { get; set; }
        public SqlCommand cmd { get; set; }
        public SqlDataReader datareader { get; set; }

        public learner_DAL()
        {
            connectionstring = ConfigurationManager.ConnectionStrings["Snap"].ConnectionString;
            conn = new SqlConnection(connectionstring);
        }
        //1 Tarun
        //  delete from LEARNING_PATH_DETAILS where COURSE_ID is null
        public void Delete_Null_Path()
        {

            cmd = new SqlCommand();
            cmd.CommandText = "delete from LEARNING_PATH_DETAILS where COURSE_ID is null";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            conn.Open();
            cmd.ExecuteScalar();
            conn.Close();
            SqlCommand cmd1 = new SqlCommand();
            cmd1.CommandText = "delete from LEARNING_PATH_MASTER where Path_name is null";
            cmd1.CommandType = CommandType.Text;
            cmd1.Connection = conn;
            conn.Open();
            cmd1.ExecuteScalar();
            conn.Close();
        }
        public List<Course> get_all_course()
        {
            cmd = new SqlCommand();
            cmd.CommandText = "get_all_course";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;
            conn.Open();
            datareader = cmd.ExecuteReader();
            List<Course> course_obj = new List<Course>();
            while (datareader.Read())
            {

                course_obj.Add(
                    new Course()
                    {
                        course_id = (int)datareader["course_id"],
                        course_name = (string)datareader["COURSE_NAME"],
                        course_lvl = (course_levels)datareader["COURSE_LVL"],
                        course_duration = (int)datareader["COURSE_DURATION"],
                        course_description = (string)datareader["COURSE_DESCRIPTION"]
                    });
            }
            conn.Close();
            return course_obj;

        }
        public void Add_account(string name, string email, string grade, string role, string password)
        {
            cmd = new SqlCommand();
            cmd.CommandText = "Add_account";
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter Name = new SqlParameter();
            SqlParameter Email = new SqlParameter();
            SqlParameter Grade = new SqlParameter();
            SqlParameter Role = new SqlParameter();
            SqlParameter Password = new SqlParameter();
            Name.ParameterName = "@name";
            Email.ParameterName = "@email";
            Grade.ParameterName = "@grade";
            Role.ParameterName = "@role";
            Password.ParameterName = "@password";
            Name.Value = name;
            Email.Value = email;
            Grade.Value = grade;
            Role.Value = role;
            Password.Value = password;
            cmd.Parameters.Add(Name);
            cmd.Parameters.Add(Email);
            cmd.Parameters.Add(Grade);
            cmd.Parameters.Add(Role);
            cmd.Parameters.Add(Password);
            cmd.Connection = conn;
            conn.Open();
            cmd.ExecuteScalar();
            conn.Close();
        }



        public int get_course_id_by_name(string cname)
        {

            cmd = new SqlCommand();
            cmd.CommandText = "select COURSE_ID from course where COURSE_NAME=@cname";
            cmd.CommandType = CommandType.Text;
            SqlParameter coursename = new SqlParameter();
            coursename.ParameterName = "@cname";
            coursename.Value = cname;
            cmd.Parameters.Add(coursename);
            cmd.Connection = conn;
            conn.Open();
            int cid =(int)cmd.ExecuteScalar();
            conn.Close();
            return cid;
        }
        public List<int> get_course_by_path(int PATHID)
        {
            cmd = new SqlCommand();
            cmd.CommandText = "get_course_by_path";
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter pathId = new SqlParameter();
            pathId.ParameterName = "@PATHID";
            pathId.Value = PATHID;
            cmd.Parameters.Add(pathId);
            cmd.Connection = conn;
            conn.Open();
            List<int> data = new List<int>();
            datareader= cmd.ExecuteReader();
            while( datareader.Read())
            {
                data.Add((int)datareader["Course_id"]);
            }
            conn.Close();
            return data;
        }
        public void name_my_path(int pathid,string pathname)
        {
            cmd = new SqlCommand();
            cmd.CommandText = "name_my_path";
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter PathId = new SqlParameter();
            PathId.ParameterName = "@PATHID";
            PathId.Value = pathid;
            cmd.Parameters.Add(PathId);
            SqlParameter PathName = new SqlParameter();
            PathName.ParameterName = "@pathname";
            PathName.Value = pathname;
            cmd.Parameters.Add(PathName);
            
            cmd.Connection = conn;
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();  
        }

        //2
        public List<Course> get_course_details_by_id(int course_id)
        {
            cmd = new SqlCommand();
            cmd.CommandText = "get_course_details_by_id";
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter courseid = new SqlParameter();
            courseid.ParameterName = "@course_id";
            courseid.Value = course_id;
            cmd.Parameters.Add(courseid);
            cmd.Connection = conn;
            conn.Open();
            datareader = cmd.ExecuteReader();
            List<Course> course_obj = new List<Course>();
            while (datareader.Read())
            {

                course_obj.Add(
                    new Course()
                    {
                        course_id = (int)datareader["course_id"],
                        course_name = (string)datareader["COURSE_NAME"],
                        course_lvl = (course_levels)datareader["COURSE_LVL"],
                        course_duration = (int)datareader["COURSE_DURATION"],
                        course_description = (string)datareader["COURSE_DESCRIPTION"]
                    });
            }
            conn.Close();
            return course_obj;

        }




        ////3
        public int add_course_to_lp(int PATHID, int course_id)
        {
            cmd = new SqlCommand();
            cmd.CommandText = "add_course_to_lp";
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter pathId = new SqlParameter();
            pathId.ParameterName = "@PATHID";
            pathId.Value = PATHID;
            cmd.Parameters.Add(pathId);
            cmd.Connection = conn;

            SqlParameter courseId = new SqlParameter();
            courseId.ParameterName = "@courseid";
            courseId.Value = course_id;
            cmd.Parameters.Add(courseId);


            conn.Open();
            int data = (int)cmd.ExecuteNonQuery();
            conn.Close();
            return data;
        }




        ////4
        public int create_learning_path(int learnerid)
        {
            cmd = new SqlCommand();
            cmd.CommandText = "create_learning_path";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;
            

            SqlParameter learnerId = new SqlParameter();
            learnerId.ParameterName = "@learnerid";
            learnerId.Value = learnerid;
            cmd.Parameters.Add(learnerId);
            //SqlParameter pathname = new SqlParameter();
            //pathname.ParameterName = "@pathName";
            //pathname.Value = PathName;
            //cmd.Parameters.Add(learnerId);


            conn.Open();
            int data = cmd.ExecuteNonQuery();
            conn.Close();
            return data;
        }
        public int get_new_pathid(int learnerid)
        {
            int pathid=0;
            cmd = new SqlCommand();
            cmd.CommandText = "select LEARNING_PATH_DETAILS.LEARNING_PATH_ID from LEARNING_PATH_DETAILS inner join LEARNING_PATH_MASTER on LEARNING_PATH_DETAILS.LEARNING_PATH_ID = LEARNING_PATH_MASTER.LEARNING_PATH_ID where LEARNING_PATH_DETAILS.COURSE_ID is null and LEARNING_PATH_MASTER.LEARNER_ID=@learnerid;";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            SqlParameter learnerId = new SqlParameter();
            learnerId.ParameterName = "@learnerid";
            learnerId.Value = learnerid;
            cmd.Parameters.Add(learnerId);
            conn.Open();
            try
            {
                pathid = (int)cmd.ExecuteScalar();
            }
            catch
            { }
            conn.Close();
            return pathid;
        }


        //5
        public int enroll_for_course(int course_id, int learner_id, int course_status, DateTime completion_date)
        {
            cmd = new SqlCommand();
            cmd.CommandText = "enroll_for_course";
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter courseid = new SqlParameter();
            courseid.ParameterName = "@course_id";
            courseid.Value = course_id;
            cmd.Parameters.Add(courseid);
            cmd.Connection = conn;

            SqlParameter learnerId = new SqlParameter();
            learnerId.ParameterName = "@learner_id";
            learnerId.Value = learner_id;
            cmd.Parameters.Add(learnerId);

            SqlParameter coursestatus = new SqlParameter();
            coursestatus.ParameterName = "@course_status";
            coursestatus.Value = course_status;
            cmd.Parameters.Add(coursestatus);

            SqlParameter completiondate = new SqlParameter();
            completiondate.ParameterName = "@completion_date";
            completiondate.Value = completion_date;
            cmd.Parameters.Add(completiondate);


            conn.Open();
            int data = (int)cmd.ExecuteNonQuery();
            conn.Close();
            return data;
        }

        //6 Pavithra
        public List<Learner> get_learner_det_by_id(int learner_id)
        {
            cmd = new SqlCommand();
            cmd.CommandText = "get_learner_det_by_id";
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter courseid = new SqlParameter();
            courseid.ParameterName = "@learner_id";
            courseid.Value = learner_id;
            cmd.Parameters.Add(courseid);
            cmd.Connection = conn;
            conn.Open();
            datareader = cmd.ExecuteReader();
            List<Learner> learner_obj = new List<Learner>();
            while (datareader.Read())
            {

                learner_obj.Add(
                    new Learner()
                    {
                        learner_id = (int)datareader["learner_ID"],
                        learner_name = (string)datareader["learner_NAME"],
                        learner_grade = (string)datareader["learner_GRADE"],
                        learner_roll = (string)datareader["learner_ROLE"],
                        learner_email = (string)datareader["learner_EMAIL"],
                        learner_pass=(string)datareader["learner_pass"]
                    });
            }
            conn.Close();
            return learner_obj;
        }
        //7
        public List<Learning_path_masted> get_learning_path(int learnerid)
        {
            List<Learning_path_masted> lp_obj = new List<Learning_path_masted>();
            cmd = new SqlCommand();
            cmd.CommandText = "get_learning_path";
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter courseid = new SqlParameter();
            courseid.ParameterName = "@learnerid";
            courseid.Value = learnerid;
            cmd.Parameters.Add(courseid);
            cmd.Connection = conn;
            conn.Open();
            datareader = cmd.ExecuteReader();
            while (datareader.Read())
            {

                lp_obj.Add(
                             new Learning_path_masted()
                             {
                                 creation_date = (DateTime)datareader["CREATION_DATE"],
                                 path_id = (int)datareader["LEARNING_PATH_ID"],
                                 learner_id = (int)datareader["LEARNER_ID"],
                                 pathName = datareader["Path_name"].ToString()
                             }
                    );
            }
            conn.Close();
            return lp_obj;
        }

        //8 mallik

        public List<Learner> get_all_learners()
        {
            cmd = new SqlCommand();
            cmd.CommandText = "get_all_learners";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;
            conn.Open();
            datareader = cmd.ExecuteReader();
            List<Learner> learner_obj = new List<Learner>();
            while (datareader.Read())
            {

                learner_obj.Add(
                    new Learner()
                    {
                        learner_id = (int)datareader["learner_ID"],
                        learner_name = (string)datareader["learner_NAME"],
                        learner_email = (string)datareader["learner_EMAIL"],
                        learner_grade = (string)datareader["learner_GRADE"],
                        learner_roll = (string)datareader["learner_ROLE"],
                        learner_pass = (string)datareader["learner_PASS"]
                    });
            }
            conn.Close();
            return learner_obj;

        }

        ////9
        // public List<Feedback> get_feedback(int course_id)
        // {
        //     cmd = new SqlCommand();
        //     cmd.CommandText = "get_feedback";
        //     cmd.CommandType = CommandType.StoredProcedure;
        //     SqlParameter courseid = new SqlParameter();
        //     courseid.ParameterName = "@course_id";
        //     courseid.Value = course_id;
        //     cmd.Parameters.Add(courseid);
        //     cmd.Connection = conn;
        //     conn.Open();
        //     datareader = cmd.ExecuteReader();
        //     List<Feedback> course_obj = new List<Feedback>();
        //     while (datareader.Read())
        //     {

        //         course_obj.Add(
        //             new Feedback
        //             {
        //                 learner_id = (int)datareader["learner_ID"],
        //                 course_id = (int)datareader["COURSE_ID"],
        //                 feedback_desc = (string)datareader["FEEDBACK"]
        //             });
        //     }
        //     conn.Close();
        //     return course_obj;
        // }

        //10
        public List<Feedback> get_avg_feedback(int course_id)
        {
            cmd = new SqlCommand();
            cmd.CommandText = "get_avg_feedback";
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter courseid = new SqlParameter();
            courseid.ParameterName = "@course_id";
            courseid.Value = course_id;
            cmd.Parameters.Add(courseid);
            cmd.Connection = conn;
            conn.Open();
            datareader = cmd.ExecuteReader();
            List<Feedback> course_obj = new List<Feedback>();
            while (datareader.Read())
            {

                course_obj.Add(
                    new Feedback
                    {
                        rating = (int)datareader["RATING"]
                    });
            }
            conn.Close();
            return course_obj;
        }

        // //11 Pavithra
        // public List<Course_recommendation> get_recommendation(int learnid)
        // {
        //     cmd = new SqlCommand();
        //     cmd.CommandText = "get_recommendation";
        //     cmd.CommandType = CommandType.StoredProcedure;
        //     SqlParameter LID = new SqlParameter();
        //     LID.ParameterName = "@learnerid";
        //     LID.Value = learnid;
        //     LID.Direction = ParameterDirection.Input;
        //     cmd.Parameters.Add(LID);


        //     cmd.Connection = conn;
        //     conn.Open();
        //     datareader = cmd.ExecuteReader();
        //     List<Course_recommendation> recommdation_obj = new List<Course_recommendation>();
        //     while (datareader.Read())
        //     {

        //         recommdation_obj.Add(
        //             new Course_recommendation()
        //             {
        //                 recommendation_to = (int)datareader["RECOMMENDED_TO"],

        //             });
        //     }
        //     conn.Close();
        //     return recommdation_obj;
        // }

        // //12 Nikitha
        //public int SET_STATUS(int learner_id, int course_id, int course_status)
        //{
        //    cmd = new SqlCommand();
        //    cmd.CommandText = "SET_STATUS";
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    SqlParameter courseid = new SqlParameter();
        //    courseid.ParameterName = "@course_id";
        //    courseid.Value = course_id;
        //    cmd.Parameters.Add(courseid);
        //    cmd.Connection = conn;

        //    SqlParameter learnerId = new SqlParameter();
        //    learnerId.ParameterName = "@learner_id";
        //    learnerId.Value = learner_id;
        //    cmd.Parameters.Add(learnerId);

        //    SqlParameter coursestatus = new SqlParameter();
        //    coursestatus.ParameterName = "@course_status";
        //    coursestatus.Value = course_status;
        //    cmd.Parameters.Add(coursestatus);
        //    conn.Open();
        //    int data = (int)cmd.ExecuteNonQuery();
        //    conn.Close();
        //    return data;
        //}


        // //13 Set_FEEDBACK Stored procedure method
        //public int SET_FEEDBACK(int empid, int courseid, string comments, int rating)
        //{
        //    cmd = new SqlCommand();
        //    cmd.CommandText = "SET_FEEDBACK";
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    SqlParameter emp_id = new SqlParameter();
        //    emp_id.ParameterName = "@empid";
        //    emp_id.Value = empid;
        //    cmd.Parameters.Add(emp_id);
        //    cmd.Connection = conn;

        //    SqlParameter course_id = new SqlParameter();
        //    course_id.ParameterName = "@courseid";
        //    course_id.Value = courseid;
        //    cmd.Parameters.Add(course_id);

        //    SqlParameter Comments = new SqlParameter();
        //    Comments.ParameterName = "@comments";
        //    Comments.Value = comments;
        //    cmd.Parameters.Add(Comments);

        //    SqlParameter Rating = new SqlParameter();
        //    Rating.ParameterName = "@rating";
        //    Rating.Value = rating;
        //    cmd.Parameters.Add(Rating);

        //    conn.Open();
        //    int data = (int)cmd.ExecuteNonQuery();
        //    conn.Close();
        //    return data;
        //}

        ////14 set_recommendation Stored procedure method
        //public int set_recomendation(int recid, int recby, int recto, string cmnt, int courseid)
        //{
        //    cmd = new SqlCommand();
        //    cmd.CommandText = "set_recomendation";
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    SqlParameter rec_id = new SqlParameter();
        //    rec_id.ParameterName = "@recid";
        //    rec_id.Value = recid;
        //    cmd.Parameters.Add(rec_id);
        //    cmd.Connection = conn;

        //    SqlParameter rec_by = new SqlParameter();
        //    rec_by.ParameterName = "@recby";
        //    rec_by.Value = recby;
        //    cmd.Parameters.Add(rec_by);

        //    SqlParameter rec_to = new SqlParameter();
        //    rec_to.ParameterName = "@recto";
        //    rec_to.Value = recto;
        //    cmd.Parameters.Add(rec_to);

        //    SqlParameter Comment = new SqlParameter();
        //    Comment.ParameterName = "@cmnt";
        //    Comment.Value = cmnt;
        //    cmd.Parameters.Add(Comment);

        //    SqlParameter course_id = new SqlParameter();
        //    course_id.ParameterName = "@courseid";
        //    course_id.Value = courseid;
        //    cmd.Parameters.Add(course_id);

        //    conn.Open();
        //    int data = (int)cmd.ExecuteNonQuery();
        //    conn.Close();
        //    return data;
        //}
        //15

        //public int SET_STATUS(int learner_id, int course_id, int course_status)
        //{
        //    cmd = new SqlCommand();
        //    cmd.CommandText = "SET_STATUS";
        //    cmd.CommandType = CommandType.StoredProcedure;

        //    SqlParameter learnerId = new SqlParameter();
        //    learnerId.ParameterName = "@learnerid";
        //    learnerId.Value = learner_id;
        //    learnerId.Direction = ParameterDirection.Input;
        //    cmd.Parameters.Add(learnerId);

        //    SqlParameter courseid = new SqlParameter();
        //    courseid.ParameterName = "@COURSEID";
        //    courseid.Value = course_id;
        //    courseid.Direction = ParameterDirection.Input;
        //    cmd.Parameters.Add(courseid);
        //    cmd.Connection = conn;         

        //    SqlParameter coursestatus = new SqlParameter();
        //    coursestatus.ParameterName = "@STATUS";
        //    coursestatus.Value = course_status;
        //    coursestatus.Direction = ParameterDirection.Input;
        //    cmd.Parameters.Add(coursestatus);
        //    conn.Open();

        //    int recordcount = (int)cmd.ExecuteNonQuery();

        //    conn.Close();
        //    return recordcount;
        //}
        //  Get_status Stored procedure method
        //public List<Course_enrolled> Get_Status(int learner_id, int course_id, out int status)
        //{
        //    cmd = new SqlCommand();
        //    cmd.CommandText = "Get_Status";
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    SqlParameter learnerID = new SqlParameter();

        //    learnerID.ParameterName = "@learnerid";
        //    learnerID.Value = learnerID;
        //    learnerID.Direction = ParameterDirection.Input;
        //    cmd.Parameters.Add(learnerID);

        //    SqlParameter courseID = new SqlParameter();
        //    courseID.ParameterName = "@COURSEID";
        //    courseID.Value = courseID;
        //    courseID.Direction = ParameterDirection.Input;
        //    cmd.Parameters.Add(courseID);


        //    SqlParameter Status = new SqlParameter();
        //    Status.ParameterName = "@STATUS";
        //    Status.Direction = ParameterDirection.Output;
        //    cmd.Parameters.Add(Status);
        //    cmd.Connection = conn;
        //    conn.Open();
        //    datareader = cmd.ExecuteReader();
        //    List<Course_enrolled> status_obj = new List<Course_enrolled>();
        //    status = int.Parse((datareader["Status"]).ToString());
        //    //status_obj.Add(
        //    //    new Course_enrolled()
        //    //    {
        //    //        learner_id = learner_id,
        //    //        course_id = course_id,
        //    //        status = status

        //    //    }
        //    //    ) ;
        //    conn.Close();
        //    return status_obj;
    }
    }

