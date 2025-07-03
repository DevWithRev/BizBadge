using BizBadgeApp.Models;
using Microsoft.Data.SqlClient;

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
    }
}
