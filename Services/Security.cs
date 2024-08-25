using System.Data;
using System.Data.SqlClient;

namespace insurance.Services
{
    public interface ISecurity
    {
        public string UserRegistration(string username, string password, string firstName,  string lastName, string email, string contactNo, string address);
        public string ValidateUser(string username, string password);
    }
    public class Security:ISecurity
    {
        public readonly IConfiguration config;
        string? role;
        
        public Security(IConfiguration config)
        {
            this.config = config;
        }

        public string UserRegistration(string username, string password, string firstName,
        string lastName, string email, string contactNo, string address )
        {

            SqlConnection conn = new SqlConnection();
            try
            {

                conn.ConnectionString = config.GetValue<string>("ConnectionStrings:Cstr");

                SqlCommand query = new SqlCommand();
                query.Connection = conn;
                query.CommandText = "exec Registration @username, @password, @firstName, @lastName, @Email, @ContactNo, @Address";

                // Add parameters
                query.Parameters.AddWithValue("@username", username);
                query.Parameters.AddWithValue("@password", password);
                query.Parameters.AddWithValue("@firstName", firstName);
                query.Parameters.AddWithValue("@lastName", lastName);
                query.Parameters.AddWithValue("@Email", email);
                query.Parameters.AddWithValue("@ContactNo", contactNo);
                query.Parameters.AddWithValue("@Address", address);

                conn.Open();

                object result = query.ExecuteScalar();
                if (result == null) { return "user added successfully"; }
                else { return result.ToString(); }
            }
            catch (Exception ex) { return ex.Message; }
            finally { conn.Close(); }

        }

        public string ValidateUser(string username, string password)
        {
            SqlConnection conn = new SqlConnection(config.GetValue<string>("ConnectionStrings:Cstr"));

            SqlCommand query = new SqlCommand();
            query.Connection = conn;
            query.CommandText = $"exec login {username}, {password}";

            conn.Open() ;
            object result = query.ExecuteScalar();
            if (result != null) role = result.ToString();
            conn.Close();
            return role;
        }

        public string GenerateToken(string role)
        {
            
        }

    }
}
