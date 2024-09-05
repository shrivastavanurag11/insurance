using insurance.Models.Db;
using Microsoft.AspNetCore.Mvc;

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
            //var result = from a in database
            return null;
        }


        //cart list

        //purchase policy
        //update customer details(phone , email , address)

    }
}
