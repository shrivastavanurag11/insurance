using insurance.Models;
using insurance.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;

namespace insurance.Controllers
{
    [Authorize(Policy = SecurityPolicy.User)]
    //[Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        public ICustomerService service;
        int c_offset;
        public CustomerController(ICustomerService service)
        {
            this.service = service;
        }


        [HttpGet]
        [Route("customerHome")]
        public IActionResult HomePage()
        {

            c_offset = 0;
            var result = service.HomePage(c_offset);
            if(result == null) return NotFound();
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

        //}

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