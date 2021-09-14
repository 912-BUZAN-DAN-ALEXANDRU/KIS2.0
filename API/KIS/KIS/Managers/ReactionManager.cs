using KIS.Models;
using KIS.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KIS.Managers
{
    public class ReactionManager
    {
        private ReactionRepository _reactionRepository;
        public ReactionManager(ReactionRepository reactionRepository)
        {
            _reactionRepository = reactionRepository;
        }

        public List<Reaction> GetReactions()
        {
            return _reactionRepository.GetReactions();
        }

        public Reaction GetReactionByID(Guid ID)
        {
            return _reactionRepository.GetReactionByID(ID);
        }

        public List<Reaction> GetReactionsByUser(Guid UserID)
        {
            return _reactionRepository.GetReactionsByUser(UserID);
        }

        public List<Reaction> GetReactionsByPost(Guid PostID)
        {
            return _reactionRepository.GetReactionsByPost(PostID);
        }

        public void AddReaction(Reaction reaction)
        {
            var r = GetReactionsByUser(reaction.UserId).Where(react => react.PostId == reaction.PostId).FirstOrDefault();

            if (r == null)
            {
                _reactionRepository.AddReaction(reaction);
                return;
            }
            if (r.ReactionType == reaction.ReactionType)
            {
                _reactionRepository.DeleteReaction(r.Id);
                return;
            }
            
            reaction.Id = r.Id;
            UpdateReaction(reaction);
        }

        public bool DeleteReaction(Guid reactionID)
        {
            return _reactionRepository.DeleteReaction(reactionID);
        }

        public void UpdateReaction(Reaction reaction)
        {
            _reactionRepository.UpdateReaction(reaction);

        }
    }
}
