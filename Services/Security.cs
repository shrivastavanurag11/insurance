using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using insurance.Models;
using Microsoft.IdentityModel.Tokens;

namespace insurance.Services
{
    public interface ISecurity
    {
        public string UserRegistration(string username, string password, string firstName, string lastName, string email, string contactNo, string address);
        public SecurityTokenModel ValidateUser(string username, string password);
    }
    public class Security : ISecurity
    {
        public readonly IConfiguration config;
        string? role;

        public Security(IConfiguration config)
        {
            this.config = config;
        }

        public string UserRegistration(string username, string password, string firstName,
        string lastName, string email, string contactNo, string address)
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

        public SecurityTokenModel ValidateUser(string username, string password)
        {
            SqlConnection conn = new SqlConnection(config.GetValue<string>("ConnectionStrings:Cstr"));

            SqlCommand query = new SqlCommand();
            query.Connection = conn;
            query.CommandText = $"exec login {username}, {password}";


            conn.Open();
            object result = query.ExecuteScalar();
            if (result != null)
            {
                SecurityTokenModel model;
                //generating jwt token if user is valid
                role = result.ToString();
                string? audience, issuer, secret;
                audience = config.GetValue<string>("Audience");
                issuer = config.GetValue<string>("issuer");
                secret = config.GetValue<string>("secret");

                List<Claim> claims = new List<Claim>();
                {
                    new Claim(JwtRegisteredClaimNames.Iss, issuer);
                    new Claim(JwtRegisteredClaimNames.Aud, audience);
                    new Claim(ClaimTypes.Role, role);
                }

                byte[] SecretTextBytes = System.Text.Encoding.UTF8.GetBytes(secret);
                var key = new SymmetricSecurityKey(SecretTextBytes);
                var SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.Sha256);

                var SecurityToken = new JwtSecurityToken(issuer, audience, claims, expires: DateTime.Now.AddMinutes(60), signingCredentials: SigningCredentials);
                var handler = new JwtSecurityTokenHandler();
                string token = handler.WriteToken(SecurityToken);

                model = new SecurityTokenModel();
                model.jwttoken = token;
                model.role = role;
                return model;
            }
            conn.Close();
            return null;

        }
    }
}
