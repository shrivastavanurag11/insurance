using insurance.Models.Dbase;
using insurance.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using insurance.Models.Dbase;
using insurance.Services;


namespace insurance.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _service;

        public CustomerController(ICustomerService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddCustomers([FromBody] Customer customer)
        {
            int result = await _service.AddCustomers(customer);
            return Ok(result);
        }

        [HttpGet]
        [Route("customer/{id}")]
        public IActionResult GetCustomers(int id)
        {
            var customer = _service.GetCustomers(id);
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateCustomers(int id, [FromBody] Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return BadRequest();
            }

            bool result = await _service.UpdateCustomers(customer);
            if (result)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }
    }
}















//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using PolicyManagementAPI.Models.Dbase;
//using PolicyManagementAPI.Services;

//namespace PolicyManagementAPI.Controllers
//{
//    //[Route("api/[controller]")]
//    [ApiController]
//    public class CustomerController : Controller
//    {
//        private readonly ICustomerService service;
//        public CustomerController(ICustomerService service)
//        {
//            this.service = service;
//        }

//        [HttpGet]
//        [Route("customer")]
//        public IActionResult AllCustomers()  //action method
//        {
//            List<Customer> customers = service.GetCustomer();
//            return Ok(customers);
//        }

//        [HttpGet]
//        [Route("customer/{CustomerId}")]
//        public IActionResult GetCustomers(int CustomerId)
//        {
//            Customer? c = service.GetCustomers(CustomerId);
//            if (c == null)
//                return NotFound();
//            else
//                return Ok(c);
//        }

//        [HttpPost]
//        [Route("add")]
//        public async Task<IActionResult> AddCustomers(Customer c)
//        {
//            int result = await service.AddCustomers(c);
//            return Ok(result);
//        }
//        //[HttpPut]
//        //[Route("update")]
//        //public async Task<IActionResult> UpdateCustomers(Customer c)
//        //{
//        //    bool result = await service.UpdateCustomers(c);
//        //    if (result)
//        //        return Ok();
//        //    else
//        //        return NotFound();
//        //}

//        [HttpPut]
//        [Route("update/{id}")]
//        public IActionResult UpdateCustomers(int id, [FromBody] Customer customer)
//        {
//            if (id != customer.CustomerId)
//            {
//                return BadRequest();
//            }

//            var existingCustomer = service.Customers.Find(id);
//            if (existingCustomer == null)
//            {
//                return NotFound();
//            }

//            existingCustomer.Name = customer.Name;
//            existingCustomer.Phone = customer.Phone;
//            existingCustomer.Email = customer.Email;
//            existingCustomer.Address = customer.Address;

//            _context.SaveChanges();

//            return NoContent();
//        }
//        [HttpDelete]
//        [Route("delete/{EmployeeId}")]
//        public IActionResult DeleteCustomers(int EmployeeId)
//        {
//            bool result = service.DeleteCustomers(EmployeeId);
//            if (result)
//            {
//                return Content("Record Deleted..");
//            }
//            else
//                return NotFound();

//        }
//    }
//}










////using Microsoft.AspNetCore.Http;
////using Microsoft.AspNetCore.Mvc;
////using PolicyManagementAPI.Models.Dbase;
////using PolicyManagementAPI.Services;

////namespace insurance.Controllers
////{
////    //[Route("api/[controller]")]
////    [ApiController]

////    public class CustomerController : ControllerBase
////    {
////        private readonly ApplicationDbContext _context;

////        public CustomerController(ApplicationDbContext context)
////        {
////            _context = context;
////        }

////        [HttpPost]
////        public IActionResult AddCustomers([FromBody] Customer customer)
////        {
////            _context.Customers.Add(customer);
////            _context.SaveChanges();

////            return CreatedAtAction(nameof(GetCustomers), new { id = customer.CustomerId }, customer);
////        }

////        [HttpGet("{id}")]
////        public IActionResult GetCustomers(int id)
////        {
////            var customer = _context.Customers.Find(id);
////            if (customer == null)
////            {
////                return NotFound();
////            }

////            return Ok(customer);
////        }

////        [HttpPut("{id}")]
////        public IActionResult UpdateCustomers(int id, [FromBody] Customer customer)
////        {
////            if (id != customer.CustomerId)
////            {
////                return BadRequest();
////            }

////            var existingCustomer = _context.Customers.Find(id);
////            if (existingCustomer == null)
////            {
////                return NotFound();
////            }

////            existingCustomer.Name = customer.Name;
////            existingCustomer.Phone = customer.Phone;
////            existingCustomer.Email = customer.Email;
////            existingCustomer.Address = customer.Address;

////            _context.SaveChanges();

////            return NoContent();
////        }
////    }

////}


////using Microsoft.AspNetCore.Http;
////using Microsoft.AspNetCore.Mvc;
////using PolicyManagementAPI.Models.Dbase;
////using PolicyManagementAPI.Services;

////namespace insurance.Controllers
////{
////    //[Route("api/[controller]")]
////    [ApiController]
////    public class CustomerController : ControllerBase
////    {
////        private readonly ApplicationDbContext _context;

////        public CustomerController(ApplicationDbContext context)
////        {
////            _context = context;
////        }

////        [HttpPost]
////        [Route("add")]
////        public IActionResult AddCustomers([FromBody] Customer customer)
////        {
////            _context.Customers.Add(customer);
////            _context.SaveChanges();

////            return CreatedAtAction(nameof(GetCustomers), new { id = customer.CustomerId }, customer);
////        }

////        [HttpGet]
////        [Route("customer/{id}")]
////        public IActionResult GetCustomers(int id)
////        {
////            var customer = _context.Customers.Find(id);
////            if (customer == null)
////            {
////                return NotFound();
////            }

////            return Ok(customer);
////        }

////        [HttpPut]
////        [Route("update/{id}")]
////        public IActionResult UpdateCustomers(int id, [FromBody] Customer customer)
////        {
////            if (id != customer.CustomerId)
////            {
////                return BadRequest();
////            }

////            var existingCustomer = _context.Customers.Find(id);
////            if (existingCustomer == null)
////            {
////                return NotFound();
////            }

////            existingCustomer.Name = customer.Name;
////            existingCustomer.Phone = customer.Phone;
////            existingCustomer.Email = customer.Email;
////            existingCustomer.Address = customer.Address;

////            _context.SaveChanges();

////            return NoContent();
////        }
////    }
////}

