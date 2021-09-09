using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KIS.Models;

namespace KIS.Repositories
{
    public class ReactionRepository
    {
        private KISContext _context;
        public ReactionRepository(KISContext context)
        {
            _context = context;
        }

        public List<Reaction> GetReactions()
        {
            return _context.Reactions.Select(item => item).ToList();
        }

        public Reaction GetReactionByID(Guid ID)
        {
            return _context.Reactions.FirstOrDefault(item => item.Id == ID);
        }

        public List<Reaction> GetReactionsByUser(Guid UserID)
        {
            return _context.Reactions.Where(item => item.UserId == UserID).ToList();
        }

        public List<Reaction> GetReactionsByPost(Guid PostID)
        {
            return _context.Reactions.Where(item => item.PostId == PostID).ToList();
        } 

        public void AddReaction(Reaction reaction)
        {
           _context.Reactions.Add(reaction);
           _context.SaveChanges();
        }

        public bool DeleteReaction(Guid reactionID)
        {
            Reaction _reaction = GetReactionByID(reactionID);
            if (_reaction == null)
                return false;
            
           _context.Reactions.Remove(_reaction);
            _context.SaveChanges();
            return true;
        }

        public void UpdateReaction(Reaction reaction)
        {
            Reaction _reaction = GetReactionByID(reaction.Id);
            if (reaction == null)
                return;

           _context.Entry(_reaction).CurrentValues.SetValues(reaction);
            _context.SaveChanges();

        }

        public void DeleteReactionsByPostID(Guid PostID)
        {
            GetReactionsByPost(PostID).ForEach(item => DeleteReaction(item.Id));
            _context.SaveChanges();
        }

        public void DeleteReactionsByUserID(Guid UserID)
        {
            GetReactionsByUser(UserID).ForEach(item => DeleteReaction(item.Id));
            _context.SaveChanges();
        }
    }
}
