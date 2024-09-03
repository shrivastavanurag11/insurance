using insurance.Models.Db;

namespace insurance.Services
{
    public interface ICustomerService
    { 

        
    }

    public class CustomerService:ICustomerService
    {
        public readonly InsuranceContext database;
        

        public CustomerService(IConfiguration config)
        {
            this.database = new InsuranceContext();
        }


        //display policy details on home page
        public List<Policy> HomePage(int skip)
        {

            List<Policy> res = (from a in database.Policies where a.PolicyId > skip select a).Take(10).ToList();
            return res;
            
        }


        //search policy
        public Policy? GetPolicy(int id) 
        {
            return database.Policies.Where(a => a.PolicyId == id).FirstOrDefault();
        }


        //add policy to cart
        //public AddToCart(string username , int PolicyId)
        //{
        //    database.cart
        //}
        //purchase policy
        //update customer details(phone , email , address)

    }
}
