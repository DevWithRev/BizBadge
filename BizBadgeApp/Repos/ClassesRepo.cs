using BizBadgeApp.Models;
using Microsoft.Data.SqlClient;

namespace BizBadgeApp.Repos
{
    public class ClassesRepo
    {
        public List<ClassesModel> GetAllClasses(string connectionString)
        {
            List<ClassesModel> classes = new List<ClassesModel>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT ClassID, ClassName FROM Classes", conn);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        ClassesModel classModel = new ClassesModel
                        {
                            ClassID = reader.GetInt32(0),
                            ClassName = reader.GetString(1)
                        };
                        classes.Add(classModel);
                    }

                    reader.Close(); // Optional but good practice
                }
                catch (Exception ex)
                {
                    // Log or handle the exception here
                    throw new Exception("Error fetching class data", ex);
                }
            }

            return classes;
        }

    }
}
