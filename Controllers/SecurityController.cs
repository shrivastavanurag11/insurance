using insurance.Models;
using insurance.Models.Db;
using insurance.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace insurance.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        public readonly ISecurity service;
        public SecurityController(ISecurity service)
        {
            this.service = service;
        }

        [HttpPost]
        [Route("registration")]
        public IActionResult Registration(User u)
        {
            var result = service.UserRegistration(u.UserName, u.Password, u.FirstName, u.LastName, u.Email, u.ContactNo, u.Address);
            return Ok(result);
        }

        [HttpPost]
        [Route("login")]
        public IActionResult login(UserCredentials cred)
        {
            DataTransferModel? transferobj = new DataTransferModel();
            object? model = service.ValidateUser(cred.UserName!, cred.Password!);
            if (model == null)
            {
                transferobj.Success = false;
                transferobj.Message = "Invalid UserName or Password";
            }
            else
            {
                transferobj.Data = model;
                transferobj.Success = true;                
            }
            return Ok(transferobj);
        }

        [HttpPost]
        [Route("checkExistingUser/{userName}")]
        public IActionResult CheckExistingUser(string userName)
        {
            var res = service.CheckUsers(userName);
            if (res) return Ok(true);
            else return Ok(false);
        }
    }
}
