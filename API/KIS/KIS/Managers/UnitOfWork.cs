using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KIS.Managers
{
    
    public class UnitOfWork
    {
        public UserManager userManager;
        public PostManager postManager;
        public ReactionManager reactionManager; 
        public CommentManager commentManager;

        public UnitOfWork(UserManager userManager, PostManager postManager, ReactionManager reactionManager,  CommentManager commentManager)
        {
            this.userManager = userManager;
            this.postManager = postManager;
            this.reactionManager = reactionManager;
            this.commentManager = commentManager;
        }
    }
}
