using insurance.Models;
using insurance.Services;
using Microsoft.AspNetCore.Authorization;
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


        [HttpGet]
        [Route("customerHome")]
        public IActionResult HomePage()
        {

            c_offset = 0;
            var result = service.HomePage(c_offset);
            if (result == null) return NotFound();
            return Ok(result);
        }
        [HttpGet]
        [Route("nextCustomerHome")]
        public IActionResult NextPage()
        {
            c_offset += 10;
            var result = service.HomePage(c_offset);
            return Ok(result);
        }

        //public IActionResult GetPolicy()
        //{

      [HttpPut("{id}")]
      public IActionResult UpdateCustomer(int id, [FromBody] Customer customer)
      {
          if (id != customer.CustomerId)
          {
              return BadRequest();
          }

        //buy policy
        [HttpGet]
        [Route("buyPolicy/{id}")]
        public IActionResult buyPolicy(int id)
        {
            var username = User.Claims.Where(c => c.Type == "Username").FirstOrDefault().Value;
                //User.Claims.ToList().ElementAt(4).ToString();
            var response = service.buyPolicy(username , id);
            if (response == null) return Ok("Policy purchased successfully");
            return Ok(response);
        }

        //public IActionResult UpdateDetails()
        //{

        //}
    }
}
//display policy details on home page
//search policy
//add policy to cart
//purchase policy
//update customer details(phone , email , address)