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
            _reactionRepository.AddReaction(reaction);
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
