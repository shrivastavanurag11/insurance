using insurance.Models.Db;
using insurance.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;

namespace insurance.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        public readonly IAdminService service;
        int offset;
        public AdminController(IAdminService service)
        {
            this.service = service;
        }


        //------------------------ user management----------------------
        //previe on first page
        [HttpGet]
        [Route("UserList")]
        public IActionResult RegisteredUsers()
        {
            offset = 0;
            return Ok(service.UserList(0));
        }

        //next button 
        [HttpGet]
        [Route("NextUserList")]
        public IActionResult NextRegisteredUsers()
        {
            offset = offset + 10;
            return Ok( service.UserList(offset));
        }

        //searchuser
        [HttpPost]
        [Route("User/{id}")]
        public IActionResult UserDetail(int id)
        {
            User? result = service.UserDetail(id);
            if(result == null) { return NotFound(); }
            else { return Ok( result ); }
        }

        //deleteUser
        //UpdateUser

        //------------------------------------ Policy Management -----------------


    }
}
