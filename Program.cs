
using insurance.Models;
using insurance.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace insurance
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // --- verification of token ---
            var audience = builder.Configuration.GetValue<string>("Audience");
            var Issuer = builder.Configuration.GetValue<string>("Issuer");
            var Secret = builder.Configuration.GetValue<string>("Secret");

            byte[] keyBytes = System.Text.Encoding.UTF8.GetBytes(Secret!);

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(c =>
            {
                c.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = Issuer,
                    ValidateAudience = true,
                    ValidAudience = audience,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(keyBytes)

                };
            });

                // --- ROLE BASED AUTHORIZATION  -----

            builder.Services.AddAuthorization(config =>
            {
                config.AddPolicy(SecurityPolicy.Admin, SecurityPolicy.AdminPolicy());
                config.AddPolicy(SecurityPolicy.User, SecurityPolicy.UserPolicy());
            });

            //-----------------------------------------

            builder.Services.AddTransient<ISecurity, Security>();
            builder.Services.AddTransient<IAdminService, AdminService>();
            builder.Services.AddTransient<ICustomerService, CustomerService>();


            //configuration to communicate with front end
            builder.Services.AddCors(conf =>
            {
                conf.AddPolicy("policy1", pol => {
                    pol.AllowAnyHeader();
                    pol.WithMethods("GET", "POST", "PUT", "DELETE");
                    pol.WithOrigins("http://localhost:4200");
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("policy1");
            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
