using BizBadgeApp.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BizBadgeApp.Repos
{
    public class StudentsRepo
    {
        public List<StudentModel> GetClassWiseStudentData(int ClassId, string Connecton)
        {
            List<StudentModel> students = new List<StudentModel>();
            using (SqlConnection conn = new SqlConnection(Connecton))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("UspClassWiseStudentData", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@classId", ClassId);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        StudentModel student = new StudentModel();
                        student.Id = Convert.ToInt32(reader["StudentID"]);
                        student.FirstName = reader["FirstName"].ToString();
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

        public StudentModel GetStudentById(int id, string conn)
        {
            using (SqlConnection connection = new SqlConnection(conn))
            {
                SqlCommand command = new SqlCommand("Usp_GetStudentById", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@StudentID", id);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    StudentModel student = new StudentModel
                    {
                        Id = Convert.ToInt32(reader["StudentID"]),
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        FatherName = reader["FatherName"].ToString(),
                        MotherName = reader["MotherName"].ToString(),
                        MobileNumber = reader["MobileNumber"].ToString(),
                        Age = Convert.ToInt32(reader["Age"]),
                        Address = reader["Address"] == DBNull.Value ? null : reader["Address"].ToString(),
                        ClassId = Convert.ToInt32(reader["ClassID"])
                        // ClassName is not returned by the procedure; remove or fetch separately
                    };

                    return student;
                }
            }

            return null; // If student not found
        }

        public int UpdateStudent(StudentModel student,string con) 
        {
            using(SqlConnection conn = new SqlConnection(con)) 
            {
                SqlCommand command = new SqlCommand("Usp_UpdateStudent", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@StudentID", student.Id);
                command.Parameters.AddWithValue("@FirstName", student.FirstName);
                command.Parameters.AddWithValue("@LastName", student.LastName);
                command.Parameters.AddWithValue("@FatherName", student.FatherName);
                command.Parameters.AddWithValue("@MotherName", student.MotherName);
                command.Parameters.AddWithValue("@MobileNumber", student.MobileNumber);
                command.Parameters.AddWithValue("@Age", student.Age);
                command.Parameters.AddWithValue("@Address", student.Address);
                command.Parameters.AddWithValue("@ClassID", student.ClassId);
                conn.Open();
                int result = command.ExecuteNonQuery();
                if (result > 0)
                {
                    return result; // Return updated student
                }
                else
                {
                    return 0; // Update failed
                }
            }
        }

        public int DeleteStudent(int id,string con) 
        {
            using(SqlConnection conn = new SqlConnection(con)) 
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM STUDENTS WHERE STUDENTID =@StudentId", conn);
                    cmd.Parameters.AddWithValue("@StudentId", id);
                    conn.Open();
                    int result = cmd.ExecuteNonQuery();
                    conn.Close();
                    return result;
                }
                catch ( Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return 0;
                }
            }
          
        }
    }

}

