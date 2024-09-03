using System.ComponentModel;
using insurance.Models.Dbase;
using insurance.Services;

namespace insurance.Services
{
    public interface ICustomerService
    {
        List<Customer> GetAllCustomers();
        Customer? GetCustomers(int CustomerId);
        bool DeleteCustomers(int CustomerId);
        Task<int> AddCustomers(Customer c);
        Task<bool> UpdateCustomers(Customer c);

    }
    public class CustomerService : ICustomerService
    {

        private readonly ApplicationDbContext context;
        public CustomerService(ApplicationDbContext context)
        {
            this.context = context;
        }


        public List<Customer> GetAllCustomers()
        {
            var list = context.Policy.ToList();
            return list;
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

        public Customer? GetCustomer(int CustomerId)
        {
            throw new NotImplementedException();
        }



        public async Task<bool> UpdateCustomers(Customer c)
        {
            Customer? customer = context.Policy.SingleOrDefault(a => a.CustomerId == c.CustomerId);
            if (customer == null) return false;
            else
            {
                customer.Name = customer.Name;
                customer.Phone = customer.Phone;
                customer.Email = customer.Email;
                customer.Address = customer.Address;

                await context.SaveChangesAsync();
                return true;
            }
        }

        public bool DeleteCustomers(int CustomerId)
        {
            var c = (from a in context.Policy where a.CustomerId == CustomerId select a).FirstOrDefault();
            if (c == null)
                return false;
            else
            {
                context.Policy.Remove(c);
                context.SaveChanges();
                return true;
            }
        }

        //public async Task<bool> UpdateCustomers(Customer c)
        //{
        //    Customer? customers = context.Customers.SingleOrDefault(c => c.Code == em.Code);
        //    if (emp == null) return false;
        //    else
        //    {
        //        emp.Name = em.Name;
        //        emp.Salary = em.Salary;

        //        await context.SaveChangesAsync();
        //        return true;
        //    }
        //}
        /*  public bool UpdateEmp2(Emp em)
            {
                Emp? emp = context.Emps.SingleOrDefault(c => c.Code == em.Code);
                if (emp == null) return false;
                else
                {
                    emp.Name = em.Name;
                    emp.Salary = em.Salary;

                    context.SaveChanges();
                    return true;
                }
            }
        */
    }
}

