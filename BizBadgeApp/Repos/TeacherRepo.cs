using BizBadgeApp.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BizBadgeApp.Repos
{
    public class TeacherRepo
    {
        public List<TeacherModel> GetTeachers(string connection)
        {
            List<TeacherModel> teachers = new List<TeacherModel>();
            using (SqlConnection con = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("Usp_AllTeachers", con);
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    TeacherModel teacher = new TeacherModel
                    {
                            TeacherId = Convert.ToInt32(reader["TeacherId"]),
                            FullName = reader["FullName"]?.ToString(),
                            Email = reader["Email"]?.ToString(),
                            PhoneNumber = reader["PhoneNumber"]?.ToString(),
                            Gender = reader["Gender"]?.ToString(),
                            DateOfBirth = reader["DateOfBirth"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["DateOfBirth"]),
                            Address = reader["Address"]?.ToString(),
                            Qualification = reader["Qualification"]?.ToString(),
                            ExperienceYears = reader["ExperienceYears"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["ExperienceYears"]),
                            Department = reader["Department"]?.ToString(),
                            JoiningDate = reader["JoiningDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["JoiningDate"]),
                            IsActive = Convert.ToBoolean(reader["IsActive"]),
                            CreatedAt = Convert.ToDateTime(reader["CreatedAt"])

                    };
                    teachers.Add(teacher);
                }
                con.Close();
            }
            return teachers;
        }

        public TeacherModel GetTeacherDataById(int id ,string Conn)
        {
            using (SqlConnection conn = new SqlConnection(Conn))
            {
                SqlCommand cmd = new SqlCommand("Usp_GetTeacherDetilesById", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    TeacherModel teacher = new TeacherModel
                    {
                        TeacherId = Convert.ToInt32(reader["TeacherId"])


                    };
                }
            }
        }
    }
}
