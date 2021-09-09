using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KIS.Models;

namespace KIS.Repositories
{
    public class PostRepository
    {
        private KISContext _context;
        public PostRepository(KISContext context)
        {
            _context = context;
        }

        public List<Post> GetPosts()
        {
            return _context.Posts.Select(item => item).ToList();
        }

        public Post GetPostByID(Guid ID)
        {
            return _context.Posts.FirstOrDefault(item => item.Id == ID);
        }

        public List<Post> GetPostsByUser(Guid UserID)
        {
            return _context.Posts.Where(item => item.UserId == UserID).ToList();

        }

        public void AddPost(Post post)
        {
            _context.Posts.Add(post);
            _context.SaveChanges();
        }

        public bool DeletePost(Guid postID)
        {
            Post _post = GetPostByID(postID);
            if (_post == null)
                return false;
            _context.Posts.Remove(_post);
            _context.SaveChanges();
            return true;
        }

        public void UpdatePost(Post post)
        {
            Post _post = GetPostByID(post.Id);
            if (post == null)
                return;

            _context.Entry(_post).CurrentValues.SetValues(post);
            _context.SaveChanges();

        }

        public void DeletePostsByUserID(Guid UserID)
        {
            GetPostsByUser(UserID).ForEach(item => DeletePost(item.Id));
            _context.SaveChanges();
        }
    }
}
