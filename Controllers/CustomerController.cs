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
        public CustomerController(ICustomerService service)
        {
            this.service = service;
        }


       // [HttpGet]
        //public IActionResult HomePage()
        //{
            
        //}

        //public IActionResult GetPolicy()
        //{

        //}


        //public IActionResult AddToCart()
        //{

        //}

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