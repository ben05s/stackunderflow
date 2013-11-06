using StackUnderflow.Common.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackUnderflow.Common.BL
{
    class UserService : IUserService
    {
        private DAL.stackunderflowEntities _dal;
        public UserService(StackUnderflow.Common.DAL.stackunderflowEntities dal)
        {
            _dal = dal;
        }

        public IQueryable<User> GetAllUsers(int page)
        {
            return _dal.User
                .Where(i => i.registered == true)
                .OrderBy(i => i.user_name)
                .Skip(page * 50)
                .Take(50);
        }

        public User GetUser(int id)
        {
            var user = _dal.User.FirstOrDefault(i => i.user_id == id);
            return user;
        }

        public void Save()
        {
            _dal.SaveChanges();
        }
    }
}
