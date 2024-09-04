using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using insurance.Models;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.IdentityModel.Tokens;

namespace insurance.Services
{
    public interface ISecurity
    {
        public string? UserRegistration(string username, string password, string firstName, string lastName, string email, string contactNo, string address);
        public SecurityTokenModel? ValidateUser(string username, string password);
        bool CheckUsers(string username);
    }
    public class Security : ISecurity
    {
        public readonly IConfiguration config;
        string? role;

        public Security(IConfiguration config)
        {
            this.config = config;
        }

        public string? UserRegistration(string username, string password, string firstName,
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
                if (result == null) { return "Account Created Successfully!!!"; }
                else { return null; }
            }
            catch (Exception ex) { return ex.Message; }
            finally { conn.Close(); } 

        }

        public SecurityTokenModel? ValidateUser(string username, string password)
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

                List<Claim>? claims = new List<Claim>();
                {
                    new Claim(JwtRegisteredClaimNames.Iss, issuer!);
                    new Claim(JwtRegisteredClaimNames.Aud, audience!);
                    new Claim(ClaimTypes.Role, role!);
                    new Claim("Username", username);
                }

                byte[] SecretTextBytes = System.Text.Encoding.UTF8.GetBytes(secret!);
                var key = new SymmetricSecurityKey(SecretTextBytes);
                var SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var SecurityToken = new JwtSecurityToken(issuer, audience, claims, expires: DateTime.Now.AddMinutes(60), signingCredentials: SigningCredentials);
                var handler = new JwtSecurityTokenHandler();
                string token = handler.WriteToken(SecurityToken);

                model = new SecurityTokenModel();
                model.jwttoken = token;
                model.role = role!;
                return model;
            }
            conn.Close();
            return null;

        }

        public bool CheckUsers(string username)
        {
            SqlConnection conn = new SqlConnection(config.GetValue<string>("ConnectionStrings:Cstr"));

            SqlCommand query = new SqlCommand();
            query.Connection = conn;
            query.CommandText = $"select UserName from Users where UserName = '{username}'";
            conn.Open();
            object? result = query.ExecuteScalar();
            if (result == null) return false;
            else return true;
        }
    }
}
