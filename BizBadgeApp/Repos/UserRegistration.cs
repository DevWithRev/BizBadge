using BizBadgeApp.Models;
using Microsoft.Data.SqlClient;

namespace BizBadgeApp.Repos
{
    public class UserRegistration
    {
        public bool IsUserExist(string email, string phone, string connection)
        {
            using (SqlConnection con = new SqlConnection(connection))
            {
                string query = "SELECT 1 FROM USERS WHERE EMAIL = @Email And PhoneNo = @Phone";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Phone", phone);

                con.Open();
                object result = cmd.ExecuteScalar();
                con.Close();
                return result != null;
            }
        }
        public int InsertUserDetails(RegistrationModel user, string connection)
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                string query = "INSERT INTO USERS (Name, Email, PhoneNo, Password) VALUES (@Name, @Email, @PhoneNo, @Password)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", user.Name);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@PhoneNo", user.PhoneNumber);
                cmd.Parameters.AddWithValue("@Password", user.Password); 
                conn.Open();
                int result = cmd.ExecuteNonQuery();
                conn.Close(); 
                return result;
            }
        }


    }
}
