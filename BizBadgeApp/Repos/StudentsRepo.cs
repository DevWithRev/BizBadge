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
            
            
            
            
            
            return new List<StudentModel>();
        }
    }
}
