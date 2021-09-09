using System;
using System.Collections.Generic;

#nullable disable

namespace KIS.Models
{
    public partial class Post
    {
        public Post()
        {
            Comments = new HashSet<Comment>();
            Reactions = new HashSet<Reaction>();
        }

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }
        public string Content { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Reaction> Reactions { get; set; }
    }
}
