using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using insurance.Models.Dbase;
using insurance.Services;

namespace insurance.Controllers{
  //[Route("api/[controller]")]
  [ApiController]

  public class CustomersController : ControllerBase
  {
      private readonly ApplicationDbContext _context;
  
      public CustomersController(ApplicationDbContext context)
      {
          _context = context;
      }

      [HttpPost]
      public IActionResult AddCustomer([FromBody] Customer customer)
      {
          _context.Customers.Add(customer);
          _context.SaveChanges();
  
          return CreatedAtAction(nameof(GetCustomer), new { id = customer.CustomerId }, customer);
      }

      [HttpGet("{id}")]
      public IActionResult GetCustomer(int id)
      {
          var customer = _context.Customers.Find(id);
          if (customer == null)
          {
              return NotFound();
          }
  
          return Ok(customer);
      }

      [HttpPut("{id}")]
      public IActionResult UpdateCustomer(int id, [FromBody] Customer customer)
      {
          if (id != customer.CustomerId)
          {
              return BadRequest();
          }

          var existingCustomer = _context.Customers.Find(id);
          if (existingCustomer == null)
          {
              return NotFound();
          }

          existingCustomer.Name = customer.Name;
          existingCustomer.Phone = customer.Phone;
          existingCustomer.Email = customer.Email;
          existingCustomer.Address = customer.Address;
  
          _context.SaveChanges();
  
          return NoContent();
      }
  }

}


