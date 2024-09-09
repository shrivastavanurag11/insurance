using System.Data.SqlClient;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using Azure.Identity;
using insurance.Models;
using insurance.Models.Db;
using Microsoft.EntityFrameworkCore;
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
        List<PolicyAnalysis> Analysis(int skip);
        List<PolicyAnalysis>? PolicyAnalysis(int id);
        List<PolicyAnalysis>? policyAnalysis(string username);
        List<PolicyAnalysis>? policyAnalysis(DateTime? startdate, DateTime? enddate);
        List<PolicyAnalysis>? policyAnalysis(int start_amount, int end_amount);
        bool DeleteUser(string username);
        bool UpdateUser(string username, User updatedUser);
        string? AddNewPolicy(Policy p);
        List<PolicyAnalysis>? FilteredData(filterOption criteria);
        object monthlyAnalysis(string year);
    }
    public class AdminService : IAdminService
    {
        public readonly InsuranceContext database;
        SqlConnection conn = new SqlConnection();
        public readonly IConfiguration config;

        public AdminService(IConfiguration config)
        {
            this.database = new InsuranceContext();
            this.config = config;
        }


        //-----user management
        public List<User> UserList(int skip)
        {
            List<User> users = (from User in database.Users select User).Skip(skip).Take(10).ToList();
            return users;
        }
        public User? UserDetail(string username)
        {
            User? users = database.Users.SingleOrDefault(c => c.UserName == username);
            return users;
        }
        //filter based on username
        //delete user, update user
        public bool DeleteUser(string username)
        {
            var user = database.Users.SingleOrDefault(u => u.UserName == username);
            if (user == null)
            {
                return false; // User not found
            }

            database.Users.Remove(user);
            database.SaveChanges();
            return true;
        }

        public bool UpdateUser(string username, User updatedUser)
        {
            var user = database.Users.SingleOrDefault(u => u.UserName == username);
            if (user == null)
            {
                return false; // User not found
            }

            user.FirstName = updatedUser.FirstName;
            user.LastName = updatedUser.LastName;
            user.Age = updatedUser.Age;
            user.Gender = updatedUser.Gender;
            user.Email = updatedUser.Email;
            user.ContactNo = updatedUser.ContactNo;
            user.Address = updatedUser.Address;
            // Update other fields as needed

            database.SaveChanges();
            return true;
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
        public List<claimrecord> Claim_date(DateTime? start_date, DateTime? end_date)
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
        public List<PolicyAnalysis> Analysis(int skip)
        {
            List<PolicyAnalysis> result = (from a in database.PolicySolds
                                           join
                                           b in database.Policies on a.PolicyId equals b.PolicyId
                                           join
                                           c in database.Users on
                                           a.UserId equals c.UserId
                                           select new PolicyAnalysis()
                                           {
                                               PolicyID = b.PolicyId,
                                               PolicyType = b.PolicyType,
                                               PolicyName = b.PolicyName,
                                               UserName = c.UserName,
                                               Amount = a.Amount,
                                               SoldOn = a.SoldDate
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
        public List<PolicyAnalysis>? policyAnalysis(DateTime? startdate, DateTime? enddate)
        {
            List<PolicyAnalysis>? res;
            if (enddate == null)
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
        public List<PolicyAnalysis>? policyAnalysis(int start_amount, int end_amount)
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

        public string? AddNewPolicy(Policy p)
        {

            try
            {
                conn.ConnectionString = config.GetValue<string>("ConnectionStrings:Cstr");

                SqlCommand query = new SqlCommand();
                query.Connection = conn;
                query.CommandText = $"insert into Policies values('{null}','{p.PolicyType}','{p.PolicyName}', '{p.InsuranceAmount}', '{p.PolicyValidity}', '{p.PolicyDescription}', '{'Y'}')";
                conn.Open();

                object result = query.ExecuteScalar();
                if (result == null) { return "Operation Successful!!!"; }
                else { return null; }


            }
            catch (Exception e)
            {
                return e.Message;
            }
            //try
            //{
            //    var res = database.Policies.Add(p);
            //    database.SaveChanges();
            //    return null;
            //}
            //catch (Exception ex)
            //{
            //   return ex.Message;
            //}
        }

        public List<PolicyAnalysis>? FilteredData(filterOption criteria)
        {
            var users = (from a in database.Users where
                       (criteria.UserName == null || a.UserName == criteria.UserName) &&
                       (criteria.CustomerName == null || a.FirstName == criteria.CustomerName) select a);

            var policies = (from b in database.Policies where
                       (criteria.PolicyId == null || b.PolicyId == criteria.PolicyId) &&
                       (criteria.PolicyType == null || b.PolicyType == criteria.PolicyType) select b);


            var sales = (from c in database.PolicySolds
                         where
                         (criteria.StartDate == null || criteria.StartDate >= c.SoldDate) &&
                         (criteria.EndDate == null || criteria.EndDate <= c.SoldDate)
                         select c);
            var res = (from a in users join
                      b in sales on a.UserId equals b.UserId join
                      c in policies on b.PolicyId equals c.PolicyId
                      select new PolicyAnalysis()
                      {
                          PolicyID = c.PolicyId,
                          PolicyName = c.PolicyName,
                          UserName = a.UserName,
                          Amount = b.Amount,
                          PolicyType = c.PolicyType,
                          SoldOn = b.SoldDate
                      }).ToList();
            return res;
        }


        public object monthlyAnalysis(string year)
        {
           var res =  database.PolicySolds
               .Where(p => $"{p.SoldDate.Year}" == year)
               .GroupBy(p => p.SoldDate.Month)
               .Select(g => new
               {
                   Month = g.Key,
                   PoliciesSold = g.Count()
               })
               .ToList();
            return res;
        }
    }

}
