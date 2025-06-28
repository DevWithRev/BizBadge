using BizBadgeApp.Models;
using Microsoft.Data.SqlClient;

namespace BizBadgeApp.Repos
{
    public class LoginValidate
    {
        public LoginModel IsUserExist(LoginModel user, string connection)
        {
            using (SqlConnection con = new SqlConnection(connection))
            {
                
                string query = "SELECT * FROM USERS WHERE EMAIL = @Email AND Password = @Password";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@Password", user.Password);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                LoginModel foundUser = null;

                if (reader.Read())
                {
                    foundUser = new LoginModel
                    {
                        Email = reader["Email"].ToString(),
                        Name = reader["Name"].ToString(),
                    };
                }

                con.Close();
                return foundUser; // returns null if not found
            }
        }
    }
}
