using System.Data.SqlClient;
using insurance.Models;
using insurance.Models.Db;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Hosting;

namespace insurance.Services
{
    public interface ICustomerService
    {
        List<Policy>? HomePage(int skip);
        string? buyPolicy(string username , int id);
        Policy? GetPolicy(int id);
        List<Policy>? GetPolicy(string type);
        List<PolicySold>? myPolicies(string username);
        List<claimrecord>? Claims(string username);
        List<groupPolicyDetail>? ClaimGroup(string username);
        string? newClaim(int purchaseid, int amount, int remainingamount);
        List<claimdetails>? claimDetails(int id);
    }

    public class CustomerService:ICustomerService
    {
        SqlConnection conn = new SqlConnection();
        public readonly InsuranceContext database;
        public readonly IConfiguration config;

        public CustomerService(IConfiguration config)
        {
            this.database = new InsuranceContext();
            this.config = config;
        }

        //display policy details on home page
        public List<Policy>? HomePage(int skip)
        {

            List<Policy> res = (from a in database.Policies where a.PolicyId > skip select a).Take(10).ToList();
            return res;
            
        }


        //search policy
        public Policy? GetPolicy(int id) 
        {
            return database.Policies.Where(a => a.PolicyId == id).FirstOrDefault();
        }

        public List<Policy>? GetPolicy(string type)
        {
            return database.Policies.Where(a => a.PolicyType == type).ToList();
        }


        //buy policy
        public string? buyPolicy(string username , int id)
        {
            var i = (from a in database.Policies where a.PolicyId == id select a).SingleOrDefault();
            var userid = (from a in database.Users where a.UserName == username select a.UserId).SingleOrDefault();
            int? exist = (from a in database.PolicySolds where a.PolicyId == id && a.UserId == userid select a.PurchaseId).SingleOrDefault();
            if (exist != 0) return "Policy already Purchased.";
            else
            {  //Invalid column name 
                try
                {


                    PolicySold p = new PolicySold
                    {
                        UserId = userid,
                        PolicyId = id,
                        SoldDate = DateTime.Now,
                        Amount = i.InsuranceAmount,
                        Duration = i.PolicyValidity

                    };
                    var result = database.PolicySolds.Add(p);
                    database.SaveChanges();
                    return null;
                }
                catch (Exception ex) { return ex.Message; }
            }
        }

        //my policies

        public List<PolicySold>? myPolicies(string username)
        {
            var userid = (from a in database.Users where a.UserName == username select a.UserId).SingleOrDefault();
            var result = (from a in database.PolicySolds where a.UserId == userid select a).ToList();
            return result;
        }

        //myclaims



        public List<claimrecord>? Claims(string username)
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



        public List<groupPolicyDetail>? ClaimGroup(string username)
        {

            var resp = database.Claims.GroupBy(x => x.PurchaseId).Select(x => new
            {
                purchaseId = x.Key,
                numberofcaims = x.Count(),
                totalclaimedamount = x.Sum(y => y.ClaimAmount),
                lastclaimdate = x.Max(y => y.ClaimDate)

            });

           List<groupPolicyDetail> result = (from a in database.Users
                                   where a.UserName == username
                                   join
                                   b in database.PolicySolds on a.UserId equals b.UserId
                                   join
                                   c in database.Policies on b.PolicyId equals c.PolicyId
                                   join d in resp on b.PurchaseId equals d.purchaseId 
                         select new groupPolicyDetail 
                         {
                            PurchaseId = b.PurchaseId,
                            PolicyId = c.PolicyId,
                            PolicyName = c.PolicyName,
                            PolicyType = c.PolicyType,
                            InsuredAmount = b.Amount,
                            numberOFClaims = d.numberofcaims,
                            totalClaimedAmount = d.totalclaimedamount,
                            lastClaimDate = d.lastclaimdate,
                            RemainingAmount = b.Amount - d.totalclaimedamount

                         }

                                   ).ToList();





            return result;
        }


        public string? newClaim(int purchaseid , int amount , int remainingamount)
        {
            try
            {
                conn.ConnectionString = config.GetValue<string>("ConnectionStrings:Cstr");

                SqlCommand query = new SqlCommand();
                query.Connection = conn;
                query.CommandText = $"insert into claims values('{purchaseid}','{amount}', '{remainingamount}', '{DateTime.Now}')";
                conn.Open();

                object result = query.ExecuteScalar();
                if (result == null) { return "Claim is Successful!!!"; }
                else { return null; }

                
            }
            catch(Exception e)
            {
                return e.Message;
            }





        }

        public List<claimdetails>? claimDetails(int id)
        {
            List<claimdetails>? res = (from a in database.Claims
                       where a.PurchaseId == id
                       select new claimdetails()
                      {
                          date = a.ClaimDate,
                          amount =  a.ClaimAmount,
                          remainingamount =  a.RemainingAmount,
                      }).ToList();

            return res;
        }
        //cart list

        //purchase policy
        //update customer details(phone , email , address)

    }
}
