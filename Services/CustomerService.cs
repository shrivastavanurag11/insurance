using Microsoft.Extensions.Configuration;
using System.Linq;

namespace insurance.Services
{
    public interface ICustomerService
    {
        List<Customer> GetAllCustomers();
        Customer? GetCustomers(int CustomerId);
        bool DeleteCustomers(int CustomerId);
        Task<int> AddCustomers(Customer c);
        Task<bool> UpdateCustomers(Customer c);

    public class CustomerService : ICustomerService
    {
        private readonly ApplicationDbContext _context;
        public readonly IConfiguration config;

        public CustomerService(ApplicationDbContext context, IConfiguration config)
        {
            _context = context;
            this.config = config;
        }

        public Customer GetCustomer(int id)
        {
            return _context.Customers.Find(id);
        }
        public Customer? GetCustomers(int CustomerId)
        {
            var customer = context.Policy.SingleOrDefault(c => c.CustomerId == CustomerId);
            return customer;
        }
        //public bool DeleteCustomer(int CustomerId)
        //{
        //    var c = (from a in context.Customers where a.CustomerId == CustomerId select a).FirstOrDefault();
        //    if (c == null)
        //        return false;
        //    else
        //    {
        //        context.Customers.Remove(c);
        //        context.SaveChanges();
        //        return true;
        //    }
        //    //throw new NotImplementedException();
        //}
        public async Task<int> AddCustomers(Customer c)
        {
            context.Policy.Add(c);
            var i = await context.SaveChangesAsync();
            return i;
        }

        public void AddCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
        }

        public void UpdateCustomer(int id, Customer customer)
        {
            var existingCustomer = _context.Customers.Find(id);
            if (existingCustomer != null)
            {
                existingCustomer.Name = customer.Name;
                existingCustomer.Phone = customer.Phone;
                existingCustomer.Email = customer.Email;
                existingCustomer.Address = customer.Address;
                _context.SaveChanges();
            }
        }

        // Method to display policy details
        public Policy GetPolicyDetails(int policyId)
        {
            return _context.Policies.Find(policyId);
        }

        // Method to search policy
        public IQueryable<Policy> SearchPolicy(string policyName)
        {
            return _context.Policies.Where(p => p.Name.Contains(policyName));
        }
    }
}

