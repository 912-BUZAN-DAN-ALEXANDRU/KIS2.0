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
    public class CommentController : Controller
    {
        private UnitOfWork _unitOfWork;

        public CommentController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("{postId}/Comments")]
        public IEnumerable<Comment> GetComments(Guid postId)
        {
            return _unitOfWork.commentManager.GetCommentsByPost(postId);
        }

        [HttpPost]
        [Route("Comments/Add")]
        public async Task<ActionResult> AddComment()
        {
            var reader = new StreamReader(Request.Body, Encoding.UTF8);
            var body = await reader.ReadToEndAsync();
            var submitedComment = JsonConvert.DeserializeObject<CommentSubmit>(body);
            var comment = new Comment();
            comment.Id = Guid.NewGuid();
            comment.PostId = submitedComment.PostId;
            comment.Content = submitedComment.Content;
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(submitedComment.Token);

            var userId = Guid.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);
            comment.UserId = userId;
            comment.Username = _unitOfWork.userManager.GetUserByID(userId).Name;

            _unitOfWork.commentManager.AddComment(comment);
            return Ok(comment);
        }

        [HttpDelete]
        [Route("Comment/Delete/{id}")]
        public async Task<ActionResult> DeleteComment(Guid id)
        {
            if (!_unitOfWork.commentManager.DeleteComment(id))
                return Content("Comment does not exist!");
            return Ok();
        }

    }
}
