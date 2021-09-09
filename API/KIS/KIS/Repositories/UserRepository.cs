using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KIS.Models;

namespace KIS.Repositories
{
    public class UserRepository
    {
        private KISContext _context;
        public UserRepository(KISContext context)
        {
            _context = context;
        }

        public List<User> GetUsers()
        {
            return _context.Users.Select(item => item).ToList();
        }

        public User GetUserByID(Guid ID)
        {
            return _context.Users.FirstOrDefault(item => item.Id == ID);
        }

        public User GetUserByName(string name)
        {
            return _context.Users.FirstOrDefault(item => item.Name == name);
        }

        public bool AddUser(User user)
        {
            if (GetUserByName(user.Name) != null)
                return false;
            _context.Users.Add(user);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteUser(Guid userID)
        {
            User _user = GetUserByID(userID);
            if (_user == null)
                return false;
            _context.Users.Remove(_user);
            _context.SaveChanges();
            return true;
        }

        public void UpdateUser(User user)
        {
            User _user = GetUserByID(user.Id);
            if (user == null)
                return;

            _context.Entry(_user).CurrentValues.SetValues(user);
            _context.SaveChanges();

        }
    }
}
