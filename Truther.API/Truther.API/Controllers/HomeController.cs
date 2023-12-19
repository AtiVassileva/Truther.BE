using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Truther.API.Implementation;
using Truther.API.Models;

namespace Truther.API.Controllers
{
    //This controllers is responsible for all operations regarding Truther post - for example: Create a post, Like a post, Comment on a post, Delete a post etc.
    public class HomeController : Controller
    {
        private readonly SqlHelper _sqlHelper;

        public HomeController(IConfiguration configuration)
        {
            _sqlHelper = new SqlHelper(configuration.GetConnectionString("DefaultConnection"));
        }

        // GET: HomeController
        public ActionResult Index()
        {
            return View();
        }

        // GET: HomeController/Details/5
        public ActionResult<Post[]> GetPosts()
        {
            return _sqlHelper.GetPosts().ToArray();
        }

        // GET: HomeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HomeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HomeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HomeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
