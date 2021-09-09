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
    public class ReactionController : Controller
    {
        private UnitOfWork _unitOfWork;
        public ReactionController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("{postId}/Reactions/")]
        public IEnumerable<Reaction> GetReactions(Guid postId)
        {
            return _unitOfWork.reactionManager.GetReactionsByPost(postId);
        }

        [HttpPost]
        [Route("Reactions/Add")]
        public async Task<ActionResult> AddReaction()
        {
            var reader = new StreamReader(Request.Body, Encoding.UTF8);
            var body = await reader.ReadToEndAsync();
            var reaction = JsonConvert.DeserializeObject<Reaction>(body);
            reaction.Id = Guid.NewGuid();

            _unitOfWork.reactionManager.AddReaction(reaction);
            return Ok(reaction);
        }

        [HttpDelete]
        [Route("Reaction/Delete/{id}")]
        public async Task<ActionResult> DeleteReaction(Guid id)
        {
            if (!_unitOfWork.reactionManager.DeleteReaction(id))
                return Content("Reaction does not exist!");
            return Ok();
        }
    }
}
