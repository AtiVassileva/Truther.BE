using Microsoft.AspNetCore.Mvc;

namespace Truther.API.Controllers
{
    // This controller is responsible for registrations and login operations.
    public class AccountsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
