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
        public IActionResult login(string username , string password)
        {
            string? role = service.ValidateUser(username, password);
            if (role == null)
            {
                return BadRequest();
            }
            else
            {
               return Ok(role);
            }
        }
    }
}
