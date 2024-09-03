using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
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
        User? UserDetail(string username);
        List<Policy> PolicyList(int skip);
        Policy? PoliDetail(int id);
        List<claimrecord>? Claims(int skip);
        List<claimrecord>? Claims_User(string username);
        List<claimrecord>? Claims_Policy(int PolicyId);
        List<AnalysisData> Analysis(int skip);
        List<PolicyAnalysis>? PolicyAnalysis(int id);
        List<PolicyAnalysis>? policyAnalysis(string username);
        List<PolicyAnalysis>? policyAnalysis(DateTime? startdate, DateTime? enddate);
        List<PolicyAnalysis>? policyAnalysis(int start_amount, int end_amount);
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
        public User? UserDetail(string username)
        {
            User? users = database.Users.SingleOrDefault(c => c.UserName==username);
            return users;
        }
        //filter based on username
        //delete user, update user

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

        //delete policy , update policy

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

        //filter user 
        public List<claimrecord>? Claims_User(string username)
        {
            List<claimrecord>? res = (from a in database.Users
                                where a.UserName == username
                       join
                       b in database.PolicySolds on a.UserId equals b.UserId
                       join
                       c in database.Claims on b.PurchaseId equals c.PurchaseId
                       select new claimrecord()
                       {
                           UserName = a.UserName,
                           FirstName = a.FirstName,
                           PolicyId = b.PolicyId,
                           PurchasedON = b.SoldDate,
                           Amount = b.Amount,
                           ClaimedAmount = c.ClaimAmount,
                           RemainingAmount = c.RemainingAmount,
                           ClaimDate = c.ClaimDate
                       }).ToList();
            return res;
        }

        //filter policy 

        public List<claimrecord>? Claims_Policy(int PolicyId)
        {
            List<claimrecord>? res = (from a in database.Policies
                                where a.PolicyId == PolicyId join
                                b in database.PolicySolds on a.PolicyId equals b.PolicyId join
                                c in database.Claims on b.PurchaseId equals c.PurchaseId join
                                d in database.Users on b.UserId equals d.UserId
                                select new claimrecord()
                                {
                                    UserName = d.UserName,
                                    FirstName = d.FirstName,
                                    PolicyId = a.PolicyId,
                                    PurchasedON = b.SoldDate,
                                    Amount = b.Amount,
                                    ClaimedAmount = c.ClaimAmount,
                                    RemainingAmount = c.RemainingAmount,
                                    ClaimDate = c.ClaimDate
                                }).ToList();
            return res;
        }

        //filter date
        public List<claimrecord> Claim_date(DateTime? start_date , DateTime? end_date)
        {
            List<claimrecord>? res;
            if (end_date == null)
            {
                res = (from a in database.Claims where a.ClaimDate >= start_date join
                       b in database.PolicySolds on a.PurchaseId equals b.PurchaseId join
                       c in database.Users on b.UserId equals c.UserId
                       select new claimrecord()
                       {
                           UserName = c.UserName,
                           FirstName = c.FirstName,
                           PolicyId = b.PolicyId,
                           PurchasedON = b.SoldDate,
                           Amount = b.Amount,
                           ClaimedAmount = a.ClaimAmount,
                           RemainingAmount = a.RemainingAmount
                       }).ToList();
            }
            else
            {
                res = (from a in database.Claims
                       where a.ClaimDate >= start_date && a.ClaimDate <= end_date
                       join
                       b in database.PolicySolds on a.PurchaseId equals b.PurchaseId
                       join
                       c in database.Users on b.UserId equals c.UserId
                       select new claimrecord()
                       {
                           UserName = c.UserName,
                           FirstName = c.FirstName,
                           PolicyId = b.PolicyId,
                           PurchasedON = b.SoldDate,
                           Amount = b.Amount,
                           ClaimedAmount = a.ClaimAmount,
                           RemainingAmount = a.RemainingAmount
                       }).ToList();
            }
            return res;
            
        }



        //------ Sale Analysis ----
        public List<AnalysisData> Analysis(int skip)
        {
            List<AnalysisData> result = (from a in database.PolicySolds
                          join 
                          b in database.Policies on a.PolicyId equals b.PolicyId
                          join
                          c in database.Users on
                          a.UserId equals c.UserId
                          select new AnalysisData()
                          {
                              UserName = c.UserName, PolicyName=b.PolicyName, PurchasedOn=a.SoldDate, Amount = a.Amount
                          }).Skip(skip).Take(10).ToList();
            return result;
        }


        // filter based on policy id
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


        //filter based on user
        public List<PolicyAnalysis>? policyAnalysis(string username)
        {
            List<PolicyAnalysis>? result = (from a in database.Users where a.UserName == username join
                                            b in database.PolicySolds on a.UserId equals b.UserId join
                                            c in database.Policies on b.PolicyId equals c.PolicyId
                                            select new PolicyAnalysis()
                                            {
                                                PolicyID = b.PolicyId,
                                                PolicyName = c.PolicyName,
                                                UserName = a.UserName,
                                                Amount = b.Amount,
                                                PolicyType = c.PolicyType,
                                                SoldOn = b.SoldDate,
                                            }).ToList();
            return result;
        }
        //filter based on date
        public List<PolicyAnalysis>? policyAnalysis(DateTime? startdate , DateTime? enddate)
        {
            List<PolicyAnalysis>? res;
            if(enddate == null)
            {
                res = (from a in database.PolicySolds where a.SoldDate >= startdate join
                       b in database.Users on a.UserId equals b.UserId join 
                       c in database.Policies on a.PolicyId equals c.PolicyId
                       select new PolicyAnalysis()
                       {
                           PolicyID = c.PolicyId,
                           PolicyName = c.PolicyName,
                           UserName = b.UserName,
                           Amount = a.Amount,
                           PolicyType = c.PolicyType,
                           SoldOn = a.SoldDate
                       }).ToList();
            }
            else
            {
                res = (from a in database.PolicySolds
                       where a.SoldDate >= startdate && a.SoldDate <= enddate
                       join
                       b in database.Users on a.UserId equals b.UserId
                       join
                       c in database.Policies on a.PolicyId equals c.PolicyId
                       select new PolicyAnalysis()
                       {
                           PolicyID = c.PolicyId,
                           PolicyName = c.PolicyName,
                           UserName = b.UserName,
                           Amount = a.Amount,
                           PolicyType = c.PolicyType,
                           SoldOn = a.SoldDate
                       }).ToList();
            }
            return res;
        }

        //filter on amount
        public List<PolicyAnalysis>? policyAnalysis(int start_amount , int end_amount)
        {
            List<PolicyAnalysis> res = (from a in database.PolicySolds
                                              where a.Amount >= start_amount && a.Amount <= end_amount
                                              join
                                              b in database.Users on a.UserId equals b.UserId
                                              join
                                              c in database.Policies on a.PolicyId equals c.PolicyId
                                              select new PolicyAnalysis()
                                              {
                                                  PolicyID = c.PolicyId,
                                                  PolicyName = c.PolicyName,
                                                  UserName = b.UserName,
                                                  Amount = a.Amount,
                                                  PolicyType = c.PolicyType,
                                                  SoldOn = a.SoldDate
                                              }).ToList();
            return res;
        }
        //, poolicyid , date , amount
    }

}
