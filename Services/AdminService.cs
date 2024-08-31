using insurance.Models.Db;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;

namespace insurance.Services
{
    public interface IAdminService
    {
        List<User> UserList(int skip);
        User? UserDetail(int id);
    }
    public class AdminService : IAdminService
    {
        public readonly InsuranceContext database;

        public AdminService() 
        {
            this.database = new InsuranceContext(); 
        }

        public List<User> UserList(int skip)
        {
            List<User> users = (from User in database.Users select User).Skip(skip).Take(10).ToList();
            return users;
        }



        public User? UserDetail(int id)
        {
            User? users = database.Users.SingleOrDefault(c => c.UserId==id);
            return users;
        }

    }

}
