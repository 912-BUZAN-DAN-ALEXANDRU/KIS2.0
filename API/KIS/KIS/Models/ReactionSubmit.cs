using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KIS.Models
{
    public class ReactionSubmit
    {
        public Guid PostId { get; set; }
        public int ReactionType { get; set; }
        public string Token { get; set; }
    }
}
