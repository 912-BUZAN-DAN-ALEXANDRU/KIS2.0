using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KIS.Models
{
    public class LoginResponse
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }


        public LoginResponse(User user, string token)
        {
            Id = user.Id;
            Username = user.Name;
            Token = token;
        }
    }
}
