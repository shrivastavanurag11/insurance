using System.Diagnostics.Eventing.Reader;
using System.Reflection;
using insurance.Models;
using insurance.Models.Db;
using insurance.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Identity.Client;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace insurance.Controllers
{

    [Authorize(Policy = SecurityPolicy.Admin)]
    //[Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        public readonly IAdminService service;
        int u_offset;
        int p_offset;
        int c_offset;
        int a_offset;
        public AdminController(IAdminService service)
        {
            this.service = service;
        }


        //------------------------ user management----------------------
        //preview on first page
        [HttpGet]
        [Route("UserList")]
        public IActionResult RegisteredUsers()
        {
            u_offset = 0;
            return Ok(service.UserList(0));
        }

        //next button 
        [HttpGet]
        [Route("NextUserList")]
        public IActionResult NextRegisteredUsers()
        {
            u_offset +=  10;
            return Ok(service.UserList(u_offset));
        }

        //searchuser
        [HttpGet]
        [Route("User/{username}")]
        public IActionResult UserDetail(string username)
        {
            User? result = service.UserDetail(username);
            if (result == null) { return NotFound(); }
            else { return Ok(result); }
        }

        //deleteUser

        // Controller
        [HttpDelete]
        [Route("Userdelete/{username}")]
        public IActionResult DeleteUser(string username)
        {
            bool isDeleted = service.DeleteUser(username);
            if (!isDeleted)
            {
                return NotFound(); // Return 404 if the user was not found
            }
            return NoContent(); // Return 204 No Content on successful deletion
        }

        //UpdateUser

        // Controller
        [HttpGet]
        [Route("Userupdate/{username}")]
        public IActionResult UpdateUser(string username, [FromBody] User updatedUser)
        {
            bool isUpdated = service.UpdateUser(username, updatedUser);
            if (!isUpdated)
            {
                return NotFound(); // Return 404 if the user was not found
            }
            return NoContent(); // Return 204 No Content on successful update
        }


        //------------------------------------ Policy Management -----------------

        [HttpGet]
        [Route("PolicyList")]
        public IActionResult RegisteredPolicy()
        {
            p_offset = 0;
            return Ok(service.PolicyList(0));
        }


        [HttpPost]
        [Route("Policy/{id}")]
        public IActionResult PolicyDetail(int id)
        {
            Policy? result = service.PoliDetail(id);
            if (result == null) { return NotFound(); }
            else { return Ok(result); }
        }

        //next button 
        [HttpGet]
        [Route("NextPolicyList")]
        public IActionResult NextRegisteredPolicy()
        {
            p_offset = p_offset + 10;
            return Ok(service.UserList(p_offset));
        }

        [HttpPost]
        [Route("addNewPolicy")]
        public IActionResult AddNewPolicy(Policy p)
        {
            var res = this.service.AddNewPolicy(p);
            return Ok(res);
            //if (res == null) { return Ok("Added Successfully!"); }
            //else { return BadRequest("Operation Unsuccessful!"); }
        }
        //---- policy claim data management ----

        [HttpGet]
        [Route("claims")]
        public IActionResult ClaimData()  // to show the claims made by users
        {
            c_offset = 0;
            List<claimrecord>? claimrecords = service.Claims(c_offset);
            if (claimrecords == null) { return NotFound(); }
            else { return Ok(claimrecords); }
        }

        //show claims based on user id
        //show claims based on policy id
        //show claims based on date
        //show calims based on range of amount



        // -- Analysis on policy sales data by admin----
        [HttpGet]
        [Route("analysis")]
        public IActionResult Analysis()
        {
            a_offset = 0;
            return Ok(service.Analysis(a_offset));
        }


        [HttpGet]
        [Route("nextanalysis")]
        public IActionResult NextAnalysis()
        {
            a_offset = a_offset + 10;
            return Ok(service.Analysis(a_offset));
        }

        [HttpPost]
        [Route("analysis/policy/{id}")]
        public IActionResult PolicyAnalysis(int id)
        {
            List<PolicyAnalysis>? result = service.PolicyAnalysis(id);
            if (result == null) { return NotFound(); }
            else return Ok(result);
        }

        [HttpPost]
        [Route("filter")]
        public IActionResult FilterData(filterOption criteria)
        {
            List<PolicyAnalysis>? res = this.service.FilteredData(criteria);
            if (res == null) { return NotFound(); }
            else return Ok(res);
        }


        [HttpGet]
        [Route("bar/{year}")]
        public IActionResult GetPoliciesSoldByMonth(string year)
        {
            var result = this.service.monthlyAnalysis(year);

            return Ok(result);
        }

        //similarly create for data based on user id and date


    }
    }
