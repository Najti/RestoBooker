using Restobooker.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restobooker.Domain.Interfaces
{
    public interface IUserRepository
    {
        public User UpdateUser(User user);
        public List<User> GetUsers();
        public List<User> GetUsersByFilter(string filter);
        public void DeleteUser(int id);
        public User GetUserById(int id);
        public User AddUser(User user);
        public List<User> GetDeletedUsers();
    }
}
