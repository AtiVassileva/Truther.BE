using Microsoft.AspNetCore.Mvc;

namespace Truther.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // This controller is responsible for registrations and login operations.
    public class AccountsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            return BadRequest("Not implemented yet.");
        }
    }
}
