using Microsoft.AspNetCore.Mvc;
using Truther.API.Implementation;
using Truther.API.Models;

namespace Truther.API.Controllers
{
    //This controllers is responsible for all operations regarding Truther post - for example: Create a post, Like a post, Comment on a post, Delete a post etc.
    public class PostsController : Controller
    {
        private readonly SqlHelper _sqlHelper;

        public PostsController(IConfiguration configuration)
        {
            _sqlHelper = new SqlHelper(configuration.GetConnectionString("DefaultConnection"));
        }

        [HttpGet]
        public ActionResult<Post[]> GetPosts()
        {
            return _sqlHelper.GetPosts().ToArray();
        }

        [HttpGet("{postId:guid}")]
        public ActionResult<Post> GetPost(Guid postId)
        {
            return _sqlHelper.GetPost(postId);
        }

        [HttpPost]
        public Task Create([FromBody] Post post)
        {
            return _sqlHelper.CreatePost(post);
        }

        [HttpPatch("like/{postId:guid}")]
        [ValidateAntiForgeryToken]
        public Task LikePost([FromRoute] Guid postId, IFormCollection collection)
        {
           return _sqlHelper.LikePost(postId);
        }
    }
}
