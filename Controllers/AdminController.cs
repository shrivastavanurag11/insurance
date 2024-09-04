using System.Diagnostics.Eventing.Reader;
using insurance.Models;
using insurance.Models.Db;
using insurance.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Identity.Client;

namespace insurance.Controllers
{
    //[Authorize(Policy = SecurityPolicy.Admin)]
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
        [HttpPost]
        [Route("User/{username}")]
        public IActionResult UserDetail(string username)
        {
            User? result = service.UserDetail(username);
            if (result == null) { return NotFound(); }
            else { return Ok(result); }
        }

        //deleteUser
        //UpdateUser

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
            return Ok(service.Analysis(0));
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

        //similarly create for data based on user id and date


        }
    }
