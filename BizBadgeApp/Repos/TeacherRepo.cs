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

        public int  Delete(int id, string connection)
        {
            using (SqlConnection con = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("Usp_DeleteTeacher", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id",id);
                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                con.Close();
                return rowsAffected; // Return true if a row was deleted
            }
        }
    }
}
