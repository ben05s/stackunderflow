using StackUnderflow.Common.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackUnderflow.Common.BL
{
    public class UserService : IUserService
    {
        private DAL.stackunderflowEntities _dal = new DAL.stackunderflowEntities();
        public UserService() { }

        public Boolean CreateUser(string username, string password, string email)
        {
            var usr = new User();
            return false;
        }

        public Boolean RegisterUser(string username)
        {
            return false;
        }

        public Boolean LoginUser(string username, string password)
        {
            return false;
        }

        public Boolean UpdatePassword(string username, string newpassword, string oldpassword)
        {
            return false;
        }

        public User GetUser(int id)
        {
            var user = _dal.User.FirstOrDefault(i => i.user_id == id);
            return user;
        }

        public IQueryable<User> GetAllUsers(int page)
        {
            return _dal.User
                .Where(i => i.registered == true)
                .OrderBy(i => i.user_name)
                .Skip(page * 50)
                .Take(50);
        }

        public IQueryable<User> SearchForUsers(string query, int page)
        {
            return null;
        }

        public IQueryable<Question> GetAllQuestions(int page)
        {
            return null;
        }

        public IQueryable<Question> SearchForQuestions(string query, int page)
        {
            return null;
        }

        public Boolean CreateQuestion(string username, string title, string content)
        {
            return false;
        }

        public Boolean CreateAnswer(int user_id, int question_id, string content)
        {
            return false;
        }

        public int RateUpAnswer(int answer_id)
        {
            return 0;
        }

        public int RateDownAnswer(int answer_id)
        {
            return 0;
        }

        public void Save()
        {
            _dal.SaveChanges();
        }
    }
}
