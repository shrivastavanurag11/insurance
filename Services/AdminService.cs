using System.Reflection.Metadata.Ecma335;
using Azure.Identity;
using insurance.Models;
using insurance.Models.Db;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
        List<AnalysisData> Analysis(int skip);
        List<PolicyAnalysis>? PolicyAnalysis(int id);
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
            List<User> users = (from User in database.Users where User.UserId>skip select User).Take(10).ToList();
            return users;
        }
        public User? UserDetail(int id)
        {
            User? users = database.Users.SingleOrDefault(c => c.UserId==id);
            return users;
        }

        //filter based on username

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


        // filter based on policy id

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

        //filter user , policy , date

        //------ Sale Analysis ----

        public List<AnalysisData> Analysis(int skip)
        {
            List<AnalysisData> result = (from a in database.PolicySolds
                          join b in database.Policies on
                          a.PolicyId equals b.PolicyId
                          join
                          c in database.Users on
                          a.UserId equals c.UserId
                          select new AnalysisData()
                          {
                              UserName = c.UserName, PolicyName=b.PolicyName, PurchasedOn=a.SoldDate, Amount = a.Amount
                          }).Skip(skip).Take(10).ToList();
            return result;
        }


        public List<PolicyAnalysis>? PolicyAnalysis(int id)
        {
            var res = (from a in database.PolicySolds
                       where a.PolicyId == id
                       join b in database.Policies on
                       a.PolicyId equals b.PolicyId
                       join c in database.Users on
            a.UserId equals c.UserId
                       select new PolicyAnalysis()
                       {
                           PolicyID = a.PolicyId,
                           PolicyName = b.PolicyName,
                           UserName = c.UserName,
                           Amount = a.Amount,
                           PolicyType = b.PolicyType,
                           SoldOn = a.SoldDate,
                       }).ToList();
            return res;
        }
    }

}
