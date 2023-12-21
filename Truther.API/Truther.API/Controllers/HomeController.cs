using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
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

        [HttpGet]
        public ActionResult<Post[]> GetPosts()
        {

            return _sqlHelper.GetPosts().ToArray();
        }

        [HttpPost]
        public ActionResult Create([FromBody] Post post)
        {
            return _sqlHelper.CreatePost(post);
        }

        [HttpPatch]
        [Route("{postId}")]
        [ValidateAntiForgeryToken]
        public ActionResult LikePost([FromRoute] Guid postId, IFormCollection collection)
        {
            var postId = Request.Body.

           return _sqlHelper.LikePost(postId);
        }
    }
}
