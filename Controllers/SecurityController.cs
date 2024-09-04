using insurance.Models;
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
        public IActionResult Registration(string username,
        string password,
        string firstName,
        string lastName,
        string email,
        string contactNo,
        string address)
        {
            var result = service.UserRegistration(username, password, firstName, lastName, email, contactNo, address);
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
    }
}
