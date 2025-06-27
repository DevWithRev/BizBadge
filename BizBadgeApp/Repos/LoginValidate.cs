
using BizBadgeApp.Models;
using Microsoft.Data.SqlClient;

namespace BizBadgeApp.Repos
{
    public class LoginValidate
    {

        public bool IsUserExist(LoginModel user, string connection)
        {
            using (SqlConnection con = new SqlConnection(connection))
            {
                string query = "SELECT 1 FROM USERS WHERE EMAIL = @Email And Password = @Password";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@Password", user.Password);

                con.Open();
                object result = cmd.ExecuteScalar();
                con.Close();
                return result != null;
            }
}

    }
}
