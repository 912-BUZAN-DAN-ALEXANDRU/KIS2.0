using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KIS.Models
{
    public class CommentSubmit
    {
        public Guid PostId;
        public string Content;
        public string Token;
    }
}
