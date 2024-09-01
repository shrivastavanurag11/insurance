using insurance.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace insurance.Controllers
{
    [Authorize(Policy = SecurityPolicy.User)]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

    }
}
