using KIS.Models;
using KIS.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KIS.Managers
{
    public class PostManager
    {
        private PostRepository _postRepository;
        private ReactionRepository _reactionRepository;
        private CommentRepository _commentRepository;

        public PostManager(PostRepository postRepository, ReactionRepository reactionRepository, CommentRepository commentRepository)
        {
            _postRepository = postRepository;
            _reactionRepository = reactionRepository;
            _commentRepository = commentRepository;
        }

        public List<Post> GetPosts()
        {
            return _postRepository.GetPosts();
        }

        public Post GetPostByID(Guid ID)
        {
            return _postRepository.GetPostByID(ID);
        }

        public List<Post> GetPostsByUser(Guid UserID)
        {
            return _postRepository.GetPostsByUser(UserID);

        }

        public void AddPost(Post post)
        {
            _postRepository.AddPost(post);
        }

        public bool DeletePost(Guid postID)
        {
            _commentRepository.DeleteCommentsByPostID(postID);
            _reactionRepository.DeleteReactionsByPostID(postID);
            
            return _postRepository.DeletePost(postID);
        }

       
    }
}
