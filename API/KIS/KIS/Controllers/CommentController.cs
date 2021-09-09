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
            var comment = JsonConvert.DeserializeObject<Comment>(body);
            comment.Id = Guid.NewGuid();

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
