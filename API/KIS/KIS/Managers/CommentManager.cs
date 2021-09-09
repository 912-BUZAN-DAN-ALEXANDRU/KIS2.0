using KIS.Models;
using KIS.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KIS.Managers
{
    public class CommentManager
    {
        private CommentRepository _commentRepository;
        public CommentManager(CommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public List<Comment> GetComments()
        {
            return _commentRepository.GetComments();
        }

        public Comment GetCommentByID(Guid ID)
        {
            return _commentRepository.GetCommentByID(ID);
        }

        public List<Comment> GetCommentsByUser(Guid UserID)
        {
            return _commentRepository.GetCommentsByUser(UserID);
        }

        public List<Comment> GetCommentsByPost(Guid PostID)
        {
            return _commentRepository.GetCommentsByPost(PostID);
        }

        public void AddComment(Comment comment)
        {
            _commentRepository.AddComment(comment);
        }

        public bool DeleteComment(Guid commentID)
        {
            return _commentRepository.DeleteComment(commentID);
        }

        public void UpdateComment(Comment comment)
        {
            _commentRepository.UpdateComment(comment);

        }
    }
}
