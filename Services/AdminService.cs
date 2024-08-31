using insurance.Models.Db;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;

namespace insurance.Services
{
    public interface IAdminService
    {
        List<User> UserList(int skip);
        User? UserDetail(int id);
        List<Policy> PolicyList(int skip);
        Policy? PoliDetail(int id);
        List<claimrecord>? Claims(int skip);
    }
    public class AdminService : IAdminService
    {
        public readonly InsuranceContext database;

        public AdminService() 
        {
            this.database = new InsuranceContext(); 
        }


     //-----user management
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


     // -- policy management --
        public List<Policy> PolicyList(int skip)
        {
            List<Policy> res = (from a in database.Policies select a).Skip(skip).Take(10).ToList();
            return res;
        }
        public Policy? PoliDetail(int id)
        {
            Policy? p = database.Policies.SingleOrDefault(c => c.PolicyId == id);
            return p;
        }


        //-- claims data --
        public List<claimrecord>? Claims(int skip)
        {
            List<claimrecord>? result = (from a in database.Claims join
                         b in database.PolicySolds on
                         a.PurchaseId equals b.PurchaseId join
                         c in database.Users on
                         b.UserId equals c.UserId
                         select new claimrecord() { UserName = c.UserName, FirstName = c.FirstName,
                             PolicyId = b.PolicyId, PurchasedON = b.SoldDate, Amount = b.Amount,
                             ClaimedAmount = a.ClaimAmount, RemainingAmount = a.RemainingAmount }).Skip(skip).Take(10).ToList();
            return result;
        }


        //------ Analysis  ----

        public object analysis(int skip)
        {
            List<User> users = (from User in database.Users select User).Skip(skip).Take(10).ToList();
            return users;
        }
    }

}
