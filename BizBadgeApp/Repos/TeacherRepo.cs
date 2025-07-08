namespace BizBadgeApp.Repos
{
    public class TeacherRepo
    {
        public List<TeacherRepo> GetTeachers(string connection)
        {
            List<TeacherRepo> teachers = new List<TeacherRepo>();
            using (SqlConnection con = new SqlConnection(connection))
            {
                string query = "SELECT * FROM Teachers";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    TeacherRepo teacher = new TeacherRepo
                    {
                        // Assuming TeacherRepo has properties like Id, Name, Subject, etc.
                        // Id = reader.GetInt32(0),
                        // Name = reader.GetString(1),
                        // Subject = reader.GetString(2)
                    };
                    teachers.Add(teacher);
                }
                con.Close();
            }
            return teachers;
        }
    }
}
