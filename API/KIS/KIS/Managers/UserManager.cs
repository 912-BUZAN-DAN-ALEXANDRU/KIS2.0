using KIS.Helpers;
using KIS.Models;
using KIS.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace KIS.Managers
{
    public class UserManager
    {
        private UserRepository _userRepository;
        private PostRepository _postRepository;
        private CommentRepository _commentRepository;
        private ReactionRepository _reactionRepository;
        private readonly AppSettings _appSettings;

        public UserManager(UserRepository userRepository, PostRepository postRepository, CommentRepository commentRepository, ReactionRepository reactionRepository, IOptions<AppSettings> appSettings)
        {
            _userRepository = userRepository;
            _postRepository = postRepository;
            _commentRepository = commentRepository;
            _reactionRepository = reactionRepository;
            _appSettings = appSettings.Value;
        }

        public List<User> GetUsers()
        {
            return _userRepository.GetUsers();
        }

        public User GetUserByID(Guid ID)
        {
            return _userRepository.GetUserByID(ID);
        }

        public User GetUserByName(string name)
        {
            return _userRepository.GetUserByName(name);
        }

        public bool AddUser(User user)
        {
            return _userRepository.AddUser(user);
        }

        public bool DeleteUser(Guid userID)
        {
            _reactionRepository.DeleteReactionsByUserID(userID);
            _commentRepository.DeleteCommentsByUserID(userID);
            _postRepository.DeletePostsByUserID(userID);
            return _userRepository.DeleteUser(userID);
        }

        public void UpdateUser(User user)
        {
            _userRepository.UpdateUser(user);

        }

        public LoginResponse Login(LoginModel loginCredentials)
        {
            var user = _userRepository.GetUserByName(loginCredentials.Username);
            if (user == null)
                return null;
            if (!CheckPassword(loginCredentials))
                return null;

            var token = GenerateJwtToken(user);

            return new LoginResponse(user, token);
        }

        private bool CheckPassword(LoginModel login)
        {
            User user = GetUserByName(login.Username);
            if (user.Password == login.Password)
                return true;
            return false;
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
