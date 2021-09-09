using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KIS.Models;

namespace KIS.Repositories
{
    public class CommentRepository
    {
        private KISContext _context;
        public CommentRepository(KISContext context)
        {
            _context = context;
        }

        public List<Comment> GetComments()
        {
            return _context.Comments.Select(item => item).ToList();
        }

        public Comment GetCommentByID(Guid ID)
        {
            return _context.Comments.FirstOrDefault(item => item.Id == ID);
        }

        public List<Comment> GetCommentsByUser(Guid UserID)
        {
            return _context.Comments.Where(item => item.UserId == UserID).ToList();
        }

        public List<Comment> GetCommentsByPost(Guid PostID)
        {
            return _context.Comments.Where(item => item.PostId == PostID).ToList();
        }

        public void AddComment(Comment comment)
        {
            _context.Comments.Add(comment);
            _context.SaveChanges();
        }

        public bool DeleteComment(Guid commentID)
        {
            Comment _comment = GetCommentByID(commentID);
            if (_comment == null)
                return false;
            _context.Comments.Remove(_comment);
            _context.SaveChanges();
            return true;

        }

        public void UpdateComment(Comment comment)
        {
            Comment _comment = GetCommentByID(comment.Id);
            if (comment == null)
                return;

            _context.Entry(_comment).CurrentValues.SetValues(comment);
            _context.SaveChanges();

        }

        public void DeleteCommentsByPostID(Guid PostID)
        {
            GetCommentsByPost(PostID).ForEach(item => DeleteComment(item.Id));
            _context.SaveChanges();
        }

        public void DeleteCommentsByUserID(Guid UserID)
        {
            GetCommentsByUser(UserID).ForEach(item => DeleteComment(item.Id));
            _context.SaveChanges();
        }
    }
}
