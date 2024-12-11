using insurance.Models.Dbase;
using Microsoft.EntityFrameworkCore;
using insurance.Models.Dbase;
using System.Collections.Generic;

namespace insurance.Services
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Policy { get; set; }
        public DbSet<Policy> Policies { get; set; }
        public DbSet<User> Users { get; set; }
    }







}