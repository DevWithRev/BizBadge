using BizBadgeApp.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BizBadgeApp.Repos
{
    public class StudentsRepo
    {
        public List<StudentModel> GetClassWiseStudentData(int ClassId,string Connecton) 
        {
            List<StudentModel> students = new List<StudentModel>();
            using(SqlConnection conn = new SqlConnection(Connecton)) 
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("UspClassWiseStudentData",conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@classId", ClassId);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read()) 
                    {
                        StudentModel student = new StudentModel();
                        student.Id = Convert.ToInt32(reader["StudentID"]);
                        student.FirstName =reader["FirstName"].ToString();
                        student.LastName = reader["LastName"].ToString();
                        student.FatherName = reader["FatherName"].ToString();
                        student.MotherName = reader["MotherName"].ToString();
                        student.Age = Convert.ToInt32(reader["age"]);
                        student.MobileNumber = reader["MobileNumber"].ToString();
                        student.Address = reader["Address"] == DBNull.Value ? null : reader["Address"].ToString();
                        student.ClassId = Convert.ToInt32(reader["ClassId"]);
                        student.ClassName = reader["ClassName"].ToString();
                        students.Add(student);
                    }
                }
                catch (SqlException ex) 
                {
                    Console.WriteLine(ex.Message);
                }
                return students;
            }   
            
        }
        public int NewStudetData(StudentModel newStudent, string conn)
        {
            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("Usp_InsertStudent", con);
                cmd.CommandType = CommandType.StoredProcedure;

                // Add parameters
                cmd.Parameters.AddWithValue("@FirstName", newStudent.FirstName);
                cmd.Parameters.AddWithValue("@LastName", newStudent.LastName);
                cmd.Parameters.AddWithValue("@FatherName", newStudent.FatherName);
                cmd.Parameters.AddWithValue("@MotherName", newStudent.MotherName);
                cmd.Parameters.AddWithValue("@MobileNumber", newStudent.MobileNumber);
                cmd.Parameters.AddWithValue("@Age", newStudent.Age);
                cmd.Parameters.AddWithValue("@Address", newStudent.Address);
                cmd.Parameters.AddWithValue("@ClassID", newStudent.ClassId); // Make sure this is in your model

                con.Open();
                int result = cmd.ExecuteNonQuery(); // returns number of rows affected
                return result;
            }
        }

    }

}
