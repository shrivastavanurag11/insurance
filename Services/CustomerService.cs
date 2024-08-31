using Microsoft.Extensions.Configuration;
using System.Linq;

namespace insurance.Services
{
    public interface ICustomerService
    {
        Customer GetCustomer(int id);
        void AddCustomer(Customer customer);
        void UpdateCustomer(int id, Customer customer);
        // Add methods for displaying policy details and searching policy
    }

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
