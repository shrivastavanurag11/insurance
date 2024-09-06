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


        //cart list

        //purchase policy
        //update customer details(phone , email , address)

    }
}
