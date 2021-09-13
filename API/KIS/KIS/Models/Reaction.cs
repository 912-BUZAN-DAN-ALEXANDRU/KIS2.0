using System;
using System.Collections.Generic;

#nullable disable

namespace KIS.Models
{
    public partial class Reaction
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid PostId { get; set; }
        public int ReactionType { get; set; }
        public string Username { get; set; }

        public virtual Post Post { get; set; }
        public virtual User User { get; set; }
    }
}
