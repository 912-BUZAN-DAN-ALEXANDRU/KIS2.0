using KIS.Managers;
using KIS.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
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
             var posts = _unitOfWork.postManager.GetPosts();
            return posts;
        }

        [HttpPost]
        [Route("Posts/Add")]
        public async Task<ActionResult> AddPost()
        {
            var reader = new StreamReader(Request.Body, Encoding.UTF8);
            var body = await reader.ReadToEndAsync();
            var submitedPost = JsonConvert.DeserializeObject<PostSubmit>(body);
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(submitedPost.Token);

            var userId = Guid.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);
            var post = new Post();
            post.Id = Guid.NewGuid();
            post.UserId = userId;
            post.Date = DateTime.Now;
            post.Username = _unitOfWork.userManager.GetUserByID(userId).Name;
            post.Content = submitedPost.Content;

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
