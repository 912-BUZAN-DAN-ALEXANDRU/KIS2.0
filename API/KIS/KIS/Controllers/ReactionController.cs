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
            var submitedReaction = JsonConvert.DeserializeObject<ReactionSubmit>(body);
            var reaction = new Reaction();
            reaction.Id = Guid.NewGuid();
            reaction.PostId = submitedReaction.PostId;
            reaction.ReactionType = submitedReaction.ReactionType;
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(submitedReaction.Token);

            var userId = Guid.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);
            reaction.UserId = userId;
            reaction.Username = _unitOfWork.userManager.GetUserByID(userId).Name;


            _unitOfWork.reactionManager.AddReaction(reaction);
            return Ok(reaction);
        }

       
    }
}
