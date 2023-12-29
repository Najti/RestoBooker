using Restobooker.Domain.Interfaces;
using Restobooker.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restobooker.Domain.Services
{
    public class UserService
    {
        private IUserRepository repo;
        public UserService(IUserRepository repo)
        {
            this.repo = repo;
        }
        public User UpdateUser(User user) { return repo.UpdateUser(user); }
        public List<User> GetUsers() { return repo.GetUsers(); }
        public List<User> GetUsersByFilter(string filter) { return repo.GetUsersByFilter(filter); }
        public void DeleteUser(int id) { repo.DeleteUser(id); }
        public User GetUserById(int id) { return repo.GetUserById(id); }
        public User AddUser(User user) { return repo.AddUser(user); }
        public User LogUserIn(string userName) { return repo.LogUserIn(userName); }
        public List<User> GetAllUsers() { return repo.GetUsers(); }
        public List<User> GetDeletedUsers() { return repo.GetDeletedUsers(); }
    }
}
