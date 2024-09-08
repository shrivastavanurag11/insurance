using insurance.Models.Dbase;
using insurance.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using System.Linq;

namespace insurance.Services
{
    public interface IPolicyService
    {
        IQueryable<Policy> GetPolicies(string searchCriteria);
        Policy GetPolicy(int id);
        void AddPolicy(Policy policy);
        void UpdatePolicy(int id, Policy policy);
    }

    public class PolicyService : IPolicyService
    {
        private readonly ApplicationDbContext _context;
        public readonly IConfiguration config;

        public PolicyService(ApplicationDbContext context, IConfiguration config)
        {
            _context = context;
            this.config = config;
        }

        public IQueryable<Policy> GetPolicies(string searchCriteria)
        {
            var policies = _context.Policies.Include(p => p.Customer).AsQueryable();

            if (!string.IsNullOrEmpty(searchCriteria))
            {
                policies = policies.Where(p => p.PolicyNumber.Contains(searchCriteria) ||
                                               p.Customer.Name.Contains(searchCriteria));
            }

            return policies;
        }

        public Policy GetPolicy(int id)
        {
            return _context.Policies.Include(p => p.Customer).SingleOrDefault(p => p.PolicyId == id);
        }

        public void AddPolicy(Policy policy)
        {
            _context.Policies.Add(policy);
            _context.SaveChanges();
        }

        public void UpdatePolicy(int id, Policy policy)
        {
            var existingPolicy = _context.Policies.Find(id);
            if (existingPolicy != null)
            {
                existingPolicy.Phone = policy.Phone;
                existingPolicy.Email = policy.Email;
                existingPolicy.Address = policy.Address;
                _context.SaveChanges();
            }
        }
    }
}