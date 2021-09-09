using KIS.Managers;
using KIS.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIS.Controllers
{
    public class PostController : Controller
    {
        private UnitOfWork _unitOfWork;
        public PostController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("Posts")]
        public IEnumerable<Post> GetPosts()
        {
            return _unitOfWork.postManager.GetPosts();
        }

        [HttpPost]
        [Route("Posts/Add")]
        public async Task<ActionResult> AddPost()
        {
            var reader = new StreamReader(Request.Body, Encoding.UTF8);
            var body = await reader.ReadToEndAsync();
            var post = JsonConvert.DeserializeObject<Post>(body);
            post.Id = Guid.NewGuid();

            _unitOfWork.postManager.AddPost(post);
            return Ok(post);

        }

        [HttpDelete]
        [Route("Post/Delete/{id}")]
        public async Task<ActionResult> DeletePost(Guid id)
        {
            if (!_unitOfWork.postManager.DeletePost(id))
                return Content("Post does not exist!");
            return Ok();
        }
    }
}
