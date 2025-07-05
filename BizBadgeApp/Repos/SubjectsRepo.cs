using BizBadgeApp.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BizBadgeApp.Repos
{
    public class SubjectsRepo
    {
        public List<SubjectModel> GetAllSubjects(string connectionString)
        {
            List<SubjectModel> subjectsList = new List<SubjectModel>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
               
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Subjects", conn))
                {
                    try
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                SubjectModel subject = new SubjectModel
                                {
                                    Id = reader.GetInt32(0),
                                    SubjectName = reader.GetString(1),
                                    SubjectCode = reader.GetString(2),
                                    SubjectDescription = reader.GetString(3)
                                };
                                subjectsList.Add(subject);
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        // Log the exception or handle it as needed
                        SubjectModel errorSubject = new SubjectModel
                        {
                            ErrorMessage = "Error retrieving subjects: " + ex.Message
                        };
                        subjectsList.Add(errorSubject);
                    }
                    return subjectsList;
                }
            }
        }
        public int AddSubject(SubjectModel subject, string con)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(con))
                {
                    SqlCommand cmd = new SqlCommand("Usp_AddSubject", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters without spaces in the name
                    cmd.Parameters.AddWithValue("@SubjectName", subject.SubjectName);
                    cmd.Parameters.AddWithValue("@SubjectCode", subject.SubjectCode);
                    cmd.Parameters.AddWithValue("@SubjectDescription", subject.SubjectDescription);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery(); // Returns number of rows inserted
                    return rowsAffected;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public SubjectModel GetSubjectDataById(int Id,string conn) 
        {
            SubjectModel subject = new SubjectModel();
            using(SqlConnection con = new SqlConnection(conn))
            {
                try
                {


                    SqlCommand cmd = new SqlCommand("Usp_GetSubjectDataById", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", Id);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        subject.Id = Convert.ToInt32(reader["Id"]);
                        subject.SubjectName = reader["SubjectName"].ToString();
                        subject.SubjectCode = reader["SubjectCode"].ToString();
                        subject.SubjectDescription = reader["SubjectDescription"].ToString();
                    }
                    return subject;
                }catch(Exception ex)
                {
                    subject.ErrorMessage = ex.Message;
                }
                return null;
            }

        }

    }
}
