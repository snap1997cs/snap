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
    }
}

