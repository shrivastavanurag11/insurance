using insurance.Models;
using insurance.Models.Db;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;

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
        User? UserDetail(string username);
        string? UpdateUser( string username, User updated);
    }

    public class CustomerService:ICustomerService
    {
        public readonly InsuranceContext database;
        
        public CustomerService(IConfiguration config)
        {
            this.database = new InsuranceContext();
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


        //userdetails
        public User? UserDetail(string username)
        {
            User? users = database.Users.SingleOrDefault(c => c.UserName == username);
            return users;
        }

       
        public string? UpdateUser(string username,User updated)
        {
            var user = (from a in database.Users where a.UserName == username select a).SingleOrDefault();
            if (user != null)
            {
                //// Modify the entity
                user.Email = updated.Email;
                user.ContactNo = updated.ContactNo;
                user.CustomerImage = updated.CustomerImage; // Assuming you have the image as a byte array

                // Save changes
                database.SaveChanges();
                return "Profile updated successfully!";
            }
            else {
                return "User Details Not Found!!!";
            }

            
        }
          
        //purchase policy
        //update customer details(phone , email , address)

    }
}
