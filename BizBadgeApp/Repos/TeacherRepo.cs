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

        public int UpadateTeacher(TeacherModel Teacher ,string con)
        {
            using (SqlConnection conn = new SqlConnection(con))
            {
                SqlCommand cmd = new SqlCommand("Usp_UpdateTeacher", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                // Add parameters
                cmd.Parameters.AddWithValue("@TeacherId", Teacher.TeacherId);
                cmd.Parameters.AddWithValue("@FullName", Teacher.FullName);
                cmd.Parameters.AddWithValue("@Email", Teacher.Email);
                cmd.Parameters.AddWithValue("@PhoneNumber", Teacher.PhoneNumber);
                cmd.Parameters.AddWithValue("@Gender", Teacher.Gender);
                cmd.Parameters.AddWithValue("@DateOfBirth", Teacher.DateOfBirth);
                cmd.Parameters.AddWithValue("@Address", Teacher.Address);
                cmd.Parameters.AddWithValue("@Qualification", Teacher.Qualification);
                cmd.Parameters.AddWithValue("@ExperienceYears", Teacher.ExperienceYears);
                cmd.Parameters.AddWithValue("@Department", Teacher.Department);
                cmd.Parameters.AddWithValue("@JoiningDate", Teacher.JoiningDate);
                cmd.Parameters.AddWithValue("@IsActive", Teacher.IsActive);

                conn.Open();
                int result =cmd.ExecuteNonQuery();
                conn.Close();
                return result;

            }
            return 0; // Return 0 if no rows were affected
        }

        //public int InsertTeacher(TeacherModel Teacher ,string con)
        //{
        //    using(SqlConnection conn = new SqlConnection(con))
        //    {
        //        SqlCommand cmd = new SqlCommand();

        //    }
        //}
    }
}
