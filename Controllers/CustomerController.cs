using insurance.Models;
using insurance.Models.Db;
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
            var username = User.Claims.Where(c => c.Type == "Username").FirstOrDefault()!.Value;
                //User.Claims.ToList().ElementAt(4).ToString();
            var response = service.buyPolicy(username , id);
            if (response == null) return Ok("Policy purchased successfully");
            return Ok(response);
        }





        [HttpGet]
        [Route("myPolicies")]
        public IActionResult myPolicies()
        {
            var username = User.Claims.Where(c => c.Type == "Username").FirstOrDefault()!.Value;
            var response = this.service.myPolicies(username);
            if (response == null) return BadRequest("You don't have any Active Policy");
            return Ok(response);
        }

        [HttpGet]
        [Route("myClaims")]

        public IActionResult MyClaims()
        {
            var username = User.Claims.Where(c => c.Type == "Username").FirstOrDefault()!.Value;
            var response = this.service.ClaimGroup(username);
            if (response == null) return BadRequest("You have not claimed anything!");
            return Ok(response);

        }

        [HttpGet]
        [Route("newClaim/{id}/{claimamount}/{remaining}")]
        public IActionResult newClaim(int id , int claimamount , int remaining)
        {
            var response = this.service.newClaim(id, claimamount , remaining);
            if (response == null) return Ok("Claim Unsuccessful!");
            return Ok("Claim Processed!");
        }

        [HttpGet]
        [Route("claimDetails/{id}")]
        public IActionResult claimDetails(int id)
        {
            var response = this.service.claimDetails(id);
            return Ok(response);
        }


        //////update user detials
        [HttpGet]
        [Route("useredetail")]
        public IActionResult UserDetails()
        {
            var result = this.service.UserDetail(User.Claims.Where(c => c.Type == "Username").FirstOrDefault()!.Value);
            if (result == null) return BadRequest("UserDetils Not Found!");
            else return Ok(result);
        }

        //update details
        //updateUser
        [HttpPost]
        [Route("updateuser")]
        public IActionResult UpdateProfile(User updated)
        {
            var res = this.service.UpdateUser(User.Claims.Where(c => c.Type == "Username").FirstOrDefault()!.Value, updated);
            return Ok(res);
        }

        //public IActionResult UpdateDetails()
        //{

        //}
    }
}

//search policy

//update customer details(phone , email , address)