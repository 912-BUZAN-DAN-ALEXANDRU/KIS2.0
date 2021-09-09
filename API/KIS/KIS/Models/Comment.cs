using System;
using System.Collections.Generic;

#nullable disable

namespace KIS.Models
{
    public partial class Comment
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid PostId { get; set; }
        public string Content { get; set; }

        public virtual Post Post { get; set; }
        public virtual User User { get; set; }
    }
}
